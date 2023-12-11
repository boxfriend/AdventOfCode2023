using AdventOfCode2023.Utilities;

namespace AdventOfCode2023;
internal partial class PipeMaze : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var pipes = new Pipe?[data[0].Length, data.Length];
        var start = new PointInt(-1, -1);
        for(var i = 0; i < data.Length; i++)
        {
            for(var j = 0; j < data[i].Length; j++)
            {
                var pos = new PointInt(j, i);
                pipes[j,i] = Pipe.FromChar(data[i][j], pos);

                if (data[i][j] == 'S')
                    start = pos;
            }
        }

        var toProcess = new Queue<Pipe>();
        for(var i = -1; i < 2; i++)
        {
            for(var j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                var newPos = start + new PointInt(j, i);
                if (newPos.X < 0 || newPos.Y < 0)
                    continue;

                if (newPos.X > data[0].Length || newPos.Y > data.Length)
                    continue;

                var pipe = pipes[newPos.X, newPos.Y];
                if (pipe is null) continue;

                if (pipe.Position + pipe.DirA != start && pipe.Position + pipe.DirB != start)
                    continue;

                pipe.Distance = 1;
                toProcess.Enqueue(pipe);
            }
        }

        var processed = new HashSet<Pipe>();
        while (toProcess.Count > 0)
        {
            var pipe = toProcess.Dequeue();
            var distance = pipe.Distance + 1;

            var first = pipe.Position + pipe.DirA;
            var firstPipe = pipes[first.X, first.Y];
            if(firstPipe is not null)
            {
                if (processed.Contains(firstPipe))
                {
                    if (firstPipe.Distance > distance)
                    {
                        processed.Remove(firstPipe);
                        toProcess.Enqueue(firstPipe);
                    }
                } else
                    toProcess.Enqueue(firstPipe);
                firstPipe.Distance = Math.Min(distance, firstPipe.Distance);
            }

            var second = pipe.Position + pipe.DirB;
            var secondPipe = pipes[second.X, second.Y];

            if (secondPipe is not null)
            {
                if (processed.Contains(secondPipe))
                {
                    if (secondPipe.Distance > distance)
                    {
                        processed.Remove(secondPipe);
                        toProcess.Enqueue(secondPipe);
                    }
                } else
                    toProcess.Enqueue(secondPipe);
                secondPipe.Distance = Math.Min(distance, secondPipe.Distance);
            }

            processed.Add(pipe);
        }

        var furthest = processed.Max(x => x.Distance);

        return furthest.ToString();
    }

    private class Pipe
    {
        public PointInt Position { get; init; }
        public PointInt DirA { get; init; }
        public PointInt DirB { get; init; }

        public int Distance { get; set; } = int.MaxValue;

        public static Pipe? FromChar (char c, PointInt position) => c switch
        {
            '|' => new() { Position = position, DirA = new(0, 1), DirB = new(0, -1) },
            '-' => new() { Position = position, DirA = new(-1, 0), DirB = new(1, 0) },
            'L' => new() { Position = position, DirA = new(1, 0), DirB = new(0, -1) },
            'J' => new() { Position = position, DirA = new(-1, 0), DirB = new(0, -1) },
            '7' => new() { Position = position, DirA = new(-1, 0), DirB = new(0, 1) },
            'F' => new() { Position = position, DirA = new(1, 0), DirB = new(0, 1) },
            _ => default
        };
    }
}
