using System;
using System.Collections.Generic;
using Godot;

[GlobalClass]
internal sealed partial class SusceptibleGroup : Node
{
  [Export] private PackedScene? _chairGlb;
  [Export] private int _period = 8;
  [Export] private int _periods = 8;

  internal List<StaticBody3D> Bodies = [];

  public override void _EnterTree()
  {
    if (_chairGlb is null)
      return;

    for (int x = -_periods * _period; x < _periods * _period; x += _period)
    {
      for (int y = -_periods * _period; y < _periods * _period; y += _period)
      {
        for (int z = -_periods * _period; z < _periods * _period; z += _period)
        {
          StaticBody3D chair = _chairGlb.Instantiate<StaticBody3D>();

          AddChild(chair);

          chair.GlobalPosition = new Vector3(x, y, z);
          chair.GlobalRotation = new Vector3(RandomRadian(), RandomRadian(), RandomRadian());

          Bodies.Add(chair);
        }
      }
    }
  }

  private static float RandomRadian()
    => Random.Shared.NextSingle() * 2f * Mathf.Pi - Mathf.Pi;
}
