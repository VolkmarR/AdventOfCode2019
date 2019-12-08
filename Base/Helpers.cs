using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Base
{
    public static class Helpers
    {
        public static int[] Digits(int value)
        {
            var list = new Stack<int>(32);
            do
            {
                list.Push(value % 10);
                value /= 10;
            } while (value != 0);

            return list.ToArray();
        }

        public static int MaxValue(int value1, int value2)
            => value2 > value1 ? value2 : value1;

        public static T AddReturn<T>(this ICollection<T> collection, T item)
        {
            collection.Add(item);
            return item;
        }

        public static void MinValueWithAction(int value, ref int? minValue, Action newMinValueAction)
        {
            if (minValue == null || value < minValue)
            {
                minValue = value;
                newMinValueAction();
            }
        }

        public static void MaxValueWithAction(int value, ref int? maxValue, Action newMaxValueAction)
        {
            if (maxValue == null || value < maxValue)
            {
                maxValue = value;
                newMaxValueAction();
            }
        }
    }
}
