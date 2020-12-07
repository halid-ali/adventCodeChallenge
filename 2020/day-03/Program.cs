using System;
using System.IO;
using System.Collections.Generic;

namespace day_03
{
    class Program
    {
        static int fieldX = 0;
        static int fieldY = 0;
        static IList<char[]> field;

        static void Main(string[] args)
        {
            field = CreateField();

            var r1d1 = TraverseField(1, 1);
            var r3d1 = TraverseField(3, 1);
            var r5d1 = TraverseField(5, 1);
            var r7d1 = TraverseField(7, 1);
            var r1d2 = TraverseField(1, 2);

            Console.WriteLine(r1d1 * r3d1 * r5d1 * r7d1 * r1d2);
        }

        static long TraverseField(int x, int y)
        {
            var treeCount = 0;

            var posX = 0;
            var posY = 0;

            while (true)
            {
                posX += x;
                posX = posX % fieldX;
                posY += y;

                if(posY >= fieldY) break;
                if(field[posY][posX] == '#') treeCount++;
            }

            return treeCount;
        }

        static IList<char[]> CreateField()
        {
            var area = new List<char[]>();

            foreach (var line in File.ReadLines("input-question.txt"))
            {
                var areaRow = new char[line.Length];
                fieldX = line.Length;

                for (int i = 0; i < line.Length; i++)
                {
                    areaRow[i] = line[i];
                }
                
                area.Add(areaRow);
                fieldY++;
            }

            return area;
        }
    }
}
