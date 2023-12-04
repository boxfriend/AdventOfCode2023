using AdventOfCode2023;

string? path = null;
while(string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
{
    Console.WriteLine("Enter the path to your input directory: ");
    path = Console.ReadLine();
}

ProcessSolution<Trebuchet>("Day One: Trebuchet", Path.Combine(path, "DayOne.txt"));
ProcessSolution<CubeConundrum>("Day Two: Cube Conundrum", Path.Combine(path, "DayTwo.txt"));
ProcessSolution<GearRatios>("Day Three: Gear Ratios", Path.Combine(path, "DayThree.txt"));



static async void ProcessSolution<T>(string message, string path) where T : IAdventSolution
{
    Console.WriteLine(message);
    var input = await File.ReadAllLinesAsync(path);
    var solution = T.Evaluate(input);
    Console.WriteLine(solution);
}