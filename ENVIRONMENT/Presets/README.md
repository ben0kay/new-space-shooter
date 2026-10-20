# Presets

DarkBlueSpace.tres is an implemented ShaderMaterial preset for the procedural space sky. Select it in Inspector and expand Shader Parameters:
- star_brightness: star intensity (default 1.15).
- nebula_strength: cloud intensity (default 0.85).
- background_color: near-black blue baseline.
- nebula_color: dark blue cloud tint.
- nebula_direction: strongest formation direction; use a nonzero vector.

Background/SpaceBackground.tres references it. This material is shared by scenes that reference that sky; duplicate both resources for an independent look. It is a background-only preset, not yet a full sector lighting/rendering/atmosphere config. Planned DeepSpace/MiningBelt/NebulaRegion presets and runtime orchestration remain future work.
