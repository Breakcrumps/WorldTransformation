using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using WorldTransformation.Types;

namespace WorldTransformation.Algo;

[GlobalClass]
internal sealed partial class Transformer : Node
{
  [Export] private Timer? _timer;
  [Export] private Node? _susceptibleGroup;

  [ExportGroup("Parameters")]
  [Export] internal float LerpSpeed { private get; set; } = 2f;

  private bool _canTransform;

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

    _timer.Timeout += ExecuteTransform;
  }

  internal void ExecuteTransform()
    => _wantedPositions = [.. _wantedPositions.Select(Transform.Apply)];

  public override void _PhysicsProcess(double delta)
  {
    for (int i = 0; i < _bodies.Count; i++)
    {
      _bodies[i].GlobalPosition = _bodies[i].GlobalPosition.Lerp(_wantedPositions[i], LerpSpeed * (float)delta);
    }
  }

  internal void EnableTransform()
    => _timer?.Start();

  internal void DisableTransform()
    => _timer?.Stop();
}
