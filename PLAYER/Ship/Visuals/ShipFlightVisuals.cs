using Godot;

// Tilts the authored visual pivot independently of the hull, interior markers and camera target.
public partial class ShipFlightVisuals : Node
{
    [Export] public PlayerShip Ship;
    [Export] public Node3D Pivot;
    private Vector3 _angles;
    private Basis _restBasis;
    private int _revision = -1;

    // =========================================================
    // Preserves the authored pivot orientation without runtime reparenting.
    public override void _Ready()
    {
        if (Ship == null || Pivot == null)
        {
            GD.PushError("ShipFlightVisuals requires Ship and Pivot.");
            SetProcess(false);
            return;
        }
        _restBasis = Pivot.Basis;
    }

    // =========================================================
    // Smooths cosmetic pitch and banking, clearing previous tilt on sandbox reset.
    public override void _Process(double delta)
    {
        if (!GodotObject.IsInstanceValid(Ship) || !GodotObject.IsInstanceValid(Pivot)) return;
        ShipVisualConfig s = Ship.VisualConfig;
        if (_revision != Ship.PresentationRevision)
        {
            _revision = Ship.PresentationRevision;
            _angles = Vector3.Zero;
        }
        Vector3 desired = Vector3.Zero;
        if (s != null && s.Enabled)
        {
            float bank = Ship.YawInput * s.TurnBankDegrees
                - Ship.StrafeInput * s.StrafeBankDegrees - Ship.RollInput * s.RollTiltDegrees;
            desired = new Vector3(Mathf.DegToRad(Ship.PitchInput * s.PitchTiltDegrees), 0, Mathf.DegToRad(bank));
        }
        float speed = s?.SmoothingSpeed ?? 0f;
        float blend = speed <= 0 ? 1f : 1f - Mathf.Exp(-speed * (float)delta);
        _angles = _angles.Lerp(desired, blend);
        Pivot.Basis = _restBasis * Basis.FromEuler(_angles);
    }
}
