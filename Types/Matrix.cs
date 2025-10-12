using Godot;

namespace WorldTransformation.Types;

internal sealed record Matrix
{
  internal Vector3 X;
  internal Vector3 Y;
  internal Vector3 Z;

  internal Vector3 Apply(Vector3 vector)
    => vector[0] * X + vector[1] * Y + vector[2] * Z;

  internal Vector3 LerpApply(Vector3 vector, float weight)
    => vector.Lerp(Apply(vector), weight);

  internal static Matrix FromArray(float[] ints) => new()
  {
    X = new Vector3(ints[0], ints[1], ints[2]),
    Y = new Vector3(ints[3], ints[4], ints[5]),
    Z = new Vector3(ints[6], ints[7], ints[8])
  };

  internal static Matrix Identity => new()
  {
    X = new(1f, 0f, 0f),
    Y = new(0f, 1f, 0f),
    Z = new(0f, 0f, 1f)
  };
}
