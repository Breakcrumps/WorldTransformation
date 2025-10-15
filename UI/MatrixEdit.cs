using Godot;
using WorldTransformation.UI;

[GlobalClass]
internal sealed partial class MatrixEdit : TextEdit
{
  [Export] private SubmitButton? _submitButton;
  
  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event is not InputEventKey eventKey)
      return;

    if (eventKey.IsActionPressed("MatrixEdit"))
      GrabFocus();

    if (!HasFocus())
      return;

    if (eventKey.Keycode == Key.Enter && eventKey.ShiftPressed)
      _submitButton?.SubmitMatrix();
  }
}
