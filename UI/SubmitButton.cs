using Godot;

namespace WorldTransformation.UI;

[GlobalClass]
internal sealed partial class SubmitButton : Button
{
  [Export] private MatrixEdit? _matrixEdit;

  public override void _Ready()
  {
    if (_matrixEdit is not null)
      Pressed += _matrixEdit.SubmitMatrix;
  }
}
