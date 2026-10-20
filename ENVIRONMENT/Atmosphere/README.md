# Atmosphere

**Role:** Spatial environmental visuals occupying nearby or medium-distance space.

- **DistanceHaze/**: distance-dependent softening/tint of geometry, where appropriate.
- **Clouds/**: local gas/dust wisps and larger spatial cloud cards.
- **Dust/**: tiny floating travel specks and subtle parallax cues near the player/camera.

These are independent features. Sector-specific parameters come from Presets via the environment controller.

**Old reference:** `SectorAtmosphere.cs`, `SpaceTravelClouds.cs`, `SpaceTravelDust.cs`, and their shaders. Avoid tying reusable visual modules directly to `PlayerShip`.
