using System.Text;

namespace AdventOfCode2023;
internal class WaitForIt : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var times = new List<long>();
        ExtractNumbers(data[0].Trim().Split(':')[1], times);
        var distances = new List<long>();
        ExtractNumbers(data[1].Trim().Split(':')[1], distances);

        if (times.Count != distances.Count)
            throw new InvalidOperationException("You done parsed wrong");

        var total = 1;
        for(var i = 0; i < times.Count; i++)
        {
            total *= RaceWins(times[i], distances[i]);
        }

        return total.ToString();
    }

    private static int RaceWins(long time, long distance)
    {
        var wins = 0;
        for(var i = 1; i < time; i++)
        {
            if (i * (time - i) > distance)
                wins++;
        }
        return wins;
    }

    private static void ExtractNumbers(string line, List<long> numbers)
    {
        var builder = new StringBuilder();
        for(var i = 0; i < line.Length; i++)
        {
            if (!char.IsNumber(line[i]))
                continue;

            builder.Append(line[i]);
            /*var start = i;
            while (i < line.Length && char.IsDigit(line[i]))
                i++;

            var num = line[start..i];
            numbers.Add(int.Parse(num));*/
        }
        numbers.Add(long.Parse(builder.ToString()));
    }
}
