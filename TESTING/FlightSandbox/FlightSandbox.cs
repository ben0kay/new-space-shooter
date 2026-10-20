using Godot;

// Sandbox-only reference geometry, flight readout and reset; no production world dependencies.
public partial class FlightSandbox : Node3D
{
    [Export] public PlayerShip Ship;
    [Export] public PlayerInput Controls;
    private Transform3D _spawn;
    private Label _readout;

    // =========================================================
    // Builds lightweight reference shapes for judging speed, scale and illumination.
    public override void _Ready()
    {
        _spawn = Ship.GlobalTransform;
        Node3D geometry = GetNode<Node3D>("TestGeometry");
        StandardMaterial3D gridMaterial = new() { AlbedoColor = new Color(0.12f, 0.25f, 0.32f) };
        for (int i = -10; i <= 10; i++)
        {
            AddBox(geometry, new Vector3(i * 20, -8, -150), new Vector3(0.12f, 0.12f, 400), gridMaterial);
            AddBox(geometry, new Vector3(0, -8, i * 20 - 150), new Vector3(400, 0.12f, 0.12f), gridMaterial);
        }
        Node3D obstacles = new() { Name = "Obstacles" };
        geometry.AddChild(obstacles);
        StandardMaterial3D rock = new() { AlbedoColor = new Color(0.36f, 0.32f, 0.27f), Roughness = 0.9f };
        for (int i = 0; i < 14; i++)
        {
            float size = 4 + i % 4 * 2;
            Vector3 position = new((i % 2 == 0 ? -1 : 1) * (18 + i % 3 * 9), i % 3 * 7 - 2, -35 - i * 24);
            MeshInstance3D visual = new()
            {
                Mesh = new SphereMesh { Radius = size, Height = size * 2, RadialSegments = 12, Rings = 6 },
                MaterialOverride = rock, Scale = new Vector3(1, 0.7f, 1.2f)
            };
            StaticBody3D body = new() { Position = position, Name = $"Rock_{i:00}" };
            obstacles.AddChild(body);
            body.AddChild(visual);
            body.AddChild(new CollisionShape3D { Shape = new SphereShape3D { Radius = size * 1.2f } });
        }
        for (int i = 1; i <= 5; i++)
        {
            Label3D marker = new() { Text = $"{i * 50} m", Position = new Vector3(0, 8, -i * 50), FontSize = 64, PixelSize = 0.02f };
            geometry.AddChild(marker);
        }
        CanvasLayer ui = GetNode<CanvasLayer>("DebugUI");
        _readout = new Label { Position = new Vector2(16, 16), MouseFilter = Control.MouseFilterEnum.Ignore };
        ui.AddChild(_readout);
    }

    // =========================================================
    // Adds one shared-material grid segment without physics overhead.
    private static void AddBox(Node parent, Vector3 position, Vector3 size, Material material)
    {
        parent.AddChild(new MeshInstance3D { Position = position, Mesh = new BoxMesh { Size = size }, MaterialOverride = material });
    }

    // =========================================================
    // Shows essential controls and actual speed without coupling HUD code to the ship.
    public override void _Process(double delta)
    {
        _readout.Text = $"FLIGHT SANDBOX   |   {Ship.Velocity.Length():0.0} m/s   |   {Engine.GetFramesPerSecond()} FPS\n"
            + "Arrows: thrust / reverse / strafe   •   Mouse: steer   •   X / Z: roll\n"
            + "Shift: boost   •   RMB: rise   •   Ctrl: descend\n"
            + "R: reset ship   •   Esc: release mouse   •   Click: resume";
    }

    // =========================================================
    // Resets at the physics boundary, including velocity, steering and buffered input.
    public override void _PhysicsProcess(double delta)
    {
        if (!_resetRequested) return;
        _resetRequested = false;
        Ship.GlobalTransform = _spawn;
        Ship.Velocity = Vector3.Zero;
        Ship.SetCommands(default);
        Ship.GetNode<ShipFlightController>("Flight").ResetFlight();
        Controls.ClearControls();
        Ship.ResetPhysicsInterpolation();
        Ship.ResetPresentation();
    }

    private bool _resetRequested;

    // =========================================================
    // Queues a reset without repeatedly triggering from keyboard echo.
    public override void _UnhandledInput(InputEvent e)
    {
        if (e is InputEventKey key && key.PhysicalKeycode == Key.R && key.Pressed && !key.Echo)
            _resetRequested = true;
    }
}
