using Godot;
using WorldTransformation.UI;

[GlobalClass]
internal sealed partial class MatrixEdit : TextEdit
{
  [Export] private SubmitButton? _submitButton;
  
  public override void _Input(InputEvent @event)
  {
    if (@event is not InputEventKey eventKey)
      return;

    if (Key.Key0 <= eventKey.Keycode && eventKey.Keycode <= Key.Key9)
      GrabFocus();

    if (!HasFocus())
      return;

    if (eventKey.Keycode == Key.Enter && eventKey.ShiftPressed)
      _submitButton?.SubmitMatrix();
  }
}
