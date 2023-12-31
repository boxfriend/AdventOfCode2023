﻿namespace AdventOfCode2023;

public class CubeConundrum : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var sumPossible = 0;
        var sumPowers = 0L;
        foreach(var game in data)
        {
            var info = game.Split(':');
            var gameNumber = ExtractGameNumber(info[0]);
            var counts = new CubeCount { R = 12, G = 13, B = 14 };
            if (IsGamePossible(info[1], counts))
                sumPossible += gameNumber;

            sumPowers += GetPower(info[1]);
        }

        return $"Sum of Possible Games: {sumPossible}\nSum of Game Powers: {sumPowers}";
    }

    private static int GetPower(string game)
    {
        var gameData = game.Split(';');
        var maxCount = new CubeCount();

        for(var i = 0; i < gameData.Length; i++)
        {
            var count = ExtractGameData(gameData[i]);
            maxCount = CubeCount.GetMaxValues(maxCount, count);
        }

        return maxCount.R * maxCount.G * maxCount.B;
    }

    private static int ExtractGameNumber(string game)
    {
        var number = 0;
        for(var i = 0; i < game.Length; i++)
        {
            if (char.IsDigit(game[i]))
            {
                number *= 10;
                number += game[i] - '0';
            }
        }
        return number;
    }

    private static bool IsGamePossible(string game, CubeCount counts)
    {
        var gameData = game.Split(';');

        foreach(var g in gameData)
        {
            var data = ExtractGameData(g);
            if (!counts.CanContain(data))
                return false;
        }
        return true;
    }

    private static CubeCount ExtractGameData(string game)
    {
        var count = new CubeCount();
        var data = game.Split(',');
        foreach(var d in data)
        {
            var gData = d.Trim().Split(' ');
            var number = int.Parse(gData[0]);
            var index = Array.IndexOf(_colors, gData[1]);
            count[index] += number;
        }
        return count;
    }

    private static readonly string[] _colors = ["red", "green", "blue"];
    private struct CubeCount
    {
        public int this[int i]
        {
            readonly get => i switch
            {
                0 => R,
                1 => G,
                2 => B,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (i)
                {
                    case 0: R = value; break;
                    case 1: G = value; break;
                    case 2: B = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public readonly bool CanContain(CubeCount other) => R >= other.R && B >= other.B && G >= other.G;

        public static CubeCount GetMaxValues (CubeCount left, CubeCount right) => new()
        {
            R = Math.Max(left.R, right.R),
            G = Math.Max(left.G, right.G),
            B = Math.Max(left.B, right.B)
        };
    }
}