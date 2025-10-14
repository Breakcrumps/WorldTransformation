using Godot;

namespace WorldTransformation.Types;

internal sealed class FunctionMatrix
{
  internal FunctionVector X;
  internal FunctionVector Y;
  internal FunctionVector Z;

  internal FunctionMatrix(
    FunctionVector x,
    FunctionVector y,
    FunctionVector z
  )
  {
    X = x;
    Y = y;
    Z = z;
  }

  internal Vector3 Apply(Vector3 vector, float x, float y, float z) => (
    X.Compute(x, y, z) * vector.X
    + Y.Compute(x, y, z) * vector.Y
    + Z.Compute(x, y, z) * vector.Z
  );
}