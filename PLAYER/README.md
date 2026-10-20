# PLAYER

Player session, input and camera are separate from the physical spacecraft. PlayerController routes commands; the ship continues coasting when control is released. PlayerDefaults.tres is the shared tuning entry point. Walking is planned, not implemented.

## Chase camera and model response

PLAYER/PlayerDefaults.tres exposes Camera and Visuals resources. Camera owns delayed world-space follow, movement framing and boost pullback/FOV. Visuals owns cosmetic banking/pitch. Ship owns physical movement and telemetry; PlayerController assigns configuration. See Camera/README.md and Ship/Visuals/README.md for tuning.

Walking remains planned. Cosmetic tilt must not move walkable interior collision or the future walking character.
