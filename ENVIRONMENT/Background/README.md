# Background

**Role:** Distant/infinite sky imagery — the view behind physical game objects.

## Planned
- `SpaceBackground.cs`: manages sky presentation and sector-specific backdrop settings.
- `BackgroundConfig.cs`: star/nebula/planet and visible-sun appearance options.
- `SpaceSky.gdshader`: starting shader for space background.
- `Stars/`: far star field / points.
- `Nebulae/`: distant nebula formations and sky colour.
- `Planets/`: distant planetary discs/silhouettes and their appearance.

These are visual backdrop elements, *not* mineable spatial asteroids or close-up volumetric clouds. Planet geometry that becomes interactable would be managed elsewhere.

**Old reference:** `SpaceSky.gdshader`; star, nebula, sun-disc and planet code currently combined.
