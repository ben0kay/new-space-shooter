# Presets

DarkBlueSkyBackdrop.tres is the default sky material preset. It replaces DarkBlueSpace.tres and renames the old nebula parameters to backdrop parameters.

Inspector > Shader Parameters:
- star_brightness: 1.15; unchanged.
- background_color: nearly black base.
- backdrop_strength: 0.18; substantially darker than the previous 0.85.
- backdrop_color: muted blue tint, freely replaceable with other colours.
- backdrop_direction: strongest variation direction.

This configures sky backdrop colours plus independently composed stars. The independent Nebula parameter group adds one localized formation; its direction, angular size, seed, colours, brightness and extra star density can vary per sector. Existing backdrop values remain unchanged. See Background/Nebulae/README.md for all controls. Sector scenes can use their own duplicated Sky/material resources; see Background/SkyBackdrop/README.md. Full lighting/atmosphere sector presets remain future work.
