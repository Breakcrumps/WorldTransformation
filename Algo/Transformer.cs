using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using WorldTransformation.Types;

namespace WorldTransformation.Algo;

[GlobalClass]
internal sealed partial class Transformer : Node
{
  [Export] private bool _functional;
  
  [Export] private Timer? _timer;
  [Export] private SusceptibleGroup? _susceptibleGroup;

  [ExportGroup("Parameters")]
  [Export] internal float LerpSpeed { private get; set; } = 2f;

  private bool _canTransform;

  private List<Vector3> _wantedPositions = [];

  internal Matrix Transform { private get; set; } = Matrix.Identity;
  internal FunctionMatrix FunctionTransform { private get; set; } = new(
    new FunctionVector(x => -.5f + .001f * x, y => .1f - .07f * y, z => -.07f * .01f * z),
    new FunctionVector(x => .05f + .09f * x, y => .05f + .01f * y, z => .7f + .01f * z),
    new FunctionVector(x => .01f * x, y => .09f + .04f * y, z => .01f * z)
  );
  // internal FunctionMatrix FunctionTransform { private get; set; } = new(
  //   new FunctionVector(x => .07f * x, y => .1f * Mathf.Sin(y), z => .5f),
  //   new FunctionVector(x => .001f / x, y => .05f * Mathf.Cos(y), z => 0),
  //   new FunctionVector(x => .0001f + .01f * x, y => -.5f * Mathf.Cosh(y), z => 1)
  // );

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
        _wantedPositions.Add(staticBody.GlobalPosition);
      }
    }

    if (_functional)
      _timer.Timeout += ExecuteFunctionalTransform;
    else
      _timer.Timeout += ExecuteTransform;
  }

  internal void ExecuteTransform()
    => _wantedPositions = [.. _wantedPositions.Select(Transform.Apply)];

  internal void ExecuteFunctionalTransform()
  {
    for (int i = 0; i < _wantedPositions.Count; i++)
    {
      Vector3 pos = _susceptibleGroup!.Bodies[i].GlobalPosition;

      if (pos.X == 0)
        continue;

      _wantedPositions[i] = FunctionTransform.Apply(_wantedPositions[i], pos.X * .2f, pos.Y * .2f, pos.Z * .2f);
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    if (_susceptibleGroup is null)
      return;
    
    for (int i = 0; i < _susceptibleGroup.Bodies.Count; i++)
    {
      _susceptibleGroup.Bodies[i].GlobalPosition = _susceptibleGroup.Bodies[i].GlobalPosition.Lerp(_wantedPositions[i], LerpSpeed * (float)delta);
    }
  }

  internal void EnableTransform()
    => _timer?.Start();

  internal void DisableTransform()
    => _timer?.Stop();

  internal static float RandomCoord()
    => Random.Shared.NextSingle() * 30f - 15f;
}
