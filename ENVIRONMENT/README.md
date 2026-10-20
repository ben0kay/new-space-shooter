# ENVIRONMENT — Space Visual Systems

**Status: initial reusable space environment implemented.** FlightSandbox instances SpaceEnvironment.tscn with a procedural star/blue-nebula background, one rendering owner and separate sunlight. Atmosphere, visible sun/lens flare, planets, runtime controllers and full sector presets remain planned.

## Current implementation
- SpaceEnvironment.tscn composes Lighting/SpaceLighting.tscn and Rendering/SpaceRendering.tscn.
- Rendering owns the only WorldEnvironment; Background/SpaceBackground.tres supplies its sky.
- Background/SpaceSky.gdshader reuses the old project's star and nebula algorithms only.
- Presets/DarkBlueSpace.tres is a sky ShaderMaterial preset: edit star brightness, nebula strength, background/nebula colours and nebula direction in Inspector.
- No ship dependencies, per-frame environment scripts, asteroids, local fog/clouds, or visible sun/planet in this pass. The existing warm physical sunlight is retained.
- This is a native scene/resource composition; config/controller scripts will be added when runtime changes warrant them.

## Purpose
Own the **general appearance of outer space**: the light that illuminates spacecraft and rocks, the rendering/post-processing pipeline, distant scenery and nearby atmospheric visual details.

Keep ownership explicit:
- **Lighting/**: the lights illuminating 3D geometry, shadows, directional sun, and the visual lens flare tied to it.
- **Rendering/**: one shared Godot `WorldEnvironment`, background/ambient illumination settings, tonemapping, exposure, glow and reflections.
- **Background/**: infinite/distant sky appearance — stars, sky nebulae, sun disc and distant planetary silhouettes.
- **Atmosphere/**: spatial visual effects — distance haze, localized gas clouds and nearby travelling dust.
- **Presets/**: per-region/sector look definitions that configure the preceding modules.

## Intended folder layout

```text
ENVIRONMENT/
├── Lighting/
│   ├── SpaceLighting.cs                 [planned]
│   ├── SpaceLighting.tscn               [implemented]
│   ├── LightingConfig.cs                [planned]
│   ├── Sun/
│   │   ├── SunLight.cs                  [planned]
│   │   ├── SunLensFlare.cs             [planned]
│   │   └── SunLensFlare.gdshader       [planned]
│   └── README.md
├── Rendering/
│   ├── SpaceRendering.cs               [planned]
│   ├── SpaceRendering.tscn             [implemented]
│   ├── RenderingConfig.cs              [planned]
│   └── README.md
├── Background/
│   ├── SpaceSky.gdshader                [implemented]
│   ├── SpaceBackground.cs              [planned]
│   ├── BackgroundConfig.cs             [planned]
│   ├── Stars/
│   ├── Nebulae/
│   ├── Planets/
│   └── README.md
├── Atmosphere/
│   ├── DistanceHaze/
│   ├── Clouds/
│   ├── Dust/
│   └── README.md
├── Presets/
│   ├── DeepSpace.tres                   [planned]
│   ├── MiningBelt.tres                  [planned]
│   └── NebulaRegion.tres                [planned]
├── EnvironmentController.cs            [planned]
├── EnvironmentMasterConfig.cs          [planned]
├── SpaceEnvironment.tscn               [implemented]
└── README.md
```

**Outside this folder, planned later:** `TESTING/EnvironmentTest/EnvironmentTest.tscn` and its README. The test scene is not being implemented in this structure-only pass.

## System boundaries
1. **One owner of the `WorldEnvironment` resource.** Rendering creates/owns it. The controller coordinates requests from lighting, sky and haze without conflicting overwrites.
2. **One source of truth for sun direction/color.** A `DirectionalLight3D` illuminates real objects, while the sky sun disc and lens flare are synchronized **visuals**, not separate physical light sources.
3. **Background nebulae are not local cloud volumes.** Sky imagery belongs in `Background/`; floating spatial clouds belong in `Atmosphere/Clouds/`.
4. **Visual dust is not gameplay debris.** Camera-relative/space-travel dust belongs here; collision debris, explosions and weapon effects belong with their future gameplay/visual effects systems.
5. **No ship-specific assumptions in reusable environment modules.** Get the active camera or an explicitly supplied observer instead of requiring a `PlayerShip` parent.
6. **Configs hold settings; controllers coordinate; shaders render.** Avoid putting unrelated features into a single large script.

## Old-project reference / migration plan

From [godot-space-shooter-new](https://github.com/ben0kay/godot-space-shooter-new/tree/main/ENVIRONMENT):
- `SpaceEnvironment.cs`: split responsibilities between Lighting, Rendering and Background.
- `SpaceSky.gdshader`: start under Background; later separate specific sky features if justified.
- `SunLensFlare.cs` and `SunLensFlare.gdshader`: Lighting/Sun.
- `SectorAtmosphere.cs`: separate distance haze and spatial dust clouds.
- `SpaceTravelClouds.cs` + `SpaceCloud.gdshader`: Atmosphere/Clouds.
- `SpaceTravelDust.cs` + `SpaceDust.gdshader`: Atmosphere/Dust.
- `SectorDust.gdshader`: Atmosphere/Clouds or DistanceHaze depending on which effect consumes it.

Keep the **appearance and useful algorithms** from the old game, but reorganize dependencies before migration. Do not assume existing classes are drop-in compatible after moving paths.

## Presets and test workflow
- Change sector appearance by applying **one environment preset** (sun, ambient light, background, haze and particle options).
- Start with a lightweight visual test scene containing a camera, representative ship/rock geometry and a controllable sun.
- Validate light hitting objects, shadows, dark sides, glow and lens flare **before** adding floating clouds and dust.
- Test each component independently with switches for quick A/B comparisons.
- Prefer simple, performance-friendly effects and avoid heavy work every frame.

## Implementation conventions
Follow root [CodePreferences.md](../CodePreferences.md): modular C#, clear folder READMEs, exported inspector settings, centralized frequently used configuration, comments/regions where appropriate and good performance. **Names and ownership are proposals, subject to changes as we build the test scene.**
