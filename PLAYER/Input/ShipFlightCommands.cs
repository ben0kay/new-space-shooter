using Godot;

// One physics tick of pilot requests, independent of the ship and input device.
public struct ShipFlightCommands
{
    public bool Active;
    public float Thrust, Strafe, Rise, Roll;
    public bool Boost;
    public Vector2 MouseMotion;
}
