using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day_09
{
    class Program
    {
        static int preamble = 25;
        static int invalidNumber;
        static List<int> doubleSums = new List<int>();
        static List<int> preambleList = new List<int>();
        static List<long> invalidTotalList = new List<long>();

        static void Main(string[] args)
        {
            var inputNumbers = File.ReadAllLines("input-question.txt");
            SolutionOne(inputNumbers);
            SolutionTwo(inputNumbers);
        }

        static void SolutionOne(string[] inputData)
        {
            var index = 0;
            var doubleIndex = 0;
            for (int i = 0; i < inputData.Length; i++)
            {
                var currentNumber = int.Parse(inputData[i]);
                index = i % preamble;

                if (i >= preamble)
                {
                    index = preamble - 1;
                    if(!doubleSums.Contains(currentNumber))
                    {
                        invalidNumber = currentNumber;
                        Console.WriteLine($"Invalid Number: {invalidNumber}");
                        break;
                    }
                    else
                    {
                        preambleList.RemoveAt(0);
                        var dsIndex = doubleIndex;
                        for (int p = preamble - 2; p >= 0; p--)
                        {
                            doubleSums.RemoveAt(dsIndex);
                            dsIndex -= p;
                        }
                    }
                }
                else
                {
                    if(i < preamble - 1) doubleIndex += i;
                }

                preambleList.Add(currentNumber);
                for (int j = 0; j < index; j++)
                    doubleSums.Add(preambleList[j] + preambleList[index]);             
            }
        }

        static void SolutionTwo(string[] inputData)
        {
            long invalidTotal = 0;
            foreach (var number in inputData)
            {
                long currentNumber = long.Parse(number);
                invalidTotal += currentNumber;
                invalidTotalList.Add(currentNumber);
                    
                if( invalidTotal > invalidNumber)
                {
                    while(invalidTotal > invalidNumber)
                    {
                        invalidTotal -= invalidTotalList[0];
                        invalidTotalList.RemoveAt(0);
                    }
                }
                
                if(invalidTotal == invalidNumber) break;
            }
            invalidTotalList.Sort();
            var min = invalidTotalList.First();
            var max = invalidTotalList.Last();
            Console.WriteLine($"Encryption Weakness: {min + max}");
        }
    }
}
