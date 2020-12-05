using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day_05
{
    class Program
    {
        static int highestSeatId = 0;
        static int lowestSeatId = int.MaxValue;
        static List<int> seatIds = new List<int>();

        static void Main(string[] args)
        {
            FindHighestSeat();
            FindMySeat();
        }

        static void FindHighestSeat()
        {
            foreach (var input in File.ReadLines("input-question.txt"))
            {
                int row = 0;

                for (int r = 0; r < 7; r++)
                    if(input[r] == 'B') row += (int)Math.Pow(2, 6 - r);

                int col = 0;

                for (int c = 0; c < 3; c++)
                    if(input[c + 7] == 'R') col += (int)Math.Pow(2, 2 - c);

                var seatId = row * 8 + col;
                seatIds.Add(seatId);

                if(seatId < lowestSeatId) lowestSeatId = seatId;
                if(seatId > highestSeatId) highestSeatId = seatId;
            }

            Console.WriteLine($"Highest Seat: {highestSeatId}");
        }

        static void FindMySeat()
        {
            for (int i = lowestSeatId + 1; i < highestSeatId; i++)
            {
                if(!seatIds.Contains(i))
                {
                    Console.WriteLine($"My Seat: {i}");
                    break;
                }
            }
        }
    }
}
