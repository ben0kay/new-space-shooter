// =========================================================
// Generates repeatable noise values without a large sine calculation.
float hash_value(vec3 p)
{
	p = fract(p * 0.1031);
	p += dot(p, p.yzx + vec3(33.33));

	return fract((p.x + p.y) * p.z);
}

// =========================================================
// Interpolates seamless three-dimensional noise.
float noise_value(vec3 p)
{
	vec3 i = floor(p);
	vec3 f = fract(p);
	f = f * f * (3.0 - 2.0 * f);

	return mix(
		mix(
			mix(hash_value(i), hash_value(i + vec3(1, 0, 0)), f.x),
			mix(hash_value(i + vec3(0, 1, 0)),
				hash_value(i + vec3(1, 1, 0)), f.x), f.y
		),
		mix(
			mix(hash_value(i + vec3(0, 0, 1)),
				hash_value(i + vec3(1, 0, 1)), f.x),
			mix(hash_value(i + vec3(0, 1, 1)),
				hash_value(i + vec3(1, 1, 1)), f.x), f.y
		), f.z
	);
}

// =========================================================
// Combines broad shapes and finer wisps.
float layered_noise(vec3 p)
{
	return noise_value(p) * 0.53
		+ noise_value(p * 2.03 + vec3(17.0)) * 0.27
		+ noise_value(p * 4.11 + vec3(31.0)) * 0.14
		+ noise_value(p * 8.17 + vec3(53.0)) * 0.06;
}

// =========================================================
// Draws full-surround sky colour variation; localized nebula features are reserved for later.
vec3 draw_sky_backdrop(vec3 ray)
{
	vec3 p = ray * vec3(2.5, 3.5, 2.5);

	vec3 warp = vec3(
		noise_value(p + vec3(11.0)),
		noise_value(p + vec3(37.0)),
		noise_value(p + vec3(71.0))
	) - vec3(0.5);

	vec3 pattern_point = p + warp * 0.7;
	float formations = layered_noise(pattern_point + vec3(19.0));
	float wisps = layered_noise(pattern_point * 2.0 + vec3(43.0));

	float coverage = smoothstep(0.25, 0.80, formations);
	float detail = mix(0.75, 1.0, wisps);

	// Broad formations distributed around different parts of the sky.
	float primary = smoothstep(
		-0.25, 0.95,
		dot(ray, normalize(backdrop_direction + vec3(0.00001)))
	);

	float opposite = smoothstep(
		-0.20, 0.95,
		dot(ray, normalize(vec3(0.65, -0.10, 1.0)))
	);

	float upper = smoothstep(
		0.0, 0.95,
		dot(ray, normalize(vec3(-0.75, 0.65, 0.35)))
	);

	// A faint baseline prevents the opposite hemisphere becoming empty.
	float region = 0.22
		+ primary * primary * 0.78
		+ opposite * opposite * 0.65
		+ upper * upper * 0.35;

	region = min(region, 1.0);

	vec3 tint = mix(
		backdrop_color.rgb * 0.08,
		backdrop_color.rgb * 0.35,
		smoothstep(0.30, 0.75, formations)
	);

	return tint * coverage * detail * region * backdrop_strength;
}

