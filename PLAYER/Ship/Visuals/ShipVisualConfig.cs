using Godot;

// Cosmetic model tilt only; these values never change flight physics or hull collision.
[GlobalClass]
public partial class ShipVisualConfig : Resource
{
    [ExportGroup("Ship Visual Tilt")]
    [Export] public bool Enabled = true;
    [Export(PropertyHint.Range, "0,30,0.5")] public float PitchTiltDegrees = 6f;
    [Export(PropertyHint.Range, "0,45,0.5")] public float TurnBankDegrees = 12f;
    [Export(PropertyHint.Range, "0,45,0.5")] public float StrafeBankDegrees = 10f;
    [Export(PropertyHint.Range, "0,45,0.5")] public float RollTiltDegrees = 14f;
    [Export(PropertyHint.Range, "0,30,0.1")] public float SmoothingSpeed = 8f;
}
