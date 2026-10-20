# New Space Shooter

Ship-focused exploration/survival game in Godot 4.7 .NET / C#.

Read GameVision.md, CodePreferences.md and AsteroidOptimizationNotes.md for direction. ENVIRONMENT/README.md describes the planned visual systems.

## Current playable baseline

Run TESTING/FlightSandbox/FlightSandbox.tscn (F5 main scene). Build with Godot .NET and the .NET 8 SDK first. Original Cyan Interceptor model and prototype flight response migrated into modular PLAYER folders; smooth chase camera is independent of the ship model. See TESTING/FlightSandbox/README.md for controls and PLAYER/README.md for ownership.

Tune PLAYER/PlayerDefaults.tres for ship/camera settings; key bindings are on Player/Input in the sandbox. Adjust camera lag, movement offsets, boost pullback/FOV and cosmetic ship tilt through the Camera and Visuals resources. Walking/interiors and thruster/trail effects remain planned. The original repository remains unchanged.

## Validation

C# sources compiled successfully against GodotSharp 4.7.2 using Roslyn. Resource references and original model integrity checked; Godot headless imported the GLB successfully. Full SDK build/startup was blocked by the local .NET process-information failure, so interactive flight/camera validation remains to be done in the Godot .NET editor.

Latest camera/visual response update: source and scene wiring reviewed; compilation/runtime testing was unavailable because no execution environment was available for this pass.

## Modular space backdrop

FlightSandbox now instances ENVIRONMENT/SpaceEnvironment.tscn: procedural stars and a dark blue sky backdrop adapted from the old prototype, separate physical sunlight and one rendering owner. The test grid and distance markers remain; rocks have been removed. Edit ENVIRONMENT/Presets/DarkBlueSkyBackdrop.tres in Inspector for sky appearance. See ENVIRONMENT/README.md for implemented/planned boundaries.

This backdrop update was source/resource checked only; no usable environment was available for shader compilation or visual runtime verification.

Sky colour classification: full-surround wispy colour now belongs to ENVIRONMENT/Background/SkyBackdrop/. Nebulae/ is reserved for future localized features. The default backdrop is substantially darker; stars retain their previous brightness. Per-sector resource override instructions are in Background/SkyBackdrop/README.md. This update was source/resource checked; visual runtime verification was unavailable.
