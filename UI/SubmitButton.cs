using Godot;
using WorldTransformation;
using WorldTransformation.Types;

[GlobalClass]
internal sealed partial class SubmitButton : Button
{
  [Export] private TextEdit? _textEdit;
  [Export] private Transformer? _transformer;
  
  public override void _Ready()
    => Pressed += SubmitMatrix;

  private void SubmitMatrix()
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

    _textEdit.Clear();
  }
}
