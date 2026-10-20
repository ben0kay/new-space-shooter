# Lighting

SpaceLighting.tscn owns the physical DirectionalLight3D that lights the ship/grid, including shadows. It preserves the sandbox's existing direction, warm colour and energy.

Edit its Sun node in Inspector to change illumination. Background stars/nebulae live separately under Background. Visible sun disc and lens flare remain planned under Sun/; when introduced, they must share this physical sun's direction.
