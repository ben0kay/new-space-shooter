using Godot;

// Preserves prototype steering, velocity response and boost without combat/resource dependencies.
public partial class ShipFlightController : Node
{
    [Export] public PlayerShip Ship;
    private float _pitchRate, _yawRate;

    // =========================================================
    // Clears steering inertia when the sandbox resets or pilot changes.
    public void ResetFlight()
    {
        _pitchRate = _yawRate = 0;
        if (!GodotObject.IsInstanceValid(Ship)) return;
        Ship.PitchInput = Ship.YawInput = Ship.StrafeInput = Ship.RollInput = 0;
    }

    // =========================================================
    // Applies buffered commands after the player controller has supplied them.
    public override void _PhysicsProcess(double delta)
    {
        if (!GodotObject.IsInstanceValid(Ship) || Ship.Config == null) return;
        float seconds = (float)delta;
        ShipFlightCommands c = Ship.Commands;
        PlayerShipConfig s = Ship.Config;
        UpdateRotation(seconds, c, s);
        if (!c.Active)
        {
            // Unattended ships coast; releasing control does not delete or freeze them.
            Ship.MoveAndSlide();
            return;
        }
        Ship.StrafeInput = c.Strafe;
        float thrust = c.Boost ? 1f : c.Thrust;
        float speed = c.Boost ? s.ForwardSpeed * Mathf.Max(1, s.BoostSpeedMultiplier)
            : thrust >= 0 ? s.ForwardSpeed : s.ReverseSpeed;
        Vector3 target = -Ship.GlobalBasis.Z * thrust * speed
            + Ship.GlobalBasis.X * c.Strafe * s.StrafeSpeed
            + Ship.GlobalBasis.Y * c.Rise * s.VerticalSpeed;
        float response = target == Vector3.Zero ? s.Deceleration : s.Acceleration;
        if (c.Boost) response *= Mathf.Max(1, s.BoostAccelerationMultiplier);
        Ship.Velocity = Ship.Velocity.MoveToward(target, Mathf.Max(0, response) * seconds);
        Ship.MoveAndSlide();
    }

    // =========================================================
    // Smooths mouse steering and applies local pitch, yaw and roll as in the prototype.
    private void UpdateRotation(float seconds, ShipFlightCommands c, PlayerShipConfig s)
    {
        if (!c.Active) { ResetFlight(); return; }
        float safeSeconds = Mathf.Max(seconds, 0.0001f);
        float pitchLimit = Mathf.DegToRad(Mathf.Max(0, s.MaxPitchSpeedDegrees));
        float yawLimit = Mathf.DegToRad(Mathf.Max(0, s.MaxYawSpeedDegrees));
        float desiredPitch = Mathf.Clamp(c.MouseMotion.Y * s.MousePitchSensitivity / safeSeconds, -pitchLimit, pitchLimit);
        float desiredYaw = Mathf.Clamp(-c.MouseMotion.X * s.MouseYawSensitivity / safeSeconds, -yawLimit, yawLimit);
        float blend = 1f - Mathf.Exp(-Mathf.Max(0, s.SteeringResponse) * seconds);
        _pitchRate = Mathf.Lerp(_pitchRate, desiredPitch, blend);
        _yawRate = Mathf.Lerp(_yawRate, desiredYaw, blend);
        Ship.PitchInput = pitchLimit > 0.0001f ? _pitchRate / pitchLimit : 0;
        Ship.YawInput = yawLimit > 0.0001f ? _yawRate / yawLimit : 0;
        Ship.RollInput = c.Roll;
        Ship.RotateObjectLocal(Vector3.Right, _pitchRate * seconds);
        Ship.RotateObjectLocal(Vector3.Up, _yawRate * seconds);
        Ship.RotateObjectLocal(Vector3.Forward, Mathf.DegToRad(c.Roll * s.RollSpeed) * seconds);
        Ship.Basis = Ship.Basis.Orthonormalized();
    }
}
