using Godot;

// Smooth world-space chase rig; model banking never feeds back into camera orientation.
public partial class PlayerCameraController : Node3D
{
    #region References And State
    [Export] public Marker3D Target;
    [Export] public Camera3D Camera;
    [Export] public PlayerMasterConfig Config;

    private PlayerShip _ship;
    private Vector3 _previousTargetPosition;
    private Vector3 _movementOffset;
    private float _boost;
    private int _revision = -1;
    private bool _initialized;
    #endregion

    #region Setup
    // =========================================================
    // Resolves optional flight telemetry and starts with an exact chase view.
    public override void _Ready()
    {
        if (Target == null || Camera == null || Config?.Camera == null)
        {
            GD.PushError("PlayerCameraController requires target, camera and config.");
            SetProcess(false);
            return;
        }
        _ship = Target.GetParent() as PlayerShip;
        Camera.Current = true;
        UpdateCamera(0, true);
    }

    // =========================================================
    // Reads the rendered target transform rather than unsmoothed physics positions.
    public override void _Process(double delta) { UpdateCamera((float)delta); }

    // =========================================================
    // Returns a frame-rate-independent response; zero explicitly selects instant follow.
    private static float Blend(float speed, float seconds) =>
        speed <= 0 ? 1f : 1f - Mathf.Exp(-speed * seconds);
    #endregion

    #region Chase Presentation
    // =========================================================
    // Smooths position and rotation independently, snapping cleanly on reset or teleport.
    private void UpdateCamera(float seconds, bool instant = false)
    {
        if (!GodotObject.IsInstanceValid(Target)) { _initialized = false; return; }
        PlayerCameraConfig s = Config.Camera;
        Transform3D target = Target.GetGlobalTransformInterpolated();
        bool hasShip = GodotObject.IsInstanceValid(_ship);
        int revision = hasShip ? _ship.PresentationRevision : 0;
        float snapDistance = Mathf.Max(1f, s.TeleportSnapDistance);
        bool snap = instant || !_initialized || revision != _revision
            || target.Origin.DistanceSquaredTo(_previousTargetPosition) > snapDistance * snapDistance;
        _previousTargetPosition = target.Origin;
        _revision = revision;

        Vector3 desiredOffset = Vector3.Zero;
        float desiredBoost = 0;
        if (hasShip && _ship.Config != null)
        {
            PlayerShipConfig flight = _ship.Config;
            Vector3 localVelocity = target.Basis.Orthonormalized().Inverse() * _ship.Velocity;
            float sideways = Mathf.Clamp(localVelocity.X / Mathf.Max(0.01f, flight.StrafeSpeed), -1, 1);
            float vertical = Mathf.Clamp(localVelocity.Y / Mathf.Max(0.01f, flight.VerticalSpeed), -1, 1);
            float movement = Mathf.Clamp(_ship.Velocity.Length() / Mathf.Max(0.01f, flight.ForwardSpeed), 0, 1);
            desiredOffset = new Vector3(
                -sideways * s.StrafeOffset - _ship.YawInput * s.YawSway,
                -vertical * s.VerticalOffset - _ship.PitchInput * s.PitchSway,
                movement * s.MovementPullback);
            desiredBoost = _ship.Commands.Active && _ship.Commands.Boost ? 1f : 0f;
        }
        _movementOffset = snap ? desiredOffset
            : _movementOffset.Lerp(desiredOffset, Blend(s.MovementResponse, seconds));
        _boost = snap ? desiredBoost : Mathf.Lerp(_boost, desiredBoost, Blend(s.BoostResponse, seconds));

        float positionBlend = snap || !s.SmoothFollow ? 1f : Blend(s.PositionFollowSpeed, seconds);
        float rotationBlend = snap || !s.SmoothFollow ? 1f : Blend(s.RotationFollowSpeed, seconds);
        Vector3 position = GlobalPosition.Lerp(target.Origin, positionBlend);
        Vector3 lag = position - target.Origin;
        float maxLag = Mathf.Max(0, s.MaximumPositionLag);
        if (lag.LengthSquared() > maxLag * maxLag)
            position = target.Origin + lag.Normalized() * maxLag;

        Basis rotation = snap ? target.Basis.Orthonormalized()
            : GlobalBasis.Orthonormalized().Slerp(target.Basis.Orthonormalized(), rotationBlend);
        GlobalTransform = new Transform3D(rotation, position);

        Vector3 offset = _movementOffset;
        offset.Z = Mathf.Lerp(offset.Z, s.BoostPullback, _boost);
        Camera.Position = s.LocalOffset + offset;
        // Fixed rig-local aim lets the model move within the frame instead of recentering it.
        Vector3 aim = s.LookAtOffset - s.LocalOffset;
        if (aim.LengthSquared() > 0.0001f)
        {
            Vector3 up = Mathf.Abs(aim.Normalized().Dot(Vector3.Up)) > 0.99f
                ? Vector3.Forward : Vector3.Up;
            Camera.Basis = Basis.LookingAt(aim, up);
        }
        Camera.Fov = Mathf.Clamp(s.FieldOfView + _boost * s.BoostFovIncrease, 20, 120);
        Camera.Far = Mathf.Max(Camera.Near + 1, s.FarDistance);
        _initialized = true;
    }
    #endregion
}
