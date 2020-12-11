using System;
using System.IO;
using System.Linq;

namespace day_11
{
    class Program
    {
        static int sizeW = 0;
        static int sizeH = 0;
        static char[,] seatFieldForS1;
        static char[,] seatFieldForS2;
        static char[,] bufferField;

        static void Main(string[] args)
        {
            CreateSeatField();
            SolutionPartOne();
            SolutionPartTwo();
        }

        static void SolutionPartOne()
        {
            int occupiedSeatCount = 0;
            while (PlaceSeatsForS1(ref occupiedSeatCount));
            Console.WriteLine(occupiedSeatCount);
        }

        static void SolutionPartTwo()
        {
            int occupiedSeatCount = 0;
            while (PlaceSeatsForS2(ref occupiedSeatCount));
            Console.WriteLine(occupiedSeatCount);
        }

        static bool PlaceSeatsForS1(ref int occupiedSeatCount)
        {
            var hasChanges = false;
            occupiedSeatCount = 0;
            bufferField = new char[sizeW, sizeH];

            for (int i = 0; i < sizeW; i++)
            {
                for (int j = 0; j < sizeH; j++)
                {
                    bufferField[i, j] = GetNextSeatState(i, j);
                    if(bufferField[i, j] == '#') occupiedSeatCount++;
                    if(bufferField[i, j] != seatFieldForS1[i, j]) hasChanges = true;
                }
            }
            seatFieldForS1 = bufferField;

            return hasChanges;
        }

        static bool PlaceSeatsForS2(ref int occupiedSeatCount)
        {
            var hasChanges = false;
            occupiedSeatCount = 0;
            bufferField = new char[sizeW, sizeH];

            for (int i = 0; i < sizeW; i++)
            {
                for (int j = 0; j < sizeH; j++)
                {
                    bufferField[i, j] = GetMostNextSeatState(i, j);
                    if(bufferField[i, j] == '#') occupiedSeatCount++;
                    if(bufferField[i, j] != seatFieldForS2[i, j]) hasChanges = true;
                }
            }
            seatFieldForS2 = bufferField;

            return hasChanges;
        }

        static char GetNextSeatState(int i, int j)
        {
            var occupiedAdjacentCount = 0;

            //check north adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i - 1, j);
            //check south adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i + 1, j);
            //check west adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i, j - 1);
            //check east adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i, j + 1);
            //check north-west adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i - 1, j - 1);
            //check north-east adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i - 1, j + 1);
            //check south-west adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i + 1, j - 1);
            //check south-east adjacent
            occupiedAdjacentCount += TryGetAdjacentValue(i + 1, j + 1);

            if(seatFieldForS1[i, j] == 'L' && occupiedAdjacentCount == 0) return '#';
            if(seatFieldForS1[i, j] == '#' && occupiedAdjacentCount >= 4) return 'L';
            return seatFieldForS1[i, j];
        }

        static char GetMostNextSeatState(int i, int j)
        {
            var occupiedAdjacentCount = 0;

            //check north adjacent
            var x = i - 1;
            var y = j;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                { 
                    x--;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            //check south adjacent
            x = i + 1;
            y = j;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                { 
                    x++;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            //check west adjacent
            x = i;
            y = j - 1;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                { 
                    y--;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            //check east adjacent
            x = i;
            y = j + 1;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                { 
                    y++;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            //check north-west adjacent
            x = i - 1;
            y = j - 1;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                {
                    x--;
                    y--;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            //check north-east adjacent
            x = i - 1;
            y = j + 1;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                {
                    x--;
                    y++;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            //check south-west adjacent
            x = i + 1;
            y = j - 1;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                {
                    x++;
                    y--;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            //check south-east adjacent
            x = i + 1;
            y = j + 1;
            while(true)
            {
                var value = TryGetMostAdjacentValue(x, y);
                if(value == -1)
                {
                    x++;
                    y++;
                }
                else 
                {
                    occupiedAdjacentCount += value;
                    break;
                }
            }

            if(seatFieldForS2[i, j] == 'L' && occupiedAdjacentCount == 0) return '#';
            if(seatFieldForS2[i, j] == '#' && occupiedAdjacentCount >= 5) return 'L';
            return seatFieldForS2[i, j];
        }

        static int TryGetAdjacentValue(int i, int j)
        {
            try
            {
                var adjacent = seatFieldForS1[i, j];
                return adjacent == '#' ? 1 : 0;
            }
            catch (IndexOutOfRangeException)
            {
                return 0;
            }
        }

        static int TryGetMostAdjacentValue(int i, int j)
        {
            try
            {
                var adjacent = seatFieldForS2[i, j];
                switch(adjacent)
                {
                    case '#': return 1;
                    case 'L': return 0;
                    default: return -1;
                }
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
