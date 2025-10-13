using Godot;

namespace WorldTransformation.UI;

[GlobalClass]
internal sealed partial class Ui : Control
{
  public override void _UnhandledInput(InputEvent @event)
  {
    if (!@event.IsActionPressed("Pause"))
      return;

    Input.MouseMode ^= Input.MouseModeEnum.Captured;
    GetTree().Paused = Input.MouseMode != Input.MouseModeEnum.Captured;
  }
}
