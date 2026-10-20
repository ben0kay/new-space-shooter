# Visuals

PlayerShipVisual.tscn instances the original Cyan_Interceptor_Mk2.glb at its original 0.4 scale. Materials remain embedded.

ShipFlightVisuals tilts the authored Visuals pivot using normalized flight steering and strafe/roll requests. Actual hull orientation, collision, camera markers and future interiors remain separate. No runtime reparenting.

Select PLAYER/PlayerDefaults.tres and expand Visuals:
- Enabled switches cosmetic tilt on/off (returns smoothly to rest).
- Pitch Tilt Degrees, Turn Bank Degrees, Strafe Bank Degrees and Roll Tilt Degrees set independent amounts.
- Smoothing Speed controls response; lower positive values are softer, zero is instant.

Defaults: pitch 6, turn bank 12, strafe bank 10, manual roll tilt 14 degrees, smoothing 8. Cosmetic settings do not change flight handling. Thrusters/trails remain future work.
