# RealBus beginner setup

## Opening the project on Kali Linux

Unity 6000.6 currently needs `libxml2.so.2`, while this Kali installation supplies `libxml2.so.16`. Use the project launcher, which safely preloads the compatible copy included with Unity's Android tools:

```bash
cd /home/drac/Documents/coding/realbus-simulator
./tools/open_unity_6000_6.sh
```

The first open has to create the `Library` folder and import every asset. Leave it running while the progress bar moves. Do not open the project in two Unity versions at once.

If the title says **SAFE MODE** after an editor upgrade, wait for importing to stop, then click **Exit Safe Mode**. If Unity asks first, choose **Retry** or use `Assets > Refresh`. Safe Mode remembers errors from the initial compile even after their source files have been corrected; do not choose **Ignore** while red compile errors remain.

## What “wiring a script” means

A Unity script becomes a Component on a GameObject. Selecting that GameObject shows the Component and its assignable fields in the Inspector. Drag scene objects or project assets into those fields when automatic discovery is not appropriate.

Global RealBus managers create themselves at runtime. The bus model still needs physical references because Unity cannot safely infer wheel positions from artwork alone.

## Automatic bus setup

1. Open the gameplay scene containing your bus.
2. In the Hierarchy, select the top-level bus GameObject.
3. Choose `Tools > RealBus > Setup > Beginner Setup Wizard`.
4. Drag the bus root into **Bus root** if it is not already selected.
5. Click **Auto-wire Bus**.
6. Read the validation result. Fix only the items it lists.
7. Click **Prepare Open Gameplay Scene** if the scene does not already have its managers.
8. Save the scene with `Ctrl+S`. If the bus is a prefab, apply the intended prefab overrides.

The wizard adds the Rigidbody, BusController, fuel/maintenance, road grip, passenger load, assists, damage, temperature, and visual-effect components. It assigns existing EngineSystem, TransmissionSystem, and WheelCollider references.

The operation supports Unity Undo. Nothing is added until you select a bus and press the button, so you can inspect the result before saving.

## Imported bus and stop models

FBX files placed in `Assets/unity models/bus` and `Assets/unity models/busstop` are wired automatically after Unity refreshes and compiles scripts. To rebuild them manually, choose `Tools > RealBus > Setup > Wire All Imported Bus and Stop Models`.

Generated buses have separate visual and drivable prefabs. Drivable prefabs include the controller, Rigidbody, body collider, four WheelColliders, powertrain, fuel, maintenance, passenger weight, road grip, assists, damage, temperature, polish effects, and docking reference. Their visual prefabs are also registered as garage `BusSpec` assets.

Generated stop prefabs receive a grounded scale and static collider. `StopPropSpawner` discovers them automatically and creates the route-specific `StopTrigger` and `DockingZone` at runtime.

Review the generated wiring report at `Assets/_Game/Generated/IMPORTED_MODEL_WIRING.md`. Wheel positions are estimated from each model's rendered bounds, so visually inspect and adjust unusual models before shipping.

## WheelColliders—the manual part

WheelCollider components are invisible physics wheels; wheel meshes are only artwork. If validation says they are missing:

1. Create an empty child under the bus for each physical wheel position.
2. Add `Wheel Collider` to each child.
3. Move each WheelCollider to the exact center of its visible wheel.
4. Set its radius to match the visible tyre.
5. Keep the bus facing local positive Z and its roof toward local positive Y.
6. Run **Auto-wire Bus** again. It groups the forward axle as steering and the remaining rear axles as driven/parking wheels.

For a six-wheel bus, use two front WheelColliders and four rear WheelColliders. Duplicate rear tyres on the same side still need distinct WheelColliders only if the physics design models all six contact points.

## Body collider

Add one or more BoxColliders around the bus body. Avoid a non-convex MeshCollider on a moving Rigidbody. Keep body colliders clear of the ground and wheels, and leave **Is Trigger** disabled.

## Important BusController fields

- **Engine Data:** torque and RPM asset.
- **Transmission Data:** gear ratios and automatic shifting.
- **All Wheels:** every physical wheel.
- **Steer Wheels:** front axle.
- **Drive Wheels:** powered rear wheels.
- **Rear Wheels:** wheels used by the parking brake.
- **Model Root:** visible bus model when it is separate from the physics root.
- **Docking Reference:** point used to grade stop alignment.

## First test

1. Start with the existing Gameplay test scene.
2. Put the bus slightly above a road surface, not intersecting it.
3. Press Play and release the parking brake.
4. Test acceleration, braking, steering, reverse, doors, and docking at low speed.
5. Check the Console for red errors before changing graphics or adding more content.

Make a Git commit after the project opens cleanly in Unity 6000.6 and before converting materials to URP.
