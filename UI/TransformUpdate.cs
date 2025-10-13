using Godot;
using WorldTransformation.Algo;

namespace WorldTransformation.UI;

[GlobalClass]
internal sealed partial class TransformUpdate : CheckButton
{
  [Export] private Transformer? _transformer;

  public override void _Toggled(bool toggledOn)
  {
    if (toggledOn)
      _transformer?.EnableTransform();
    else
      _transformer?.DisableTransform();
  }
}
