using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

namespace Day05
{
    public class Day05Tests
    {
        private readonly ITestOutputHelper Output;
        public Day05Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day05Solver().ExecutePuzzle1());
        
        [Fact]
        public void RunStep2() => Output.WriteLine(new Day05Solver().ExecutePuzzle2());
    }

    public class Day05Solver : SolverBase
    {
        class Computer
        {
            public List<int> Data { get; private set; }
            public List<int> Mem { get; private set; }

            public Queue<int> Input { get; private set; }
            public Queue<int> Output { get; private set; }

            private int Pos = 0;

            public Computer(string data, List<int> input)
            {
                Data = data.Split(",").Select(q => int.Parse(q)).ToList();
                Input = new Queue<int>(input);
            }

            bool IsHalt()
                => Mem[Pos] == 99;

            int[] GetParameterMode(int op)
            {
                var result = new int[] { 0, 0, 0 };
                if (op > 100)
                {
                    var digits = Digits(op).Reverse().Skip(2).ToArray();
                    for (int i = 0; i < digits.Length; i++)
                        result[i] = digits[i];
                }
                return result;
            }

            int GetValue(int value, int mode)
                => mode == 0 ? Mem[value] : value;

            private bool ExecNextOp()
            {
                var op = Mem[Pos++];
                var mode = GetParameterMode(op);
                op %= 100;

                var param = new[] { 0, 0, 0 };
                var pnum = 2;
                var readDest = true;

                if (op == 3 || op == 4)
                    pnum = 0;
                else if (op == 5 || op == 6)
                {
                    pnum = 2;
                    readDest = false;
                }

                for (var i = 0; i < pnum; i++)
                    param[i] = GetValue(Mem[Pos++], mode[i]);
                var dest = readDest ? Mem[Pos++] : 0;

                if (op == 1)
                    Mem[dest] = param[0] + param[1];
                else if (op == 2)
                    Mem[dest] = param[0] * param[1];
                else if (op == 3)
                    Mem[dest] = Input.Dequeue();
                else if (op == 4)
                    Output.Enqueue(Mem[dest]);
                else if (op == 5)
                {
                    if (param[0] != 0)
                        Pos = param[1];
                }
                else if (op == 6)
                {
                    if (param[0] == 0)
                        Pos = param[1];
                }
                else if (op == 7)
                    Mem[dest] = param[0] < param[1] ? 1 : 0;
                else if (op == 8)
                    Mem[dest] = param[0] == param[1] ? 1 : 0;
                else
                    return false;

                return true;
            }

            public bool Run()
            {
                Mem = Data.ToList();
                Output = new Queue<int>();
                Pos = 0;

                while (Pos < Mem.Count)
                {
                    if (IsHalt())
                        return true;

                    if (!ExecNextOp())
                        return false;
                }

                return false;
            }
        }


        protected override string Solve1(List<string> data)
        {
            var comp = new Computer(data[0], new List<int> { 1 });
            comp.Run();

            return comp.Output.LastOrDefault().ToString();
        }

        protected override string Solve2(List<string> data)
        {
            var comp = new Computer(data[0], new List<int> { 5 });
            comp.Run();

            return comp.Output.LastOrDefault().ToString();
        }

    }
}
