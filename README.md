# New Space Shooter

Ship-focused exploration/survival game in Godot 4.7 .NET / C#.

Read GameVision.md, CodePreferences.md and AsteroidOptimizationNotes.md for direction. ENVIRONMENT/README.md describes the planned visual systems.

## Current playable baseline

Run TESTING/FlightSandbox/FlightSandbox.tscn (F5 main scene). Build with Godot .NET and the .NET 8 SDK first. Original Cyan Interceptor model and prototype flight response migrated into modular PLAYER folders; smooth chase camera is independent of the ship model. See TESTING/FlightSandbox/README.md for controls and PLAYER/README.md for ownership.

Tune PLAYER/PlayerDefaults.tres for ship/camera settings; key bindings are on Player/Input in the sandbox. Adjust camera lag, movement offsets, boost pullback/FOV and cosmetic ship tilt through the Camera and Visuals resources. Walking/interiors and thruster/trail effects remain planned. The original repository remains unchanged.

## Validation

C# sources compiled successfully against GodotSharp 4.7.2 using Roslyn. Resource references and original model integrity checked; Godot headless imported the GLB successfully. Full SDK build/startup was blocked by the local .NET process-information failure, so interactive flight/camera validation remains to be done in the Godot .NET editor.

Latest camera/visual response update: source and scene wiring reviewed; compilation/runtime testing was unavailable because no execution environment was available for this pass.
