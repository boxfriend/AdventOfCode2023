namespace AdventOfCode2023;
internal class SeedFertilizer : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var seeds = ExtractSeeds(data[0]);

        //Start at index 2 because we already dealt with 0 and we know 1 will be whitespace
        for(var i = 2; i < data.Length; i++)
        {
            if (!string.IsNullOrEmpty(data[i]) && char.IsLetter(data[i][0]))
            {
                var start = ++i;
                while (i < data.Length && !string.IsNullOrEmpty(data[i]) && char.IsDigit(data[i][0])) i++;

                var map = ExtractRanges(data[start..i]);
                TransformSeeds(seeds, map);
            }
        }

        return $"Lowest location: {seeds.Min()}";
    }

    private static List<long> ExtractSeeds(string line)
    {
        var data = line["seeds: ".Length..].Split(' ');
        var seeds = new List<long>();
        foreach(var seed in data)
        {
            if(string.IsNullOrWhiteSpace(seed)) continue;

            if (long.TryParse(seed, out var seedValue))
                seeds.Add(seedValue);
            else
                throw new FormatException();
        }

        return seeds;
    }

    private static void TransformSeeds(List<long> seeds, List<Range> map)
    {
        for(var i = 0; i < seeds.Count; i++)
        {
            foreach(var range in map)
            {
                if (range.IsSourceInRange(seeds[i]))
                {
                    seeds[i] = range.GetDestination(seeds[i]);
                    break;
                }
            }
        }
    }

    private static List<Range> ExtractRanges (string[] data)
    {
        var map = new List<Range>();
        foreach(var line in data)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var numbers = line.Trim().Split(' ');
            if (numbers.Length != 3)
                throw new FormatException($"Line has an invalid format '{line}'");

            map.Add(new()
            {
                Destination = long.Parse(numbers[0]),
                Source = long.Parse(numbers[1]),
                Length = long.Parse(numbers[2])
            });
        }

        return map;
    }

    private struct Range
    {
        public long Destination { get; init; }
        public long Source { get; init; }
        public long Length { get; init; }

        public readonly bool IsSourceInRange (long source)
        {
            var len = source - Source;
            if (len < 0)
                return false;

            return len < Length;
        }

        public readonly long GetDestination (long source) => (source - Source) + Destination;
    }
}
