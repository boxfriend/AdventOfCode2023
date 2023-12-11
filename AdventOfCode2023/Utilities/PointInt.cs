using System.Numerics;

namespace AdventOfCode2023.Utilities;

public record struct PointInt (int X, int Y) : IAdditionOperators<PointInt, PointInt, PointInt>
{
    public static PointInt operator + (PointInt left, PointInt right) =>
        new(left.X + right.X, left.Y + right.Y);

    public static PointInt operator - (PointInt left, PointInt right) =>
        new(left.X - right.X, left.Y - right.Y);

    public static PointInt MoveTowardsOneStep(PointInt origin, PointInt destination)
    {
        if (origin == destination)
            return origin;

        var diff = destination - origin;

        if(diff.X > diff.Y)
        {
            origin += new PointInt(Math.Sign(diff.X), 0);
        }
        else
        {
            origin += new PointInt(0, Math.Sign(diff.Y));
        }

        return origin;
    }
}
