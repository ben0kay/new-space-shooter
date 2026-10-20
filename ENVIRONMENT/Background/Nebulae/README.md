# Nebulae

LocalizedNebula.gdshaderinc implements one localized distant nebula, composed by ../SpaceSky.gdshader. It is independent of the 360-degree SkyBackdrop layer.

## Inspector
Select ENVIRONMENT/Presets/DarkBlueSkyBackdrop.tres > Shader Parameters > Nebula:
- nebula_enabled: hide/show only this formation.
- nebula_direction: world direction of its centre (default slightly left/up of initial forward view).
- nebula_radius_degrees: angular half-size. Default 29 degrees; a circular 29-degree cap covers about 6.3% of the whole sky (roughly 1/16). Actual visible coverage is smaller because of elliptical stretch and soft irregular fading.
- nebula_stretch: width/height multipliers (default 1.15, 0.85).
- nebula_rotation_degrees: rotates the elongated shape.
- nebula_edge_softness: width of the fade within the angular boundary.
- nebula_seed and nebula_shape_scale: deterministic organic pattern and wisp scale.
- nebula_brightness: independent intensity.
- nebula_outer_color / inner_color / core_color: blue outskirts, cyan denser pockets and pale inner highlights.
- nebula_extra_star_density / brightness: additional stars confined to the formation; zero density removes them.

The mask is exactly zero beyond the angular ellipse and in the opposite hemisphere. The nebula remains in a world direction while the camera turns; it does not move with ship position. No physical volume or travel parallax is modeled.

The shader samples shared seamless noise functions currently defined in SkyBackdrop/SkyBackdrop.gdshaderinc; include order is intentional. The star module exposes a density-controlled helper while retaining the general star field's previous occupancy unchanged.

The existing backdrop colours/strength and normal star settings are unchanged. Sector scenes can duplicate their Sky/material resources for independent nebula direction/size/appearance; see ../SkyBackdrop/README.md. Multiple simultaneous nebula definitions are not implemented yet.

Validation: include paths, parameter bindings, angular bounds and unchanged backdrop settings checked. No usable runtime environment was available for shader compilation or visual verification.
