shader_type sky;
render_mode disable_fog;

// Composes independent stars, the full-surround colour backdrop and one localized nebula.
// Background only: physical sunlight belongs to Lighting; no ship or camera dependencies.

uniform float star_brightness = 0.9;
uniform float backdrop_strength : hint_range(0.0, 2.0, 0.01) = 0.18;

uniform vec4 background_color : source_color = vec4(0.0005, 0.001, 0.003, 1.0);
uniform vec4 backdrop_color : source_color = vec4(0.10, 0.20, 0.40, 1.0);
uniform vec3 backdrop_direction = vec3(-0.25, 0.18, -1.0);


#include "res://ENVIRONMENT/Background/SkyBackdrop/SkyBackdrop.gdshaderinc"
#include "res://ENVIRONMENT/Background/Stars/StarField.gdshaderinc"
#include "res://ENVIRONMENT/Background/Nebulae/LocalizedNebula.gdshaderinc"

// =========================================================
// Composes an infinite backdrop without horizon, sun-disc or nearby geometry.
void sky()
{
	vec3 direction = normalize(EYEDIR);
	vec3 absolute_direction = abs(direction);
	vec2 coordinates;
	float face;

	if (absolute_direction.x >= absolute_direction.y
		&& absolute_direction.x >= absolute_direction.z)
	{
		coordinates = direction.yz / absolute_direction.x;
		face = direction.x > 0.0 ? 0.0 : 1.0;
	}
	else if (absolute_direction.y >= absolute_direction.z)
	{
		coordinates = direction.xz / absolute_direction.y;
		face = direction.y > 0.0 ? 2.0 : 3.0;
	}
	else
	{
		coordinates = direction.xy / absolute_direction.z;
		face = direction.z > 0.0 ? 4.0 : 5.0;
	}

	coordinates = coordinates * 0.5 + 0.5;

	vec3 stars = star_layer(coordinates, face, 180.0, 11.0);
	stars += star_layer(coordinates, face, 75.0, 83.0) * 1.3;

	vec3 backdrop = draw_sky_backdrop(direction);
	float star_attenuation = clamp(
		1.0 - length(backdrop) * 1.5, 0.45, 1.0
	);

	vec3 background = background_color.rgb
		+ backdrop
		+ stars * star_brightness * star_attenuation;

    vec2 formation = nebula_density(direction);
    vec3 extra_stars = vec3(0.0);
    if (formation.y > 0.0 && nebula_extra_star_density > 0.0)
    {
        extra_stars = star_layer_with_occupancy(
            coordinates, face, 210.0, 151.0 + nebula_seed,
            clamp(nebula_extra_star_density, 0.0, 1.0) * 0.25)
            * formation.y * max(0.0, nebula_extra_star_brightness);
    }
    COLOR = background + nebula_emission(formation) + extra_stars;
}
