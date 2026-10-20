# Rendering

SpaceRendering.tscn owns the single WorldEnvironment used by SpaceEnvironment.tscn. Its Environment references Background/SpaceBackground.tres and owns ambient illumination, tonemapping and glow.

Edit the Environment resource in this scene for ambient colour/energy and post-processing. The sky is visual background, while ambient illumination is deliberately authored separately so nebula brightness does not unexpectedly relight the ship.

No runtime configuration scripts yet. Avoid adding a second WorldEnvironment to sandbox scenes.
