# Sky Backdrop

Owns the full 360-degree background colour and its subtle wispy variation. This is the general sky backdrop, not a nebula object or local cloud volume. SkyBackdrop.gdshaderinc is composed by ../SpaceSky.gdshader alongside the independent star layer.

Tune ENVIRONMENT/Presets/DarkBlueSkyBackdrop.tres in Inspector:
- background_color: near-black base colour.
- backdrop_color: colour of the wispy variation; any colour is supported.
- backdrop_strength: brightness of that variation. Default 0.18, down from the previous 0.85.
- backdrop_direction: strongest colour region; use a nonzero vector.

The original variation shapes are retained. Tint is now fully derived from backdrop_color instead of containing a fixed blue low-tone. Stars keep their independent brightness. Nebulae/ is reserved for future smaller, brighter, localized distant features.

## Future sector scenes
Each sector can instance ENVIRONMENT/SpaceEnvironment.tscn. For an independent look, duplicate Background/SpaceBackground.tres and its ShaderMaterial preset into that sector folder, point the duplicated Sky to the duplicated material, and assign it through Rendering's Environment > Sky. Make the Environment resource unique in the sector scene before overriding Sky. Do not edit a shared material expecting only one sector to change.

No sector manager or runtime switching is introduced here; these are native resources suitable for separately loaded sector scenes.
