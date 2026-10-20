using Godot;

// Locks a separate camera rig to the ship chase marker without orbit or follow lag.
public partial class PlayerCameraController : Node3D
{
    [Export] public Marker3D Target;
    [Export] public Camera3D Camera;
    [Export] public PlayerMasterConfig Config;

    // =========================================================
    // Configures the view once; camera ownership can later transfer to a walking target.
    public override void _Ready()
    {
        if (Target == null || Camera == null || Config?.Camera == null)
        {
            GD.PushError("PlayerCameraController requires target, camera and config.");
            SetProcess(false);
            return;
        }
        Camera.Fov = Config.Camera.FieldOfView;
        Camera.Far = Config.Camera.FarDistance;
        Camera.Current = true;
        UpdateCamera();
    }

    // =========================================================
    // Follows the rendered ship transform so physics interpolation stays smooth.
    public override void _Process(double delta) { UpdateCamera(); }

    // =========================================================
    // Uses fixed ship-local position and aim offsets, including ship roll.
    private void UpdateCamera()
    {
        if (!GodotObject.IsInstanceValid(Target)) return;
        Transform3D frame = Target.GetGlobalTransformInterpolated();
        GlobalTransform = frame;
        Camera.Position = Config.Camera.LocalOffset;
        Camera.LookAt(frame * Config.Camera.LookAtOffset, frame.Basis.Y);
    }
}
