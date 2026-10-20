# Background

Infinite procedural space backdrop, independent of the ship, grid and physical lighting.

Implemented:
- SpaceSky.gdshader: two sparse star layers and broad, dark blue nebula formations adapted from the previous Godot prototype.
- SpaceBackground.tres: Sky resource using Presets/DarkBlueSpace.tres as its ShaderMaterial.
- Rendering/SpaceRendering.tscn consumes this sky; Background does not create another WorldEnvironment.

Select ENVIRONMENT/Presets/DarkBlueSpace.tres in Inspector and expand Shader Parameters to adjust star_brightness, nebula_strength, background_color, nebula_color and nebula_direction. Keep direction nonzero. No horizon or spatial edge: the backdrop surrounds the camera at any world position.

The old shader's visible sun and planet were intentionally excluded from this stars/nebula pass. Stars/ and Nebulae/ document the shared shader features; Planets/ remains planned. No local cloud volumes or gameplay geometry.
