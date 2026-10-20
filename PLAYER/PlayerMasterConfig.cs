using Godot;

// Entry point for frequently adjusted player settings; referenced resources stay modular.
[GlobalClass]
public partial class PlayerMasterConfig : Resource
{
    [Export] public PlayerShipConfig Ship { get; set; } = new();
    [Export] public PlayerCameraConfig Camera { get; set; } = new();
    [Export] public ShipVisualConfig Visuals { get; set; } = new();
    [Export] public bool CaptureMouseOnStart = true;
}
