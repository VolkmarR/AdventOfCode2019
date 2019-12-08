using AdventOfCode.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static AdventOfCode.Base.Helpers;

namespace Day08
{
    public class Day08Tests
    {
        private readonly ITestOutputHelper Output;
        public Day08Tests(ITestOutputHelper output) => Output = output;

        [Fact]
        public void RunStep1() => Output.WriteLine(new Day08Solver().ExecutePuzzle1());

        [Fact]
        public void RunStep2() => Output.WriteLine(new Day08Solver().ExecutePuzzle2());
    }

    public class Day08Solver : SolverBase
    {

        List<int[,]> InitLayers(string data, int width, int height)
        {
            var result = new List<int[,]>();
            var pos = 0;
            while (pos < data.Length)
            {
                var layer = result.AddReturn(new int[width, height]);
                for (int h = 0; h < height; h++)
                    for (int w = 0; w < width; w++)
                        layer[w, h] = int.Parse(data.Substring(pos++, 1));
            }

            return result;
        }

        IEnumerable<int> Iterate(int[,] layer)
        {
            for (int h = 0; h < layer.GetLength(1); h++)
                for (int w = 0; w < layer.GetLength(0); w++)
                    yield return layer[w, h];
        }

        int[,] FindLayerMinNullCount(List<int[,]> layers)
        {
            int[,] result = null;
            int? nullCount = null;
            foreach (var layer in layers)
                MinValueWithAction(Iterate(layer).Count(q => q == 0), ref nullCount, () => result = layer);

            return result;
        }

        int[,] MergeLayers(List<int[,]> layers)
        {
            var result = layers.First();
            foreach (var layer in layers.Skip(1))
            {
                for (int h = 0; h < layer.GetLength(1); h++)
                    for (int w = 0; w < layer.GetLength(0); w++)
                        if (result[w, h] == 2)
                            result[w, h] = layer[w, h];
            }

            return result;
        }

        string PixelToColor(int pixel)
        {
            if (pixel == 0)
                return ".";
            else if (pixel == 1)
                return "#";

            return " ";
        }

        string WriteLayer(int[,] layer)
        {
            var result = new StringBuilder();
            for (int h = 0; h < layer.GetLength(1); h++)
            {
                for (int w = 0; w < layer.GetLength(0); w++)
                        result.Append(PixelToColor(layer[w, h]));

                result.AppendLine();
            }

            return result.ToString();
        }

        protected override string Solve1(List<string> data)
        {
            var layer = FindLayerMinNullCount(InitLayers(data[0], 25, 6));
            return (Iterate(layer).Count(q => q == 1) * Iterate(layer).Count(q => q == 2)).ToString();
        }

        protected override string Solve2(List<string> data)
        {
            return WriteLayer(MergeLayers(InitLayers(data[0], 25, 6)));
        }

    }
}
