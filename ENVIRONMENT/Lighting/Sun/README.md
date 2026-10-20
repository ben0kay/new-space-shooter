# Sun

**Role:** Shared directional stellar light plus camera-facing flare.

## Planned
- `SunLight.cs`: real directional sunlight and shadow settings.
- `SunLensFlare.cs`: positions/fades flare relative to the active camera and occluders.
- `SunLensFlare.gdshader`: draws lens-flare shapes.

The sun disc visible in the *sky* is rendered by Background, not this folder. The real `DirectionalLight3D`, sky sun disc and lens flare must agree on direction and color.

**Old reference:** `SpaceEnvironment.cs` and `SunLensFlare.cs` + shader.
