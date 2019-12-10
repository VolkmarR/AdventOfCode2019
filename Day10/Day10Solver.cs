using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;
using System.Diagnostics;

namespace Day10
{
    public class Day10Tests
    {
        private readonly ITestOutputHelper Output;
        public Day10Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day10Solver().ExecutePuzzle1());

        [Fact]
        public void RunStep2() => Output.WriteLine(new Day10Solver().ExecutePuzzle2());
    }

    public class Day10Solver : SolverBase
    {
        bool[,] InitMatrix(List<string> data)
        {
            var result = new bool[data.First().Length, data.Count];
            for (int y = 0; y < data.Count; y++)
            {
                var line = data[y];
                for (int x = 0; x < line.Length; x++)
                    result[x, y] = line[x] == '#';
            }

            return result;
        }

        IEnumerable<(int x, int y)> EachCell(bool[,] matrix)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                    yield return (x, y);
            }
        }

        IEnumerable<(int x, int y)> EachAsteroid(bool[,] matrix)
            => EachCell(matrix).Where(q => matrix[q.x, q.y]);

        decimal Tan((int x, int y) start, (int x, int y) end)
        {
            if (end.x - start.x == 0)
                return 0;

            return (decimal)Math.Round((end.y - start.y) / (decimal)(end.x - start.x), 6, MidpointRounding.AwayFromZero);
        }

        bool IsInBox((int x, int y) start, (int x, int y) end, (int x, int y) pos)
        {
            var topLeft = start;
            var bottomRight = end;
            if (topLeft.x > bottomRight.x)
            {
                topLeft.x = bottomRight.x;
                bottomRight.x = start.x;
            }

            if (topLeft.y > bottomRight.y)
            {
                topLeft.y = bottomRight.y;
                bottomRight.y = start.y;
            }

            return topLeft.x <= pos.x && pos.x <= bottomRight.x && topLeft.y <= pos.y && pos.y <= bottomRight.y;
        }

        int CountVisibleAsteroids(List<(int x, int y)> asteroids, (int x, int y) pos)
        {
            var count = 0;
            foreach (var pos2 in asteroids.Where(q => q != pos))
            {
                var tan = Tan(pos, pos2);
                if (!asteroids.Any(q => q != pos && q != pos2 && IsInBox(pos, pos2, q) && Tan(pos, q) == tan))
                    count++;
            }

            return count;
        }

        protected override string Solve1(List<string> data)
        {
            var count = new Dictionary<(int x, int y), int>();
            var asteroids = EachAsteroid(InitMatrix(data)).ToList();
            foreach (var pos in asteroids)
                count[pos] = CountVisibleAsteroids(asteroids, pos);

            return count.OrderByDescending(q => q.Value).Select(q => $"{q.Key.x},{q.Key.y} = {q.Value}").First();
        }

        protected override string Solve2(List<string> data)
        {
            var asteroids = EachAsteroid(InitMatrix(data)).ToList();
            (int x, int y) monitor = (30, 34);





            return "??";
        }

    }
}
