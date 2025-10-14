using System;
using Godot;

namespace WorldTransformation.Types;

internal sealed class FunctionVector
{
  internal Func<float, float> X;
  internal Func<float, float> Y;
  internal Func<float, float> Z;

  internal FunctionVector(
    Func<float, float> x,
    Func<float, float> y,
    Func<float, float> z
  )
  {
    X = x;
    Y = y;
    Z = z;
  }

  internal Vector3 Compute(float x, float y, float z)
    => new(X(x), Y(y), Z(z));
}