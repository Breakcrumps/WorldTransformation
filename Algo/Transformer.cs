using System.Collections.Generic;
using System.Linq;
using Godot;
using WorldTransformation.Types;

namespace WorldTransformation;

[GlobalClass]
internal sealed partial class Transformer : Node
{
  [Export] private Timer? _timer;
  [Export] private Node? _susceptibleGroup;

  [ExportGroup("Parameters")]
  [Export] private float _lerpSpeed = 2f;

  private readonly List<StaticBody3D> _bodies = [];
  private List<Vector3> _wantedPositions = [];

  internal Matrix Transform { private get; set; } = Matrix.Identity;

  public override void _Ready()
  {
    if (_susceptibleGroup is null)
      return;

    if (_timer is null)
      return;
    
    foreach (Node node in _susceptibleGroup.GetChildren())
    {
      if (node is StaticBody3D staticBody)
      {
        _bodies.Add(staticBody);
        _wantedPositions.Add(staticBody.GlobalPosition);
      }
    }

    _timer.Timeout += () => _wantedPositions = [.. _wantedPositions.Select(v => Transform.Apply(v))];
  }

  public override void _PhysicsProcess(double delta)
  {
    for (int i = 0; i < _bodies.Count; i++)
    {
      _bodies[i].GlobalPosition = _bodies[i].GlobalPosition.Lerp(_wantedPositions[i], _lerpSpeed * (float)delta);
    }
  }
}
