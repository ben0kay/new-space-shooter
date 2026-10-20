# Background

Distant sky presentation, independent of ship movement and physical lighting.

- SkyBackdrop/: full-surround background colours and subtle wispy variation.
- Stars/: independent procedural star layers.
- Nebulae/: one localized organic formation with independent colours, angular placement and extra stars.
- Planets/: planned.
- SpaceSky.gdshader: composes the backdrop, star and localized nebula shader includes.
- SpaceBackground.tres: Sky resource referencing Presets/DarkBlueSkyBackdrop.tres.

Edit the material preset in Inspector > Shader Parameters. background_color is the base, backdrop_color/strength/direction control the variation, and star_brightness controls stars. The Nebula Inspector group independently controls the localized formation; it does not replace the full-surround backdrop.

See SkyBackdrop/README.md for independent per-sector resource overrides. Rendering remains the only WorldEnvironment owner.
