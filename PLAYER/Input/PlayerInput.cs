using Godot;

// Reads pilot input without depending on a ship ancestor; walking input can be added later.
public partial class PlayerInput : Node
{
    #region Configuration
    [Export] public Key ForwardKey = Key.Up;
    [Export] public Key ReverseKey = Key.Down;
    [Export] public Key StrafeLeftKey = Key.Left;
    [Export] public Key StrafeRightKey = Key.Right;
    [Export] public Key RollLeftKey = Key.X;
    [Export] public Key RollRightKey = Key.Z;
    [Export] public Key BoostKey = Key.Shift;
    [Export] public Key DescendKey = Key.Ctrl;
    [Export] public MouseButton AscendButton = MouseButton.Right;
    #endregion

    private Vector2 _mouse;
    private bool _focused = true;
    public bool Enabled { get; private set; } = true;

    // =========================================================
    // Changes input ownership and removes buffered mouse movement.
    public void SetEnabled(bool enabled) { Enabled = enabled; ClearControls(); }

    // =========================================================
    // Discards accumulated look input after reset or control transfer.
    public void ClearControls() { _mouse = Vector2.Zero; }

    // =========================================================
    // Releases the pointer with Escape and recaptures it with a click.
    public override void _UnhandledInput(InputEvent e)
    {
        if (!Enabled || !_focused) return;
        if (e is InputEventKey key && key.PhysicalKeycode == Key.Escape && key.Pressed && !key.Echo)
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
            ClearControls();
        }
        else if (e is InputEventMouseButton button && button.Pressed && Input.MouseMode != Input.MouseModeEnum.Captured)
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            ClearControls();
        }
        else if (e is InputEventMouseMotion motion && Input.MouseMode == Input.MouseModeEnum.Captured)
            _mouse += motion.ScreenRelative;
    }

    // =========================================================
    // Prevents background input after switching applications.
    public override void _Notification(int what)
    {
        if (what == NotificationApplicationFocusOut)
        {
            _focused = false;
            Input.MouseMode = Input.MouseModeEnum.Visible;
            ClearControls();
        }
        else if (what == NotificationApplicationFocusIn) _focused = true;
    }

    // =========================================================
    // Consumes one tick of input; mouse delta is applied once only.
    public ShipFlightCommands ReadCommands()
    {
        ShipFlightCommands c = new();
        if (Enabled && _focused && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            c.Active = true;
            c.Thrust = Axis(ForwardKey, ReverseKey);
            c.Strafe = Axis(StrafeRightKey, StrafeLeftKey);
            c.Roll = Axis(RollRightKey, RollLeftKey);
            c.Rise = (Input.IsMouseButtonPressed(AscendButton) ? 1 : 0) - (Input.IsPhysicalKeyPressed(DescendKey) ? 1 : 0);
            c.Boost = Input.IsPhysicalKeyPressed(BoostKey);
            c.MouseMotion = _mouse;
        }
        ClearControls();
        return c;
    }

    // =========================================================
    // Converts two physical keys into a signed movement axis.
    private static float Axis(Key positive, Key negative) =>
        (Input.IsPhysicalKeyPressed(positive) ? 1 : 0) - (Input.IsPhysicalKeyPressed(negative) ? 1 : 0);
}
