using System;
using System.IO;
using System.Collections.Generic;

namespace day_08
{
    class Program
    {
        static List<ICommand> instructionList = new List<ICommand>();

        static void Main(string[] args)
        {
            CreateInstructionList();
            ExecuteInstructionList();

            Console.WriteLine($"Current Acc Value: {Globals.Accumulator}");
        }

        static void ExecuteInstructionList()
        {
            while (true)
            {
                var command = instructionList[Globals.InstructionPointer];

                if(command.IsExecuted) break;
                command.Execute();
            }
        }

        static void CreateInstructionList()
        {
            foreach (var line in File.ReadAllLines("input-question.txt"))
            {
                var lineData = line.Split(' ');
                var op = lineData[0];
                var opSign = lineData[1][0];
                var arg = int.Parse(lineData[1].Substring(1));

                instructionList.Add(CreateCommand(op, arg, opSign));
            }
        }

        static ICommand CreateCommand(string op, int argument, char opSign)
        {
            switch (op)
            {
                case "nop":
                    return new Nop(argument, opSign);

                case "acc":
                    return new Acc(argument, opSign);

                case "jmp":
                    return new Jmp(argument, opSign);

                default:
                throw new ArgumentException(nameof(op));
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    static class Globals
    {
        public static int Accumulator = 0;
        public static int InstructionPointer = 0;
    }

    interface ICommand
    {
        void Execute();
        bool IsExecuted { get; }
    }

    abstract class Command
    {
        protected int Argument;
        protected bool IsIncrease;
        protected bool IsExecuted = false;

        public Command(int argument, char opSign)
        {
            Argument = argument;
            IsIncrease = opSign.Equals('+');
        }
    }

    class Nop : Command, ICommand
    {
        public Nop(int argument, char opSign) : base(argument, opSign)
        { }

        void ICommand.Execute()
        {
            IsExecuted = true;
            Globals.InstructionPointer++;
        }

        bool ICommand.IsExecuted => IsExecuted;
    }

    class Acc : Command, ICommand
    {
        public Acc(int argument, char opSign) : base(argument, opSign)
        { }

        void ICommand.Execute()
        {
            if (IsIncrease)
            {
                Globals.Accumulator += Argument;
            }
            else
            {
                Globals.Accumulator -= Argument;
            }

            IsExecuted = true;
            Globals.InstructionPointer++;
        }

        bool ICommand.IsExecuted => IsExecuted;
    }

    class Jmp : Command, ICommand
    {
        public Jmp(int argument, char opSign) : base(argument, opSign)
        { }

        void ICommand.Execute()
        {
            if (IsIncrease)
            {
                Globals.InstructionPointer += Argument;
            }
            else
            {
                Globals.InstructionPointer -= Argument;
            }

            IsExecuted = true;
        }

        bool ICommand.IsExecuted => IsExecuted;
    }
}
