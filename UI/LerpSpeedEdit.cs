using Godot;
using WorldTransformation.Algo;

namespace WorldTransformation.UI;

[GlobalClass]
internal sealed partial class LerpSpeedEdit : LineEdit
{
  [Export] private Transformer? _transformer;
  
  public override void _Ready()
    => TextSubmitted += EditLerpSpeed;

  private void EditLerpSpeed(string newText)
  {
    if (_transformer is null)
      return;
    
    if (!float.TryParse(newText, out float num))
      return;

    _transformer.LerpSpeed = num;

    Clear();
  }
}
