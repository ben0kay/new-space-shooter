# Camera

Smooth chase camera following Ship/ChaseView in world space. The target stays outside the cosmetic model pivot. Rendered physics interpolation is sampled before camera smoothing.

## Inspector tuning

Select PLAYER/PlayerDefaults.tres and expand Camera:
- Camera Framing: Local Offset (X sideways, Y height, Z distance behind), Look At Offset, FOV and far distance.
- Camera Follow: position and rotation speeds independently control catch-up. Lower positive values mean more delay; zero means instant. Smooth Follow off makes target tracking instant. Maximum Position Lag caps rig displacement (zero locks its position to the ship); Teleport Snap Distance prevents long catch-up after large jumps.
- Movement Response: strafe/vertical offsets, yaw/pitch sway, normal movement pullback and smoothing speed. Zero amounts disable individual effects.
- Boost: total pullback while boosting, optional FOV increase (zero disables) and transition response.

Defaults: position response 12, rotation 8, maximum positional lag 2 units, boost pullback 2.1 and FOV increase 2 degrees. Changes are read during play. Response zero always means instant, never frozen.

Startup, explicit sandbox reset and detected target teleports snap cleanly. Call PlayerShip.ResetPresentation() alongside ResetPhysicsInterpolation() for future travel teleports. A new target is currently expected to be assigned before scene startup; cockpit/walking camera switching remains future work.

The camera has no obstacle avoidance yet; large rocks can occlude it. Follow speeds are response rates, not durations in seconds.
