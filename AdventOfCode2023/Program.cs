﻿using AdventOfCode2023;

string? path = null;
while(string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
{
    Console.WriteLine("Enter the path to your input directory: ");
    path = Console.ReadLine();
}

await ProcessSolution<Trebuchet>("Day One: Trebuchet", Path.Combine(path, "DayOne.txt"));
await ProcessSolution<CubeConundrum>("Day Two: Cube Conundrum", Path.Combine(path, "DayTwo.txt"));
await ProcessSolution<GearRatios>("Day Three: Gear Ratios", Path.Combine(path, "DayThree.txt"));
await ProcessSolution<ScratchCards>("Day Four: Scratchcards", Path.Combine(path, "DayFour.txt"));



static async Task ProcessSolution<T>(string message, string path) where T : IAdventSolution
{
    Console.WriteLine(message);
    var input = await File.ReadAllLinesAsync(path);
    var solution = T.Evaluate(input);
    Console.WriteLine(solution);
    Console.WriteLine();
}