# Phase 2 bus physics

Source: RealBus_MasterTimeline.docx, sections 2.1–2.10.

Open `Assets/_Game/Scenes/PhysicsTestTrack.unity` and enter Play mode. The fixture builds a 500 × 500 m surface, six-wheel bus, 10% hill at x=50, camera and mobile controls. The Inspector's engine/transmission profiles are independent Phase 2 assets; production profiles are unchanged. Geometry is generated at runtime, so inspect Bus_Root during Play mode. Assign an audio loop to its engine AudioSource to hear RPM-driven pitch (no audio asset is bundled).

Keyboard: W throttle, S brake, A/D steering, R retarder, P parking brake, X forward/reverse (only below 1 km/h), Q/E manual shifts, H horn (requires a clip). Automatic shifting is enabled by default; disable `automatic` on the transmission profile for manual-only operation. Touch buttons select Buttons, SteeringWheel, or Tilt; the selection persists. Drag above the steering widget's center for neutral and left/right for steering. Tilt uses the accelerometer with landscape orientation compensation; tune sensitivity on device. The existing InputActions asset now also defines a composite Steer axis. The prototype reads keyboard devices and UI inputs directly through the Input System.

For production buses, preserve wheel ordering FL, FR, ML, MR, RL, RR. Add PassengerLoadDynamics and assign the matching PassengerManager. Its load center can shift vertically and laterally. Add AerodynamicsSystem and set world-space `windVelocity` in m/s from a future weather integration. Existing buses without the component retain controller drag. The test fixture deliberately isolates physics from mission, weather and maintenance systems.

## Validation

Run `BusPhysicsTests` in Unity Test Runner / EditMode. These tests cover steering geometry, transmission thresholds/reverse, drag, passenger center of mass, engine curve, parking/service brake interaction and released inputs. They do not replace driving tests.

Use T or Start recording at rest immediately before accelerating; Y or Finish recording closes a CSV under `Application.persistentDataPath`. Each file contains time, speed, RPM, gear, position, body roll/pitch, mass, service brake and parking state. Start a fresh recording for each maneuver. The summary uses -1 for an acceleration threshold never reached. Peak speed is a measured maximum, not proof of a sustained terminal speed. Stop distance starts when full brake is applied between 45 and 55 km/h; apply at 50 for the prescribed test.

| Test | Acceptance | Procedure |
| --- | --- | --- |
| 0–50 | 15–22 seconds | Empty bus, level surface, full throttle from rest |
| 0–80 | 45–65 seconds | Same; longer run needed beyond the pad |
| Top speed | 82–95 km/h | Extend the straight or use a larger surface; hold until speed stabilizes |
| Emergency stop | 22–35 m | Full brake at 50 km/h; inspect CSV/summary |
| U-turn | Fits 24 m wide | Trace CSV x/z footprint including 2.55 m width and 12 m body overhang |
| Roll | 3–6 degrees outward | Hold 40 km/h on a constant-radius turn; inspect roll column |
| Hill start | No rollback on 10% grade | Place bus aligned uphill on ramp in Play mode, hold parking brake, apply throttle and release; inspect displacement |
| Laden comparison | About 30% slower 0–50 | Set PassengerLoadDynamics.testPassengerCount to 80 and repeat identical empty run |

Also measure braking pitch (1–2 degrees), inspect jitter, and profile 60 FPS on S20 / 30+ FPS on A54 and touch latency below 16 ms. Add the test scene temporarily to a device development build for those checks.

## Units and tuning limits

WheelCollider.motorTorque takes Nm: engine torque × gear ratio × differential ratio, divided among the four driven wheels. The timeline's division by wheel radius converts torque to force and must not be passed to motorTorque. Engine braking and retarder use passive brake torque so they cannot accelerate the bus backwards. Service braking remains 12,000 Nm total, parking brake acts on each rear wheel, and fade reaches its maximum 30% reduction after 60 seconds at full brake while moving.

The fixture preserves the specified mass, damping, suspension and tire values. These are starting parameters, not validated calibration. In particular, 0.3 linear damping creates substantial speed-dependent resistance, and 12,000 Nm total braking on 0.5 m wheels gives only about 2 m/s² deceleration for 12 tonnes before other resistance. The document's acceleration, top-speed and stopping-distance targets may require revising those parameters after measurement. Do not mark the milestone passed without actual results.

Validation in this workspace: source/asset consistency and diff whitespace checks passed. Unity execution and EditMode tests could not run: no Unity editor/assemblies were found, generated csproj references another machine, and the dotnet build stops at missing project.assets.json. Driving and physical-device results remain unmeasured.
