namespace AdventOfCode2023;
internal class Trebuchet : IAdventSolution
{
    private static readonly string[] _digits = ["one", "two", "three", "four", 
        "five", "six", "seven", "eight", "nine"];


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
            var index = fromEnd ? line.Length - i - 1 : i;
            var ch = line[index];
            if (char.IsDigit(ch))
                return ch - '0';

            if(_digits.Any(x => x.StartsWith(ch)))
            {
                if (line.Length - index < 3)
                    continue;

                //we know the shortest digit string is 3 characters and the longest is 5
                //so we can loop just 3 times adding the loop index to 3
                for(var j = 0; j < 3; j++)
                {
                    var subLength = 3 + j;
                    if (subLength + index > line.Length)
                        continue;

                    var str = line.Substring(index, subLength);
                    var strIndex = Array.IndexOf(_digits, str);
                    if (strIndex >= 0)
                        return strIndex + 1;
                }
            }
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
