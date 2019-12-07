using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

namespace Day07
{
    public class Day07Tests
    {
        private readonly ITestOutputHelper Output;
        public Day07Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day07Solver().ExecutePuzzle1());

        [Fact]
        public void RunStep2() => Output.WriteLine(new Day07Solver().ExecutePuzzle2());
    }

    public class Day07Solver : SolverBase
    {
        IEnumerable<int[]> GetConfigs()
        {
            var unique = new HashSet<int>();
            for (int a = 0; a < 5; a++)
                for (int b = 0; b < 5; b++)
                    for (int c = 0; c < 5; c++)
                        for (int d = 0; d < 5; d++)
                            for (int e = 0; e < 5; e++)
                            {
                                unique.Clear();
                                unique.Add(a);
                                unique.Add(b);
                                unique.Add(c);
                                unique.Add(d);
                                unique.Add(e);
                                if (unique.Count == 5)
                                    yield return new int[] { a, b, c, d, e };
                            }
        }

        protected override string Solve1(List<string> data)
        {
            var computer = new Computer[] { new Computer(data[0]), new Computer(data[0]), new Computer(data[0]), new Computer(data[0]), new Computer(data[0]) };
            
            var maxValue = 0;
            foreach (var config in GetConfigs())
            {
                var value = 0;
                for (int i = 0; i < 5; i++)
                {
                    computer[i].SetInput(new List<int> { config[i], value }).Run();
                    value = computer[i].Output.Dequeue();
                }

                if (value > maxValue)
                    maxValue = value;
            }
            return maxValue.ToString();
        }

        protected override string Solve2(List<string> data)
        {
            return "???";
        }

    }
}
