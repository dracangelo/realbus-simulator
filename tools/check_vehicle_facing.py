#!/usr/bin/env python3
"""Which way do the downloaded vehicle models face?

TrafficModelImporter has to know which end of a model is the nose, because nothing in an
FBX records it and a model that is left as authored can drive backwards through the city.
The importer decides this from the names and positions of the model's parts, and this
script replays that same rule straight off the FBX files, so the decision can be checked
without opening the editor.

    python3 tools/check_vehicle_facing.py [files or folders ...]

With no arguments it reads Assets/unity models/traffic. 'already +Z' means the importer
leaves the model alone; 'TURN to +Z' means it will rotate it 180 degrees.

Part positions come from the FBX node transforms. A model whose named parts all sit at
their parent's origin and carry the offset inside their mesh - the Van download does -
reads as 'left as authored' here even though the editor's importer, which measures real
renderer bounds, can still tell its ends apart. Treat this script as the quick check and
the importer's own '[Traffic] <model> -> ... facing ...' log line as the authority.
"""
import os
import struct
import sys
import zlib

DEFAULT_ROOT = os.path.join('Assets', 'unity models', 'traffic')
SOURCE_SUFFIXES = ('.fbx', '.obj')


class Node:
    def __init__(self, name):
        self.name = name
        self.props = []
        self.children = []

    def find(self, name):
        return [c for c in self.children if c.name == name]


def read_properties(buf, pos, count):
    props = []
    for _ in range(count):
        kind = buf[pos:pos + 1].decode('latin1')
        pos += 1
        if kind == 'Y':
            props.append(struct.unpack_from('<h', buf, pos)[0]); pos += 2
        elif kind == 'C':
            props.append(buf[pos]); pos += 1
        elif kind == 'I':
            props.append(struct.unpack_from('<i', buf, pos)[0]); pos += 4
        elif kind == 'F':
            props.append(struct.unpack_from('<f', buf, pos)[0]); pos += 4
        elif kind == 'D':
            props.append(struct.unpack_from('<d', buf, pos)[0]); pos += 8
        elif kind == 'L':
            props.append(struct.unpack_from('<q', buf, pos)[0]); pos += 8
        elif kind in 'fdlib':
            count_here, encoding, length = struct.unpack_from('<III', buf, pos)
            pos += 12
            data = buf[pos:pos + length]
            pos += length
            if encoding == 1:
                data = zlib.decompress(data)
            fmt = {'f': 'f', 'd': 'd', 'l': 'q', 'i': 'i', 'b': 'b'}[kind]
            props.append(list(struct.unpack_from('<%d%s' % (count_here, fmt), data, 0)))
        elif kind in 'SR':
            length = struct.unpack_from('<I', buf, pos)[0]
            pos += 4
            props.append(buf[pos:pos + length].decode('latin1'))
            pos += length
        else:
            raise ValueError('unknown FBX property type %r' % kind)
    return props, pos


def read_node(buf, pos, wide):
    if wide:
        end, count, _ = struct.unpack_from('<QQQ', buf, pos)
        pos += 24
    else:
        end, count, _ = struct.unpack_from('<III', buf, pos)
        pos += 12
    if end == 0:
        return None, pos
    name_length = buf[pos]
    pos += 1
    node = Node(buf[pos:pos + name_length].decode('latin1'))
    pos += name_length
    node.props, pos = read_properties(buf, pos, count)
    while pos < end:
        child, pos = read_node(buf, pos, wide)
        if child is None:
            break
        node.children.append(child)
    return node, end


def read_parts(path):
    """[(part name, position along Z, [material names])] for one model file."""
    with open(path, 'rb') as handle:
        buf = handle.read()
    if not buf.startswith(b'Kaydara FBX Binary'):
        return []
    version = struct.unpack_from('<I', buf, 23)[0]
    wide = version >= 7500
    top, pos = [], 27
    while pos < len(buf):
        node, pos = read_node(buf, pos, wide)
        if node is None:
            break
        top.append(node)

    root = Node('Root')
    root.children = top
    objects = root.find('Objects')
    connections = root.find('Connections')
    models, materials = {}, {}
    for obj in (objects[0].children if objects else []):
        if obj.name == 'Model' and len(obj.props) >= 3:
            models[obj.props[0]] = obj
        elif obj.name == 'Material' and len(obj.props) >= 2:
            materials[obj.props[0]] = obj

    parent, model_materials = {}, {}
    for connection in (connections[0].children if connections else []):
        if connection.name != 'C' or connection.props[0] != 'OO':
            continue
        child, holder = connection.props[1], connection.props[2]
        if child in models:
            parent[child] = holder
        elif child in materials:
            model_materials.setdefault(holder, []).append(materials[child].props[1].split('\x00')[0])

    def local(uid):
        for properties in models[uid].find('Properties70'):
            for prop in properties.children:
                if prop.props and prop.props[0] == 'Lcl Translation':
                    return tuple(float(v) for v in prop.props[4:7])
        return (0.0, 0.0, 0.0)

    def along_z(uid):
        chain, current, seen = [], uid, set()
        while current in models and current not in seen:
            seen.add(current)
            chain.append(current)
            current = parent.get(current)
        return sum(local(u)[2] for u in reversed(chain))

    return [(models[uid].props[1].split('\x00')[0], along_z(uid), model_materials.get(uid, []))
            for uid in models]


