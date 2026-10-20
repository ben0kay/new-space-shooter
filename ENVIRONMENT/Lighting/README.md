# Lighting

**Role:** Illumination of real geometry, shadows and the directional sun.

## Planned files
- `SpaceLighting.cs` — coordinates physical light nodes and updates.
- `SpaceLighting.tscn` — reusable lighting scene.
- `LightingConfig.cs` — tweakable sunlight, shadow and lighting settings.
- `Sun/SunLight.cs` — controls sun `DirectionalLight3D` (intensity, direction, color, shadows).
- `Sun/SunLensFlare.cs` — projected screen flare, visibility/occlusion and fading.
- `Sun/SunLensFlare.gdshader` — actual visual flare presentation.

**Boundary:** Light hitting the hull and casting shadows belongs here. Global exposure/glow belongs to Rendering; the drawn solar disc in the sky belongs to Background. All should receive the same sun direction and color from the coordinated environment settings.

**Old reference:** `SpaceEnvironment.cs`, `SunLensFlare.cs`, `SunLensFlare.gdshader`.
