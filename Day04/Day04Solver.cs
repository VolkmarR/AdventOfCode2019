using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

namespace Day04
{
    public class Day04Tests
    {
        private readonly ITestOutputHelper Output;
        public Day04Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day04Solver().ExecutePuzzle1());

        [Fact]
        public void RunStep2() => Output.WriteLine(new Day04Solver().ExecutePuzzle2());
    }

    public class Day04Solver : SolverBase
    {
        (int min, int max) GetRange(string data)
        {
            var values = data.Split('-').Select(q => int.Parse(q)).ToList();
            return (values[0], values[1]);
        }

        bool IsValid(int[] passwordDigits)
        {
            int last = passwordDigits.First();
            bool foundSame = false;

            foreach (var digit in passwordDigits.Skip(1))
            {
                foundSame = foundSame || last == digit;
                if (digit < last)
                    return false;
                last = digit;
            }

            return foundSame;
        }

        bool IsValid2(int[] passwordDigits)
        {
            if (!IsValid(passwordDigits))
                return false;

            var digitDict = new Dictionary<int, int>();
            for (int i = 0; i < 10; i++)
                digitDict[i] = 0;

            int last = passwordDigits.First();
            foreach (var digit in passwordDigits.Skip(1))
            {
                if (last == digit)
                    digitDict[digit] += 1;
                last = digit;
            }

            return digitDict.Values.Any(q => q == 1);
        }

        protected override string Solve1(List<string> data)
        { 
            var range = GetRange(data[0]);
            var count = 0;
            for (int i = range.min; i <= range.max; i++)
                if (IsValid(Digits(i)))
                    count++;
            return count.ToString();
        }

        protected override string Solve2(List<string> data)
        {
            var range = GetRange(data[0]);
            var count = 0;
            for (int i = range.min; i <= range.max; i++)
                if (IsValid2(Digits(i)))
                    count++;
            return count.ToString();
        }

    }
}
