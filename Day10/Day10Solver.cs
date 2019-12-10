using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

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
        protected override string Solve1(List<string> data)
        {
            return "??";
        }

        protected override string Solve2(List<string> data)
        {
            return "??";
        }

    }
}
