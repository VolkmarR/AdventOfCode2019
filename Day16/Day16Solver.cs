using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

namespace Day16
{
    public class Day16Tests
    {
        private readonly ITestOutputHelper Output;
        public Day16Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day16Solver().ExecutePuzzle1());
        
        [Fact]
        public void RunStep2() => Output.WriteLine(new Day16Solver().ExecutePuzzle2());
    }

    public class Day16Solver : SolverBase
    {
        sbyte[] Parse(string data) => data.Select(q => sbyte.Parse(q.ToString())).ToArray();

        sbyte CalcOutputDigit(sbyte[] data, sbyte[] pattern, int outputPos)
        {
            var patternI = 0;
            var outputPosI = 0;
            
            void Step()
            {
                if (outputPosI == outputPos)
                {
                    patternI++;
                    outputPosI = 0;
                    if (patternI == pattern.Length)
                        patternI = 0;
                }
                else
                    outputPosI++;
            }
            
            var result = 0;
            Step();
            for (int i = 0; i < data.Length; i++)
            {
                result += (int)(data[i] * pattern[patternI]);
                Step();
            }

            return (sbyte)Math.Abs(result % 10);
        }

        void DoPhase(sbyte[] data, sbyte[] pattern, sbyte[] result)
        {
            for (int i = 0; i < data.Length; i++)
                result[i] = CalcOutputDigit(data, pattern, i);
        }

        void Swap(ref sbyte[] a1, ref sbyte[] a2)
        {
            var x = a1;
            a1 = a2;
            a2 = x;
        }

        protected override string Solve1(List<string> data)
        {
            var input = Parse(data[0]);
            var pattern = new sbyte[] { 0, 1, 0, -1 };
            var result = new sbyte[input.Length];

            for (int i = 0; i < 100; i++)
            {
                DoPhase(input, pattern, result);
                Swap(ref input, ref result);
            }

            return string.Join(" ", input.Take(8));
        }

        sbyte[] BuildInput2(sbyte[] data)
        {
            var result = new sbyte[data.Length * 10000];
            for (int i = 0; i < data.Length; i++)
                for (int j = 0; j < 10000; j++)
                    result[i * j] = data[i];
            
            return result;
        }

        protected override string Solve2(List<string> data)
        {
            var input = BuildInput2(Parse(data[0]));
            var pattern = new sbyte[] { 0, 1, 0, -1 };
            var result = new sbyte[input.Length];

            for (int i = 0; i < 100; i++)
            {
                DoPhase(input, pattern, result);
                Swap(ref input, ref result);
            }

            return string.Join(" ", input.Skip(22122816).Take(8));
        }

    }
}
