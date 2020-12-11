using System;
using System.IO;
using System.Linq;

namespace day_11
{
    class Program
    {
        static int sizeW, sizeH;
        static char[,] seatFieldForS1, seatFieldForS2, bufferField;
        delegate void MyAction<T1, T2>(ref int x, ref int y);
        delegate int MyFunc<T1, T2, T3, T4>(ref char[,] field, int i, int j);

        static void Main(string[] args)
        {
            CreateSeatField();
            Solve(GetNextSeatState, ref seatFieldForS1);
            Solve(GetMostNextSeatState, ref seatFieldForS2);
        }

        static void Solve(Func<int, int, char> getNextState, ref char [,] field)
        {
            int occupiedSeatCount = 0;
            while (PlaceSeats(getNextState, ref field, ref occupiedSeatCount));
            Console.WriteLine($"The answer is {occupiedSeatCount}");
        }

        static bool PlaceSeats(Func<int, int, char> getNextState, ref char [,] field, ref int occupiedSeatCount)
        {
            var hasChanges = false;
            occupiedSeatCount = 0;
            bufferField = new char[sizeW, sizeH];

            for (int i = 0; i < sizeW; i++)
            {
                for (int j = 0; j < sizeH; j++)
                {
                    bufferField[i, j] = getNextState(i, j);
                    if(bufferField[i, j] == '#') occupiedSeatCount++;
                    if(bufferField[i, j] != field[i, j]) hasChanges = true;
                }
            }
            field = bufferField;
            return hasChanges;
        }

        static char GetNextSeatState(int i, int j)
        {
            //check north adjacent
            var occupiedAdjacentCount = TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i - 1, j);
            //check south adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i + 1, j);
            //check west adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i, j - 1);
            //check east adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i, j + 1);
            //check north-west adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i - 1, j - 1);
            //check north-east adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i - 1, j + 1);
            //check south-west adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i + 1, j - 1);
            //check south-east adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(GetAdjacentForQ1, seatFieldForS1, i + 1, j + 1);

            if(seatFieldForS1[i, j] == 'L' && occupiedAdjacentCount == 0) return '#';
            if(seatFieldForS1[i, j] == '#' && occupiedAdjacentCount >= 4) return 'L';
            return seatFieldForS1[i, j];
        }

        static char GetMostNextSeatState(int i, int j)
        {
            //check north adjacent
            var occupiedAdjacentCount = SkipEmptyPlaces((ref int x, ref int y) => x--, i - 1, j);
            //check south adjacent
            occupiedAdjacentCount += SkipEmptyPlaces((ref int x, ref int y) => x++, i + 1, j);
            //check west adjacent
            occupiedAdjacentCount += SkipEmptyPlaces((ref int x, ref int y) => y--, i, j - 1);
            //check east adjacent
            occupiedAdjacentCount += SkipEmptyPlaces((ref int x, ref int y) => y++, i, j + 1);
            //check north-west adjacent
            occupiedAdjacentCount += SkipEmptyPlaces((ref int x, ref int y) => { x--; y--; }, i - 1, j - 1);
            //check north-east adjacent
            occupiedAdjacentCount += SkipEmptyPlaces((ref int x, ref int y) => { x--; y++; }, i - 1, j + 1);
            //check south-west adjacent
            occupiedAdjacentCount += SkipEmptyPlaces((ref int x, ref int y) => { x++; y--; }, i + 1, j - 1);
            //check south-east adjacent
            occupiedAdjacentCount += SkipEmptyPlaces((ref int x, ref int y) => { x++; y++; }, i + 1, j + 1);

            if(seatFieldForS2[i, j] == 'L' && occupiedAdjacentCount == 0) return '#';
            if(seatFieldForS2[i, j] == '#' && occupiedAdjacentCount >= 5) return 'L';
            return seatFieldForS2[i, j];
        }

        static int SkipEmptyPlaces(MyAction<int, int> action, int i, int j)
        {
            while(true)
            {
                var value = TryGetAdjacentValue(GetAdjacentForQ2, seatFieldForS2, i, j);
                if(value == -1) action(ref i, ref j);
                else return value;
            }
        }

        static int GetAdjacentForQ1(ref char[,] fieldPad, int i, int j)
        {
            var adjacent = fieldPad[i, j];
            return adjacent == '#' ? 1 : 0;
        }

        static int GetAdjacentForQ2(ref char[,] fieldPad, int i, int j)
        {
            var adjacent = fieldPad[i, j];
            switch(adjacent)
            {
                case '#': return 1;
                case 'L': return 0;
                default: return -1;
            }
        }

        static int TryGetAdjacentValue(MyFunc<char[,], int, int, int> func, char[,] field, int i, int j)
        {
            try
            {
                return func(ref field, i, j);
            }
            catch (IndexOutOfRangeException)
            {
                return 0;
            }
        }

        static void CreateSeatField()
        {
            var inputLines = File.ReadLines("input-question.txt").ToList();
            sizeW = inputLines.Count;
            sizeH = inputLines[0].Length;
            bufferField = new char[sizeW, sizeH];
            seatFieldForS1 = new char[sizeW, sizeH];
            seatFieldForS2 = new char[sizeW, sizeH];

            for (int i = 0; i < inputLines.Count; i++)
            {
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    seatFieldForS1[i, j] = inputLines[i][j];
                    seatFieldForS2[i, j] = inputLines[i][j];
                }
            }
        }
    }
}