using Godot;

// Fixed chase view tuning, separate from spacecraft visuals and movement.
[GlobalClass]
public partial class PlayerCameraConfig : Resource
{
    [Export] public Vector3 LocalOffset = new(0, 3.5f, 9);
    [Export] public Vector3 LookAtOffset = new(0, 0.5f, -8);
    [Export] public float FieldOfView = 75f;
    [Export] public float FarDistance = 20000f;
}
