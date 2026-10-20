# FlightSandbox

Open FlightSandbox.tscn and press F6, or F5 as the project main scene. Requires Godot 4.7 .NET and .NET 8 SDK; build the C# project first. Contains the original ship model, smooth chase camera, temporary sunlight/sky, grid, distance labels and collidable rocks. Arrows move, mouse steers, X/Z roll, Shift boosts, RMB rises, Ctrl descends, R resets, Escape releases mouse and click resumes. Initial speeds: forward 18, reverse 8, strafe 12, rise 10 m/s; boost 2.2x. Tune PLAYER/PlayerDefaults.tres in Inspector. Temporary lighting does not implement the ENVIRONMENT migration plan. No walking, weapons, mining, fuel or save system yet.

Camera position/rotation lag and cosmetic ship tilt are adjustable independently through PLAYER/PlayerDefaults.tres. R clears steering/visual history and snaps the camera. New chase presentation changes were source-reviewed only: no usable execution environment was available for compilation or runtime verification in this pass.
