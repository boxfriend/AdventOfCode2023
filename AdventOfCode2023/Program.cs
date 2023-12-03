using AdventOfCode2023;

string? path = null;
while(string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
{
    Console.WriteLine("Enter the path to your input directory: ");
    path = Console.ReadLine();
}

Console.WriteLine("Day One: Trebuchet");
var dayOneInput = await File.ReadAllLinesAsync(Path.Combine(path, "DayOne.txt"));
var dayOneSolution = Trebuchet.Evaluate(dayOneInput);
Console.WriteLine(dayOneSolution);

Console.WriteLine("Day Two: Cube Conundrum");
var dayTwoInput = await File.ReadAllLinesAsync(Path.Combine(path, "DayTwo.txt"));
var dayTwoSolution = CubeConundrum.Evaluate(dayTwoInput);
Console.WriteLine(dayTwoSolution);

Console.WriteLine("Day Three: Gear Ratios");
var dayThreeInput = await File.ReadAllLinesAsync(Path.Combine(path, "DayThree.txt"));
var dayThreeSolution = GearRatios.Evaluate(dayThreeInput);
Console.WriteLine(dayThreeSolution);
