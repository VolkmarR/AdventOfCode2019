using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day02
{
    public class Day02Tests
    {
        private readonly ITestOutputHelper Output;
        public Day02Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day02Solver().ExecutePuzzle1());
        
        [Fact]
        public void RunStep2() => Output.WriteLine(new Day02Solver().ExecutePuzzle2());
    }

    public class Day02Solver : SolverBase
    {
        class Computer
        {
            public List<int> Data { get; private set; }
            public List<int> Mem { get; private set; }

            public Computer(string data)
            {
                Data = data.Split(",").Select(q => int.Parse(q)).ToList();
            }

            public bool Run()
            {
                Mem = Data.ToList();
                int pos = 0;

                while (pos < Mem.Count)
                {
                    var op = Mem[pos];
                    if (op == 99)
                        return true;

                    if (op == 1)
                        Mem[Mem[pos + 3]] = Mem[Mem[pos + 1]] + Mem[Mem[pos + 2]];
                    else if (op == 2)
                        Mem[Mem[pos + 3]] = Mem[Mem[pos + 1]] * Mem[Mem[pos + 2]];
                    else
                        return false;

                    pos += 4;
                }
                return false;
            }
        }

        protected override string Solve1(List<string> data)
        {
            var comp = new Computer(data[0]);
            comp.Data[1] = 12;
            comp.Data[2] = 2;

            comp.Run();

            return comp.Mem[0].ToString();
        }

        protected override string Solve2(List<string> data)
        {
            var comp = new Computer(data[0]);

            for (int noun = 0; noun < 100; noun++)
                for (int verb = 0; verb < 100; verb++)
                {
                    comp.Data[1] = noun;
                    comp.Data[2] = verb;

                    if (comp.Run() && comp.Mem[0] == 19690720)
                        return (100 * noun + verb).ToString();
                }

            return "Error";
        }

    }
}
