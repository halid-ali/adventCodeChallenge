using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day_10
{
    class Program
    {
        static int minJoltValue = int.MaxValue;
        static int maxJoltValue = 0;
        static List<int> adapterList = new List<int>();

        static void Main(string[] args)
        {
            ReadInputs();
            SolutionPartOne();
            SolutionPartTwo();
        }

        static void SolutionPartOne()
        {
            var jolt1s = 1;
            var jolt3s = 0;

            while(true)
            {
                if(adapterList.Contains(minJoltValue + 1))
                {
                    jolt1s++;
                    minJoltValue += 1;
                    continue;
                }

                if(adapterList.Contains(minJoltValue + 3))
                {
                    jolt3s++;
                    minJoltValue += 3;
                    continue;
                }

                break;
            }

            Console.WriteLine($"Answer: {jolt1s * jolt3s}");
        }

        static void SolutionPartTwo()
        {
            adapterList.Add(0);
            adapterList.Sort();

            var arrangementList = new List<int>();
            var threeSequenceCount = 0;
            var sequenceIndex = 1;
            for (int i = 1; i < adapterList.Count - 1; i++)
            {
                var previous = adapterList[i - 1];
                var next = adapterList[i + 1];

                if (next - previous <= 3)
                {
                    if(arrangementList.Count != 0 && arrangementList.Last() + 1 == adapterList[i]) sequenceIndex++;
                    arrangementList.Add(adapterList[i]);
                    if(sequenceIndex == 3)
                    {
                        sequenceIndex = 0;
                        threeSequenceCount++;
                    }
                }
            }

            var totalDistinctWay = Math.Pow(2, arrangementList.Count);
            var isAdding = false;
            var arrangementCount = arrangementList.Count;

            for (int i = 1; i <= 9; i++)
            {
                arrangementCount -= 3;
                if (isAdding)
                {
                    totalDistinctWay += (Combination(9, i) * Math.Pow(2, arrangementCount));
                }
                else
                {
                    totalDistinctWay -= (Combination(9, i) * Math.Pow(2, arrangementCount));
                }
                isAdding = !isAdding;
            }

            Console.WriteLine($"Total Distinct Way: {totalDistinctWay}");
        }

        static long Combination(int n, int r)
        {
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }

        static long Factorial(int n)
        {
            if (n <= 1)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        static void ReadInputs()
        {
            foreach (var input in File.ReadLines("input-question.txt"))
            {
                var joltValue = int.Parse(input);
                if(joltValue < minJoltValue) minJoltValue = joltValue;
                if(joltValue > maxJoltValue) maxJoltValue = joltValue;
                adapterList.Add(joltValue);
            }
            adapterList.Add(maxJoltValue + 3); //add built-in adapter
        }
    }
}
