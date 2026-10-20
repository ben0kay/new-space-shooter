# Background

Distant sky presentation, independent of ship movement and physical lighting.

- SkyBackdrop/: full-surround background colours and subtle wispy variation.
- Stars/: independent procedural star layers.
- Nebulae/: reserved for future smaller, brighter localized nebula features; currently empty apart from documentation.
- Planets/: planned.
- SpaceSky.gdshader: composes the implemented backdrop and star shader includes.
- SpaceBackground.tres: Sky resource referencing Presets/DarkBlueSkyBackdrop.tres.

Edit the material preset in Inspector > Shader Parameters. background_color is the base, backdrop_color/strength/direction control the variation, and star_brightness controls stars. No nebula settings remain in the implemented shader.

See SkyBackdrop/README.md for independent per-sector resource overrides. Rendering remains the only WorldEnvironment owner.
