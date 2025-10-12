using Godot;

namespace WorldTransformation;

[GlobalClass]
internal sealed partial class MovingCamera : Camera3D
{
  [Export] private float _moveSpeed = 25f;
  [Export] private float _mouseSensitivity = .005f;
  [Export] private float _tiltLimit = 70f;

  public override void _Ready()
  {
    Input.MouseMode = Input.MouseModeEnum.Captured;
    
    _tiltLimit *= Mathf.Pi / 180f;
  }

  public override void _UnhandledInput(InputEvent @event)
    => HandleMouseMovement(@event);

  private void HandleMouseMovement(InputEvent @event)
  {
    if (@event is not InputEventMouseMotion mouseMotion)
      return;

    Vector2 relative = mouseMotion.Relative;

    Vector3 newRotation = GlobalRotation;

    newRotation.X -= relative.Y * _mouseSensitivity;
    newRotation.X = Mathf.Clamp(value: newRotation.X, min: -_tiltLimit, max: _tiltLimit);
    newRotation.Y -= relative.X * _mouseSensitivity;

    GlobalRotation = newRotation;
  }

  public override void _PhysicsProcess(double delta)
  {
    float forward = Input.GetAxis("Down", "Up");
    float right = Input.GetAxis("Left", "Right");

    Vector3 velocity = (-GlobalBasis.Z * forward + GlobalBasis.X * right).Normalized() * _moveSpeed;

    GlobalPosition += velocity * (float)delta;
  }
}
