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
        IEnumerable<int[]> GetConfigs(int start, int end)
        {
            var unique = new HashSet<int>();
            for (int a = start; a <= end; a++)
                for (int b = start; b <= end; b++)
                    for (int c = start; c <= end; c++)
                        for (int d = start; d <= end; d++)
                            for (int e = start; e <= end; e++)
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

        Computer[] InitComputers(string data)
            => new Computer[] { new Computer(data), new Computer(data), new Computer(data), new Computer(data), new Computer(data) };

        int Solve1SingleRun(Computer[] computer, int[] config)
        {
            var value = 0;
            for (int i = 0; i < 5; i++)
            {
                computer[i].Run();
                computer[i].SetInputAndResume(config[i]);
                computer[i].SetInputAndResume(value);
                value = computer[i].GetOutputAndResume();
                if (computer[i].State != State.Halt)
                    throw new Exception("Invalid sequence");
            }

            return value;
        }

        int Solve2SingleRun(Computer[] computer, int[] config)
        {
            var value = 0;
            do
            {
                for (int i = 0; i < 5; i++)
                {
                    if (computer[i].State == State.Halt)
                    {
                        computer[i].Run();
                        computer[i].SetInputAndResume(config[i]);
                    }
                    computer[i].SetInputAndResume(value);
                    value = computer[i].GetOutputAndResume();
                }
            } while (computer[4].State != State.Halt);

            return value;
        }

        protected override string Solve1(List<string> data)
        {
            var computers = InitComputers(data[0]);

            var maxValue = 0;
            foreach (var config in GetConfigs(0, 4))
            {
                var value = Solve1SingleRun(computers, config);
                if (value > maxValue)
                    maxValue = value;
            }
            return maxValue.ToString();
        }

        protected override string Solve2(List<string> data)
        {
            var computer = InitComputers(data[0]);

            var maxValue = 0;
            foreach (var config in GetConfigs(5, 9))
            {
                var value = Solve2SingleRun(computer, config);
                if (value > maxValue)
                    maxValue = value;
            }
            return maxValue.ToString();
        }

    }
}
