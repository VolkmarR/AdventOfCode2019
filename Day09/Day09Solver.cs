using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

namespace Day09
{
    public class Day09Tests
    {
        private readonly ITestOutputHelper Output;
        public Day09Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day09Solver().ExecutePuzzle1());
        
        [Fact]
        public void RunStep2() => Output.WriteLine(new Day09Solver().ExecutePuzzle2());
    }

    public class Day09Solver : SolverBase
    {
        protected override string Solve1(List<string> data)
        {
            var computer = new Computer(data[0]);
            computer.Run();
            computer.SetInputAndResume(1);

            var result = new List<string>();
            while (computer.State == State.OutputProduced) ;
            result.Add(computer.GetOutputAndResume().ToString());

            return string.Join(Environment.NewLine, result);
        }

        protected override string Solve2(List<string> data)
        {
            return "??";
        }

    }
}
