using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day01
{
    public class Day01Tests
    {
        private readonly ITestOutputHelper Output;
        public Day01Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day01Solver().ExecutePuzzle1());
        
        [Fact]
        public void RunStep2() => Output.WriteLine(new Day01Solver().ExecutePuzzle2());
    }

    public class Day01Solver : SolverBase
    {
        int CalcFuel(int mass) => (int)Math.Floor((double)(mass / 3)) - 2;

        int CalcFuel2(int mass)
        {
            var fuel = CalcFuel(mass);
            if (fuel <= 0)
                return 0;
            return fuel + CalcFuel2(fuel);
        }

        protected override string Solve1(List<string> data)
        {
            return data
                    .Select(q => CalcFuel(int.Parse(q)))
                    .Sum().ToString();
        }

        protected override string Solve2(List<string> data)
        {
            return data
                    .Select(q => CalcFuel2(int.Parse(q)))
                    .Sum().ToString();
        }

    }
}
