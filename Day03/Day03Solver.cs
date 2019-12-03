using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day03
{
    public class Day03Tests
    {
        private readonly ITestOutputHelper Output;
        public Day03Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day03Solver().ExecutePuzzle1());

        [Fact]
        public void RunStep2() => Output.WriteLine(new Day03Solver().ExecutePuzzle2());
    }

    public class Day03Solver : SolverBase
    {
        IEnumerable<(int x, int y)> WalkPath(string data)
        {
            var x = 0;
            var y = 0;
            var result = new Dictionary<int, HashSet<int>>();
            foreach (var item in data.Split(',').Select(q => new { Direction = q[0], Steps = int.Parse(q.Substring(1)) }))
            {
                var dX = item.Direction == 'R' ? 1 : item.Direction == 'L' ? -1 : 0;
                var dY = item.Direction == 'D' ? 1 : item.Direction == 'U' ? -1 : 0;
                for (int i = 1; i <= item.Steps; i++)
                {
                    x += dX;
                    y += dY;

                    yield return (x, y);
                }
            }
        }

        Dictionary<int, HashSet<int>> BuildPath(string data)
        {
            var result = new Dictionary<int, HashSet<int>>();
            foreach (var item in WalkPath(data))
            {
                if (!result.TryGetValue(item.x, out var Y))
                {
                    Y = new HashSet<int>();
                    result[item.x] = Y;
                }
                Y.Add(item.y);
            }
            return result;
        }

        List<(int x, int y)> Intersections(Dictionary<int, HashSet<int>> l1, Dictionary<int, HashSet<int>> l2)
        {
            var result = new List<(int x, int y)>();
            foreach (var x in l1)
                foreach (var y in x.Value)
                {
                    if (l2.TryGetValue(x.Key, out var l2y))
                        if (l2y.Contains(y))
                            result.Add((x.Key, y));
                }

            return result;
        }

        int Dist((int x, int y) pos) => Math.Abs(pos.x) + Math.Abs(pos.y);

        Dictionary<(int x, int y), int> WalkPathWithIntersections(string data, HashSet<(int x, int y)> intersections)
        {
            var result = new Dictionary<(int x, int y), int>();

            var steps = 0;
            foreach (var item in WalkPath(data))
            {
                steps++;

                if (intersections.Contains(item) && !result.ContainsKey(item))
                    result.Add(item, steps);
            }

            return result;
        }

        protected override string Solve1(List<string> data)
        {
            return Intersections(BuildPath(data[0]), BuildPath(data[1]))
                    .Select(Dist)
                    .Where(q => q != 0)
                    .Min().ToString();
        }

        protected override string Solve2(List<string> data)
        {
            var intersections = Intersections(BuildPath(data[0]), BuildPath(data[1])).ToHashSet();
            var Steps1 = WalkPathWithIntersections(data[0], intersections);
            var Steps2 = WalkPathWithIntersections(data[1], intersections);

            return Steps1.Select(q => q.Value + Steps2[q.Key]).Min().ToString();
        }
    }
}
