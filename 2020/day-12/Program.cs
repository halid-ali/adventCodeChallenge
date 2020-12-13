using System;
using System.IO;
using System.Collections.Generic;

namespace day_12
{
    class Program
    {
        static int xS = 0, yS = 0, xWP = 10, yWP = 1;
        static string currentDirection = "East";
        static List<KeyValuePair<char, int>> inputs = new List<KeyValuePair<char, int>>();
        static List<string> directions = new List<string>{ "North", "East", "South", "West" };

        static void Main(string[] args)
        {
            ReadInputs();
            SolutionPartOne();
            SolutionPartTwo();
        }

        static void ReadInputs()
        {
            foreach (var input in File.ReadLines("input-question.txt"))
            {
                inputs.Add(new KeyValuePair<char, int>(input[0], int.Parse(input.Substring(1))));
            }
        }

        static void Init()
        {
            xS = 0;
            yS = 0;
            currentDirection = "East";
        }

        static void SolutionPartOne()
        {
            Init();
            foreach (var input in inputs)
            {
                if(input.Key == 'N') yS += input.Value;
                else if(input.Key == 'S') yS -= input.Value;
                else if(input.Key == 'E') xS += input.Value;
                else if(input.Key == 'W') xS -= input.Value;
                else if(input.Key == 'F') GoForward(input.Value);
                else if(input.Key == 'R' || input.Key == 'L') SetCurrentDirection(input.Key, input.Value);
            }

            Console.WriteLine(Math.Abs(xS) + Math.Abs(yS));
        }

        static void SolutionPartTwo()
        {
            Init();
            foreach (var input in inputs)
            {
                if(input.Key == 'N') yWP += input.Value;
                else if(input.Key == 'S') yWP -= input.Value;
                else if(input.Key == 'E') xWP += input.Value;
                else if(input.Key == 'W') xWP -= input.Value;
                else if(input.Key == 'F') GoForwardWithWaypoint(input.Value);
                else if(input.Key == 'R' || input.Key == 'L') RotateWaypoint(input.Key, input.Value);
            }

            Console.WriteLine(Math.Abs(xS) + Math.Abs(yS));
        }

        static void GoForward(int value)
        {
            if(currentDirection == "North") yS += value;
            else if(currentDirection == "South") yS -= value;
            else if(currentDirection == "East") xS += value;
            else if(currentDirection == "West") xS -= value;
        }

        static void GoForwardWithWaypoint(int value)
        {
            if(currentDirection == "North")
            {
                xS += (xWP * value);
                yS += (yWP * value);
            }
            else if(currentDirection == "South")
            {
                xS -= (xWP * value);
                yS -= (yWP * value);
            }
            else if(currentDirection == "East")
            {
                xS += (xWP * value);
                yS += (yWP * value);
            }
            else if(currentDirection == "West")
            {
                xS -= (xWP * value);
                yS -= (yWP * value);
            }
        }

        static void SetCurrentDirection(char action, int value)
        {
            var cdi = directions.FindIndex((s) => s == currentDirection); //current direction index
            var nextIndex = action == 'R' ? (cdi + (value / 90)) % 4 : (cdi + 4 - (value / 90)) % 4;
            currentDirection = directions[nextIndex];
        }

        static void RotateWaypoint(char action, int value)
        {
            if (value == 90)
            {
                if(action == 'L')
                {
                    var tempXWP = xWP;
                    xWP = yWP * -1;
                    yWP = tempXWP;
                }
                else
                {
                    var tempXWP = xWP;
                    xWP = yWP;
                    yWP = tempXWP * -1;
                }
            }
            else if (value == 180)
            {
                xWP *= -1;
                yWP *= -1;
            }
            else
            {
                if(action == 'L')
                {
                    var tempXWP = xWP;
                    xWP = yWP;
                    yWP = tempXWP * -1;
                }
                else
                {
                    var tempXWP = xWP;
                    xWP = yWP * -1;
                    yWP = tempXWP;
                }
            }
        }
    }
}
