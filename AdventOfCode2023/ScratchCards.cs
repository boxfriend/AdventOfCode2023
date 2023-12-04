namespace AdventOfCode2023;
internal class ScratchCards : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var pointsSum = 0;
        var winningSet = new HashSet<string>();
        var numberSet = new HashSet<string>();
        foreach(var line in data)
        {
            winningSet.Clear();
            numberSet.Clear();

            ExtractCardLists(line, winningSet, numberSet);
            pointsSum += GetPoints(winningSet, numberSet);
        }

        return pointsSum.ToString();
    }
    private static int GetPoints(HashSet<string> winningSet, HashSet<string> numberSet)
    {
        winningSet.IntersectWith(numberSet);
        var count = winningSet.Count;
        if(count <= 1)
            return count;

        var total = 1;
        for(var i = count; i > 1; i--)
        {
            total *= 2;
        }

        return total;
    }
    private static void ExtractCardLists(string line, HashSet<string> winning, HashSet<string> numbers)
    {
        var parts = line.Split(':');
        var allNumbers = parts[1].Split('|');

        var allWinning = allNumbers[0].Trim().Split(' ');
        var allRevealed = allNumbers[1].Trim().Split(' ');

        foreach (var str in allWinning)
        {
            if (string.IsNullOrWhiteSpace(str))
                continue;

            if (!winning.Add(str))
                throw new InvalidOperationException("Winning numbers has duplicates");

        }

        foreach (var str in allRevealed)
        {
            if (string.IsNullOrWhiteSpace(str))
                continue;

            if (!numbers.Add(str))
                throw new InvalidOperationException("Revealed numbers has duplicates");
        }

    }
}
