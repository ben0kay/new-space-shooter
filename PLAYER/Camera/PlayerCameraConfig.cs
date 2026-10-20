using Godot;

// Chase framing and response. Zero follow response means instant tracking, not a frozen camera.
[GlobalClass]
public partial class PlayerCameraConfig : Resource
{
    [ExportGroup("Camera Framing")]
    [Export] public Vector3 LocalOffset = new(0, 3.5f, 9);
    [Export] public Vector3 LookAtOffset = new(0, 0.5f, -8);
    [Export(PropertyHint.Range, "20,120,1")] public float FieldOfView = 75f;
    [Export] public float FarDistance = 20000f;

    [ExportGroup("Camera Follow")]
    [Export] public bool SmoothFollow = true;
    [Export(PropertyHint.Range, "0,30,0.1")] public float PositionFollowSpeed = 12f;
    [Export(PropertyHint.Range, "0,30,0.1")] public float RotationFollowSpeed = 8f;
    [Export(PropertyHint.Range, "0,10,0.1")] public float MaximumPositionLag = 2f;
    [Export] public float TeleportSnapDistance = 30f;

    [ExportGroup("Movement Response")]
    [Export] public float StrafeOffset = 0.8f;
    [Export] public float VerticalOffset = 0.45f;
    [Export] public float YawSway = 0.12f;
    [Export] public float PitchSway = 0.08f;
    [Export] public float MovementPullback = 0.6f;
    [Export(PropertyHint.Range, "0,30,0.1")] public float MovementResponse = 5f;

    [ExportGroup("Boost")]
    [Export] public float BoostPullback = 2.1f;
    [Export(PropertyHint.Range, "0,20,0.1")] public float BoostFovIncrease = 2f;
    [Export(PropertyHint.Range, "0,30,0.1")] public float BoostResponse = 5f;
}
