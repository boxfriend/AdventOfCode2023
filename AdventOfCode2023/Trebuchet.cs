namespace AdventOfCode2023;
internal class Trebuchet : IAdventSolution
{
    
    private static int GetNumberFromLine (string line)
    {
        var num = 0;
        num += GetFirstDigit(line, false) * 10;
        num += GetFirstDigit(line, true);

        return num;
    }

    private static int GetFirstDigit (string line, bool fromEnd)
    {
        for (var i = 0; i < line.Length; i++)
        {
            var ch = line[fromEnd ? ^(i + 1) : i];
            if (char.IsDigit(ch))
                return ch - '0';
        }
        return 0;
    }

    public static string Evaluate (string[] data)
    {
        var sum = 0;
        foreach (var line in data)
        {
            sum += GetNumberFromLine(line);
        }
        return sum.ToString();
    }
}
