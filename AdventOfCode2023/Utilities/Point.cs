using System.Numerics;

namespace AdventOfCode2023.Utilities;

public record struct Point (float X, float Y) : IAdditionOperators<Point, Point, Point>,
    ISubtractionOperators<Point,Point,Point>, IDivisionOperators<Point, int, Point>,
    IMultiplyOperators<Point,Point,Point>, IMultiplyOperators<Point,int,Point>, 
    IDivisionOperators<Point, float, Point>, IMultiplyOperators<Point, float, Point>
{
    public static Point operator + (Point left, Point right) => new(left.X + right.X, left.Y + right.Y);

    public static Point operator - (Point left, Point right) => new(left.X - right.X, left.Y - right.Y);

    public static Point operator * (Point left, Point right) => new(left.X * right.X, left.Y * right.Y);

    public static Point operator * (Point left, int right) => new(left.X * right, left.Y * right);
    public static Point operator * (Point left, float right) => new(left.X * right, left.Y * right);

    public static Point operator / (Point left, int right) => new(left.X / right, left.Y / right);
    public static Point operator / (Point left, float right) => new(left.X / right, left.Y / right);
}