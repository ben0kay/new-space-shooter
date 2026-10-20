using Godot;

// Physical spacecraft; remains alive and simulated when pilot control is released.
public partial class PlayerShip : CharacterBody3D
{
    [Export] public PlayerShipConfig Config;
    public ShipVisualConfig VisualConfig { get; set; }
    public float PitchInput { get; set; }
    public float YawInput { get; set; }
    public float StrafeInput { get; set; }
    public float RollInput { get; set; }
    public int PresentationRevision { get; private set; }

    // =========================================================
    // Invalidates cosmetic history after an explicit reset or teleport.
    public void ResetPresentation() { PresentationRevision++; }

    public ShipFlightCommands Commands { get; private set; }

    // =========================================================
    // Selects free-space collision behaviour with no floor or gravity assumptions.
    public override void _Ready() { MotionMode = MotionModeEnum.Floating; }

    // =========================================================
    // Accepts commands from the current pilot without owning the player session.
    public void SetCommands(ShipFlightCommands commands) { Commands = commands; }
}
