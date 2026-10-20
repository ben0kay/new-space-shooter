# Rendering

**Role:** Global scene-render appearance and the **single owner** of `WorldEnvironment`.

## Planned
- `SpaceRendering.cs`: manages/updates one Godot Environment resource.
- `SpaceRendering.tscn`: reusable scene containing `WorldEnvironment` as needed.
- `RenderingConfig.cs`: exposure, tonemapping, glow, ambient/reflected lighting, color and global post-process configuration.

Other modules provide environment inputs (sky resource from Background, haze parameters from Atmosphere, sun information from Lighting); they should not independently replace the complete `WorldEnvironment` resource.

**Old reference:** Rendering and ambient setup currently mixed into `SpaceEnvironment.cs`.
