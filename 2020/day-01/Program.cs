using System;
using System.Collections.Generic;
using System.IO;

namespace day_01
{
    class Program
    {
        static IDictionary<int, int> firstMatches = new Dictionary<int, int>();
        static IDictionary<int, Tuple<int, int>> secondMatches = new Dictionary<int, Tuple<int, int>>();

        static void Main(string[] args)
        {
            TwoEntriesSolution();
            firstMatches.Clear();
            ThreeEntriesSolution();
        }

        static void TwoEntriesSolution()
        {
            foreach (var number in File.ReadLines("input-question.txt"))
            {
                var inputNumber = Int32.Parse(number);
                var difference = 2020 - inputNumber;

                if(firstMatches.ContainsKey(inputNumber))
                {
                    Console.WriteLine("-- Two Entries Solution --");
                    Console.WriteLine($"   Numbers: {firstMatches[inputNumber]} and {inputNumber}");
                    Console.WriteLine($"   Total: {firstMatches[inputNumber] + inputNumber}");
                    Console.WriteLine($"   Result: {firstMatches[inputNumber] * inputNumber}");
                    break;
                }

                firstMatches.Add(difference, inputNumber);
            }
        }

        static void ThreeEntriesSolution()
        {
            foreach (var number in File.ReadLines("input-question.txt"))
            {
                var inputNumber = Int32.Parse(number);
                var difference = 2020 - inputNumber;

                firstMatches.Add(inputNumber, difference);
            }

            var isFound = false;
            foreach (var firstMatch in firstMatches)
            {
                var firstDifference = firstMatch.Value;

                foreach (var possibleSecond in firstMatches)
                {
                    if (firstDifference <= possibleSecond.Key) continue;
                    if (possibleSecond.Key == firstMatch.Key) continue;

                    var secondDifference = firstDifference - possibleSecond.Key;

                    if (secondMatches.ContainsKey(possibleSecond.Key))
                    {
                        var firstNumber = secondMatches[possibleSecond.Key].Item1;
                        var secondNumber = secondMatches[possibleSecond.Key].Item2;
                        var thirdNumber = possibleSecond.Key;

                        Console.WriteLine("-- Three Entries Solution --");
                        Console.WriteLine($"   Numbers: {firstNumber} and {secondNumber} and {thirdNumber}");
                        Console.WriteLine($"   Total: {firstNumber + secondNumber + thirdNumber}");
                        Console.WriteLine($"   Result: {firstNumber * secondNumber * thirdNumber}");
                        isFound = true;
                        break;
                    }

                    if (!secondMatches.ContainsKey(secondDifference))
                    {
                        secondMatches.Add(secondDifference, new Tuple<int, int>(firstMatch.Key, possibleSecond.Key));
                    }
                }

                if (isFound) break;
            }
        }
    }
}
