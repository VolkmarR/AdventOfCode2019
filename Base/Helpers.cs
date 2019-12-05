﻿using System;
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

    }
}