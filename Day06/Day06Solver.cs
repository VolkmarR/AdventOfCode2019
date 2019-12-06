using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

namespace Day06
{
    public class Day06Tests
    {
        private readonly ITestOutputHelper Output;
        public Day06Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day06Solver().ExecutePuzzle1());
        
        [Fact]
        public void RunStep2() => Output.WriteLine(new Day06Solver().ExecutePuzzle2());
    }

    public class Day06Solver : SolverBase
    {
        (string obj, string orbits) ParseLine(string line)
        {
            var data = line.Split(')');
            return (data[1], data[0]);
        }

        Dictionary<string, string> Map = new Dictionary<string, string>();

        void InitMap(List<string> data)
        {
            Map = data.Select(ParseLine).ToDictionary(q => q.obj, q => q.orbits);
        }

        int CountDirectAndIndirect()
        {
            var count = Map.Count;
            foreach (var obj in Map.Keys)
            {
                var indirect = Map[obj];
                while (Map.TryGetValue(indirect, out indirect))
                    count++;
            }

            return count;
        }

        IEnumerable<string> Route(string start)
        {
            while (Map.TryGetValue(start, out start))
                yield return start;
        }

        int CountJumpsToSanta()
        {
            var santaRoute = Route("SAN").ToList();
            var youRoute = Route("YOU").ToList();

            var count = 0;
            var routeSwitch = youRoute.IndexOf("SAN");
            if (routeSwitch >= 0)
                return routeSwitch;

            foreach (var you in youRoute)
            {
                count++;
                routeSwitch = santaRoute.IndexOf(you);
                if (routeSwitch >= 0)
                    return count + routeSwitch - 1;
            }

            return -1;
        }

        protected override string Solve1(List<string> data)
        {
            InitMap(data);

            return CountDirectAndIndirect().ToString();
        }

        protected override string Solve2(List<string> data)
        {
            InitMap(data);

            return CountJumpsToSanta().ToString();
        }

    }
}
