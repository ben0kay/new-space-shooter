// One distant organic formation. The angular envelope is zero outside its sky region.
group_uniforms Nebula;
uniform bool nebula_enabled = true;
uniform vec3 nebula_direction = vec3(-0.25, 0.18, -1.0);
uniform float nebula_radius_degrees : hint_range(2.0, 70.0, 0.5) = 29.0;
uniform vec2 nebula_stretch = vec2(1.15, 0.85);
uniform float nebula_rotation_degrees : hint_range(-180.0, 180.0, 1.0) = -20.0;
uniform float nebula_edge_softness : hint_range(0.05, 0.8, 0.01) = 0.4;
uniform float nebula_seed : hint_range(0.0, 1000.0, 1.0) = 47.0;
uniform float nebula_shape_scale : hint_range(1.0, 10.0, 0.1) = 3.5;
uniform float nebula_brightness : hint_range(0.0, 2.0, 0.01) = 0.3;
uniform vec4 nebula_outer_color : source_color = vec4(0.04, 0.12, 0.32, 1.0);
uniform vec4 nebula_inner_color : source_color = vec4(0.08, 0.5, 0.68, 1.0);
uniform vec4 nebula_core_color : source_color = vec4(0.7, 0.85, 0.95, 1.0);
uniform float nebula_extra_star_density : hint_range(0.0, 1.0, 0.01) = 0.35;
uniform float nebula_extra_star_brightness : hint_range(0.0, 2.0, 0.01) = 0.65;
group_uniforms;

// =========================================================
// Returns organic density and regional star mask without changing the underlying backdrop.
// Uses shared seamless noise from SkyBackdrop.gdshaderinc.
vec2 nebula_density(vec3 ray)
{
    if (!nebula_enabled) return vec2(0.0);
    vec3 axis = length(nebula_direction) > 0.0001
        ? normalize(nebula_direction) : vec3(0.0, 0.0, -1.0);
    float forward = dot(ray, axis);
    if (forward <= 0.0) return vec2(0.0);

    vec3 reference = abs(axis.y) > 0.95 ? vec3(1.0, 0.0, 0.0) : vec3(0.0, 1.0, 0.0);
    vec3 right = normalize(cross(axis, reference));
    vec3 up = normalize(cross(right, axis));
    float angle = acos(clamp(forward, -1.0, 1.0));
    vec2 tangent = vec2(dot(ray, right), dot(ray, up));
    vec2 angular = length(tangent) > 0.0001 ? normalize(tangent) * angle : vec2(0.0);
    float rotation = radians(nebula_rotation_degrees);
    vec2 rotated = vec2(
        cos(rotation) * angular.x - sin(rotation) * angular.y,
        sin(rotation) * angular.x + cos(rotation) * angular.y);
    vec2 point = rotated / (radians(clamp(nebula_radius_degrees, 2.0, 70.0))
        * max(abs(nebula_stretch), vec2(0.1)));
    float radius = length(point);
    if (radius >= 1.0) return vec2(0.0);

    vec3 seed = vec3(nebula_seed * 0.37, nebula_seed * 0.71, nebula_seed * 0.19);
    vec3 p = vec3(point * max(1.0, nebula_shape_scale), 0.7) + seed;
    vec3 warp = vec3(noise_value(p + vec3(11.0)), noise_value(p + vec3(37.0)), 0.5) - vec3(0.5);
    float structure = layered_noise(p + warp * 1.4);
    float wisps = layered_noise(p * 2.7 + vec3(29.0));
    float irregular_radius = radius + (noise_value(p * 0.8) - 0.5) * 0.22;
    float envelope = 1.0 - smoothstep(
        1.0 - clamp(nebula_edge_softness, 0.05, 0.8), 1.0, radius);
    float lobes = 1.0 - smoothstep(0.4, 1.0, irregular_radius);
    float density = envelope * smoothstep(0.25, 0.78, structure)
        * mix(0.55, 1.0, wisps) * mix(0.4, 1.0, lobes);
    return vec2(density, envelope * mix(0.25, 1.0, smoothstep(0.25, 0.7, structure)));
}

// =========================================================
// Blends broad blue into cyan, with pale highlights concentrated in denser inner pockets.
vec3 nebula_emission(vec2 density)
{
    float body = density.x;
    float inner = smoothstep(0.09, 0.38, body);
    float core = smoothstep(0.30, 0.62, body);
    vec3 tint = mix(nebula_outer_color.rgb, nebula_inner_color.rgb, inner);
    tint = mix(tint, nebula_core_color.rgb, core);
    return tint * body * max(0.0, nebula_brightness);
}
