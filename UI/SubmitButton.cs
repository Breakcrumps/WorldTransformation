using Godot;
using WorldTransformation.Algo;
using WorldTransformation.Types;

namespace WorldTransformation.UI;

[GlobalClass]
internal sealed partial class SubmitButton : Button
{
  [Export] private TextEdit? _textEdit;
  [Export] private Transformer? _transformer;
  
  public override void _Ready()
    => Pressed += SubmitMatrix;

  internal void SubmitMatrix()
  {
    if (_textEdit is null || _transformer is null)
      return;

    string[] elems = _textEdit.Text.StripEdges().Replace('\n', ' ').Split(' ');

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

    _textEdit.Clear();
    _textEdit.ReleaseFocus();
  }
}
