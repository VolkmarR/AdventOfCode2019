using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AdventOfCode.Base.Helpers;

namespace Day07
{
    enum State { Running, Halt, WaitForInput, OutputProduced }

    class Computer
    {
        public List<int> Data { get; private set; }
        public List<int> Mem { get; private set; }

        private int? Input = null;
        private int? Output = null;

        private int Pos = 0;

        public State State = State.Halt;

        public Computer(string data)
        {
            Data = data.Split(",").Select(q => int.Parse(q)).ToList();
        }

        public void SetInputAndResume(int input)
        {
            if (State == State.WaitForInput)
            {
                Input = input;
                Resume();
            }
            else
                throw new Exception("Not in Input State");
        }

        public int GetOutputAndResume()
        {

            if (State == State.OutputProduced)
            {
                var result = Output.Value;
                Resume();
                return result;
            }
            else
                throw new Exception("Not in Output State");
        }


        bool IsHalt()
        {
            var isHalt = Mem[Pos] == 99;
            if (isHalt)
                State = State.Halt;

            return isHalt;
        }
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
            var opPos = Pos;
            var op = Mem[opPos++];
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
                param[i] = GetValue(Mem[opPos++], mode[i]);
            var dest = readDest ? Mem[opPos++] : 0;

            if (op == 1)
                Mem[dest] = param[0] + param[1];
            else if (op == 2)
                Mem[dest] = param[0] * param[1];
            else if (op == 3)
            {
                if (Input == null || State == State.WaitForInput)
                {
                    State = State.WaitForInput;
                    return true;
                }
                Mem[dest] = Input.Value;
                Input = null;
            }
            else if (op == 4)
            {
                if (Output == null || State == State.OutputProduced)
                {
                    Output = Mem[dest];
                    State = State.OutputProduced;
                    return true;
                }

                Output = null;
            }
            else if (op == 5)
            {
                if (param[0] != 0)
                    opPos = param[1];
            }
            else if (op == 6)
            {
                if (param[0] == 0)
                    opPos = param[1];
            }
            else if (op == 7)
                Mem[dest] = param[0] < param[1] ? 1 : 0;
            else if (op == 8)
                Mem[dest] = param[0] == param[1] ? 1 : 0;
            else
                throw new Exception("Invalid Operation");

            Pos = opPos;
            return false;
        }

        void Resume()
        {
            State = State.Running;
            while (Pos < Mem.Count)
            {
                if (IsHalt())
                    return;

                if (ExecNextOp())
                    return;
            }
        }

        public void Run()
        {
            Mem = Data.ToList();
            Output = null;
            Input = null;
            Pos = 0;

            Resume();
        }
    }
}
