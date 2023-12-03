namespace AdventOfCode2023.Utilities;

internal readonly struct Bounds (Point center, Point halfExtents)
{
    public Point HalfExtents { get; init; } = halfExtents;
    public Point Center { get; init; } = center;

    public Point Min { get; init; } = center - halfExtents;
    public Point Max { get; init; } = center + halfExtents;

    public Bounds(Point start, Point extents, bool fromStart) : this(start + (extents/2f), extents / 2f)
    {
        Min = start;
        Max = start + extents;
    }

    public static bool AreIntersecting(Bounds a, Bounds b)
    {
        if (a.Min.X > b.Max.X || a.Max.X < b.Min.X)
            return false;
        if (a.Min.Y > b.Max.Y || a.Max.Y < b.Min.Y)
            return false;

        return true;
    }

    public bool FullyContained (Bounds other)
    {
        if (!Intersects(other))
            return false;

        if (Min.X > other.Min.X || Min.Y > other.Min.Y)
            return false;
        if (Max.X < other.Max.X || Max.Y < other.Max.Y)
            return false;

        return true;
    }

    public bool PointInside (Point point) => Min.X <= point.X && Min.Y <= point.Y
        && Max.X >= point.X && Max.Y >= point.Y;

    public bool Intersects (Bounds other) => AreIntersecting(this, other);
}

internal interface ISpatialObject
{
    public Bounds Bounds { get; }
}