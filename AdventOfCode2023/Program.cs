using AdventOfCode2023;

string? path = null;
while(string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
{
    Console.WriteLine("Enter the path to your input directory: ");
    path = Console.ReadLine();
}

var dayOneInput = await File.ReadAllLinesAsync(Path.Combine(path, "DayOne.txt"));
var dayOneSolution = Trebuchet.Evaluate(dayOneInput);
Console.WriteLine(dayOneSolution);
