using Godot;

// Owns pilot input routing independently of the ship; future walking transfers ownership here.
public partial class PlayerController : Node
{
    [Export] public PlayerMasterConfig Config;
    [Export] public PlayerShip Ship;
    [Export] public PlayerInput Controls;
    public bool IsPiloting { get; private set; } = true;

    // =========================================================
    // Applies shared tuning and supplies input before flight simulation.
    public override void _Ready()
    {
        ProcessPhysicsPriority = -20;
        if (Config?.Ship == null || Ship == null || Controls == null)
        {
            GD.PushError("PlayerController requires config, ship and controls.");
            SetPhysicsProcess(false);
            return;
        }
        Ship.Config = Config.Ship;
        if (Config.CaptureMouseOnStart) Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    // =========================================================
    // Routes requests only to the actively piloted ship.
    public override void _PhysicsProcess(double delta)
    {
        if (GodotObject.IsInstanceValid(Ship))
            Ship.SetCommands(IsPiloting ? Controls.ReadCommands() : default);
    }

    // =========================================================
    // Releases the helm without disabling the physical ship; walking is a future feature.
    public void SetPiloting(bool piloting)
    {
        IsPiloting = piloting;
        Controls?.SetEnabled(piloting);
        Ship?.SetCommands(default);
        Input.MouseMode = piloting ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible;
    }

    // =========================================================
    // Returns pointer ownership when the scene closes.
    public override void _ExitTree() { Input.MouseMode = Input.MouseModeEnum.Visible; }
}
