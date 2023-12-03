using AdventOfCode2023.Utilities;

namespace AdventOfCode2023;

public class GearRatios : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var rectSize = new Point(data[0].Length, data.Length);
        var middle = rectSize / 2;

        var treeBounds = new Bounds(middle, middle + new Point(2,2));
        var tree = new QuadTree(treeBounds, 20, 20);
        var symbolSet = new HashSet<Point>();
        for(var i = 0; i < data.Length; i++)
            ExtractData(data[i], tree, i, symbolSet);

        var toSum = new HashSet<Number>();
        var collisions = new HashSet<ISpatialObject>();
        var gearRatio = 0;
        foreach(var symbol in symbolSet)
        {
            foreach(var dir in _directions)
                tree.GetCollisions(symbol + dir, collisions);

            var ratio = 1;
            foreach(var collision in collisions)
            {
                
                if (collision is Number num)
                {
                    toSum.Add(num);
                    if (collisions.Count == 2)
                        ratio *= num.Value;
                }
            }

            if (collisions.Count == 2)
                gearRatio += ratio;

            collisions.Clear();
        }

        var sum = toSum.Sum(x => x.Value);
        return $"Part Sum: {sum}, Gear Ratio: {gearRatio}";
    }

    private static void ExtractData(string line, QuadTree toInsert, int row, HashSet<Point> symbols)
    {
        for(var i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                var start = i;
                while (i < line.Length && char.IsDigit(line[i]))
                    i++;

                var length = i - start;
                var intString = line.Substring(start, length);
                var value = int.Parse(intString);

                var bounds = new Bounds(new Point(start, row), new Point(length - 1, 0),true);
                var number = new Number(value, bounds);
                toInsert.TryInsert(number);
                i--;
            }
            else if (line[i] != '.')
            {
                symbols.Add(new Point(i, row));
            }
        }
    }

    private static readonly Point[] _directions = [
        new Point(-1, -1), new Point(0, -1), new Point(1, -1),
        new Point(-1, 0), new Point(0,0), new Point(1,0),
        new Point(-1, 1), new Point(0, 1), new Point(1,1)
    ];
    private record Number(int Value, Bounds Bounds) : ISpatialObject;
    private record Symbol(Bounds Bounds) : ISpatialObject;
}