def has_token(name, token):
    at = name.find(token)
    while at >= 0:
        before = name[at - 1] if at > 0 else ' '
        end = at + len(token)
        after = name[end] if end < len(name) else ' '
        if not before.isalnum() and not after.isalnum():
            return True
        at = name.find(token, at + 1)
    return False


def is_front(name):
    return (name == 'front' or name.startswith('front ') or name.startswith('front_')
            or name.startswith('front-') or 'headlight' in name or 'head light' in name
            or 'headlamp' in name)


def is_rear(name):
    return (name in ('rear', 'back') or name.startswith('rear ') or name.startswith('rear_')
            or name.startswith('rear-') or 'taillight' in name or 'tail light' in name
            or 'brakelight' in name)


def nose_evidence(part_name):
    """Mirrors TrafficModelRules.NoseEvidence."""
    name = part_name.strip().lower()
    if not name:
        return 0.0
    if is_front(name):
        return 1.0
    if is_rear(name):
        return -1.0
    if any(t in name for t in ('windscreen', 'windshield', 'bonnet', 'hood', 'grille', 'grill',
                               'radiator', 'steering', 'dashboard', 'dash', 'cockpit',
                               'numberplate', 'licenseplate')):
        return .5
    if any(t in name for t in ('trunk', 'boot', 'tailgate', 'exhaust', 'muffler',
                               'backwindow', 'rearwindow')):
        return -.5
    if any(has_token(name, t) for t in ('fl', 'fr', 'front')):
        return .25
    if any(has_token(name, t) for t in ('bl', 'br', 'back', 'rear')):
        return -.25
    return 0.0


def face(parts, units_in_metre=100.0):
    """Mirrors TrafficModelImporter.FacingFromParts: 1, -1 or 0."""
    if not parts:
        return 0, 'no parts', 0.0, 0.0
    positions = [z for _, z, _ in parts]
    centre = (min(positions) + max(positions)) / 2.0
    half_length = (max(positions) - min(positions)) / 2.0
    deadband = max(.05 * units_in_metre, half_length * .08)

    part_votes = material_votes = 0.0
    seen = []
    for name, z, materials in parts:
        vote = 0.0 if abs(z - centre) < deadband else (1.0 if z > centre else -1.0) * nose_evidence(name)
        if abs(vote) > .01:
            part_votes += vote
            seen.append('%s@%.1f%s' % (name, z, '+' if vote > 0 else '-'))
        for material in materials:
            material_vote = 0.0 if abs(z - centre) < deadband else (1.0 if z > centre else -1.0) * nose_evidence(material)
            if abs(material_vote) > .01:
                material_votes += material_vote

    facing = 1 if part_votes >= .5 else -1 if part_votes <= -.5 else 0
    if facing == 0:
        facing = 1 if material_votes >= 1.5 else -1 if material_votes <= -1.5 else 0
    verdict = ('already +Z' if facing > 0 else 'TURN to +Z' if facing < 0 else 'left as authored')
    return facing, verdict, part_votes, material_votes


def sources(arguments):
    found = []
    for path in arguments or [DEFAULT_ROOT]:
        if os.path.isdir(path):
            for folder, _, files in os.walk(path):
                for name in sorted(files):
                    if name.lower().endswith(SOURCE_SUFFIXES):
                        found.append(os.path.join(folder, name))
        else:
            found.append(path)
    return sorted(found)


def main(arguments):
    files = sources(arguments)
    if not files:
        print('No vehicle models found under %s' % (arguments or [DEFAULT_ROOT]))
        return 1

    for path in files:
        parts = read_parts(path)
        if not parts:
            print('%-28s no FBX parts read (ASCII FBX, or not a model?)' % os.path.basename(path))
            continue
        facing, verdict, part_votes, material_votes = face(parts)
        print('%-28s partVotes=%+.2f materialVotes=%+.2f  -> %s'
              % (os.path.basename(path), part_votes, material_votes, verdict))
    return 0


if __name__ == '__main__':
    sys.exit(main(sys.argv[1:]))
