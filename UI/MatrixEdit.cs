using Godot;
using WorldTransformation.Algo;
using WorldTransformation.Types;

[GlobalClass]
internal sealed partial class MatrixEdit : TextEdit
{
  [Export] private Transformer? _transformer;

  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event is not InputEventKey eventKey)
      return;

    if (eventKey.IsActionPressed("MatrixEdit"))
    {
      if (!HasFocus())
        GrabFocus();
      else
        ReleaseFocus();
    }

    if (!HasFocus())
      return;

    if (eventKey.Keycode == Key.Enter && eventKey.ShiftPressed)
      SubmitMatrix();
  }
  
  internal void SubmitMatrix()
  {
    if (_transformer is null)
      return;
    
    string[] elems = Text.StripEdges().Replace('\n', ' ').Split(' ');

    if (elems.Length != 9)
      return;

    float[] nums = new float[9];

    for (int i = 0; i < 9; i++)
    {
      if (!float.TryParse(elems[i], out float num))
        return;

      nums[i] = num;
    }

    _transformer.Transform = Matrix.FromArray(nums);
    _transformer.ExecuteTransform();

    Clear();
    ReleaseFocus();
  }
}
