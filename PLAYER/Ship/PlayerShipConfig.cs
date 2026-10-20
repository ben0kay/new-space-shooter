using Godot;

// Authored flight tuning; each ship keeps its movement state separately.
[GlobalClass]
public partial class PlayerShipConfig : Resource
{
    [ExportGroup("Movement")]
    [Export] public float ForwardSpeed = 18f;
    [Export] public float ReverseSpeed = 8f;
    [Export] public float StrafeSpeed = 12f;
    [Export] public float VerticalSpeed = 10f;
    [Export] public float Acceleration = 24f;
    [Export] public float Deceleration = 18f;
    [ExportGroup("Steering")]
    [Export] public float RollSpeed = 75f;
    [Export] public float MousePitchSensitivity = 0.0008f;
    [Export] public float MouseYawSensitivity = 0.0008f;
    [Export] public float MaxPitchSpeedDegrees = 110f;
    [Export] public float MaxYawSpeedDegrees = 135f;
    [Export] public float SteeringResponse = 14f;
    [ExportGroup("Boost")]
    [Export] public float BoostSpeedMultiplier = 2.2f;
    [Export] public float BoostAccelerationMultiplier = 1.8f;
}
