// =========================================================
// Generates varied star positions.
vec3 star_random(vec2 cell, float seed)
{
	return fract(sin(vec3(
		dot(cell, vec2(127.1, 311.7)) + seed,
		dot(cell, vec2(269.5, 183.3)) + seed,
		dot(cell, vec2(419.2, 371.9)) + seed
	)) * 43758.5453);
}

// =========================================================
// Draws one sparse layer of distant stars.
vec3 star_layer_with_occupancy(vec2 coordinates, float face, float density, float seed, float occupancy)
{
	vec2 grid = coordinates * density;
	vec3 random = star_random(floor(grid), seed + face * 37.0);

	if (random.z > clamp(occupancy, 0.0, 1.0))
		return vec3(0.0);

	float distance_to_star = length(
		fract(grid) - (vec2(0.2) + random.xy * 0.6)
	);

	float star = 1.0 - smoothstep(0.02, 0.10, distance_to_star);
	vec3 tint = mix(
		vec3(0.65, 0.78, 1.0),
		vec3(1.0, 0.88, 0.72),
		random.x
	);

	return tint * star * mix(0.3, 1.0, random.y);
}


// =========================================================
// Preserves the original general star field independently of nebula density.
vec3 star_layer(vec2 coordinates, float face, float density, float seed)
{
    return star_layer_with_occupancy(coordinates, face, density, seed, 0.025);
}
