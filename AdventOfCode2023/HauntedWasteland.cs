namespace AdventOfCode2023;
internal class HauntedWasteland : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var locations = new Dictionary<string, Directions>();
        for(var i = 2; i < data.Length; i++)
        {
            var node = data[i][..3];
            var dir = GetDirections(data[i][6..]);
            locations.Add(node, dir);
        }

        var end = "ZZZ";
        var current = "AAA";

        var directions = data[0];
        var steps = 0;
        while(true)
        {
            var dir = directions[steps++ % directions.Length];
            var node = locations[current];
            if (dir == 'L')
                current = node.Left;
            else
                current = node.Right;

            if (current == end)
                break;
        }

        return steps.ToString();
    }

    private static Directions GetDirections (string line) => new(line[1..4], line[6..9]);
    private record struct Directions(string Left, string Right);
}
