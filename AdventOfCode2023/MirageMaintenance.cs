namespace AdventOfCode2023;
internal class MirageMaintenance : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var numbers = new List<long>();
        var sum = 0L;
        var beginningSum = 0L;
        foreach(var line in data)
        {
            numbers.Clear();
            var values = line.Trim().Split(' ');
            foreach(var v in values)
                numbers.Add(long.Parse(v));

            sum += Extrapolate(numbers, true);
            beginningSum += Extrapolate(numbers, false);
        }

        return $"End Numbers history: {sum} \nBeginning Numbers History: {beginningSum}";
    }

    private static long Extrapolate(List<long> numbers, bool fromEnd)
    {
        if (numbers.All(x => x == 0))
            return 0;

        var nums = new List<long>();
        for(var i = 1; i < numbers.Count; i++)
        {
            nums.Add(numbers[i] - numbers[i - 1]);
        }

        if (fromEnd)
            return numbers[^1] + Extrapolate(nums, fromEnd);
        else
            return numbers[0] - Extrapolate(nums, fromEnd);
    }
}
