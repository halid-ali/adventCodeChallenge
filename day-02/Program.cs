using System;
using System.IO;

namespace day_02
{
    class Program
    {
        static int minValue;
        static int maxValue;
        static char character;
        static string password;
        static int validPasswords;

        static void Main(string[] args)
        {
            FirstSolution();
            validPasswords = 0;
            SecondSolution();
        }

        static void FirstSolution()
        {
            foreach (var input in File.ReadLines("input-question.txt"))
            {
                SetData(input);

                int passCharCount = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if(password[i] == character) passCharCount++;
                }

                if((passCharCount >= minValue) && (passCharCount <= maxValue)) validPasswords++;
            }

            Console.WriteLine($"Valid password count: {validPasswords}");
        }

        static void SecondSolution()
        {
            foreach (var input in File.ReadLines("input-question.txt"))
            {
                SetData(input);
                if(password[minValue - 1] == character ^ password[maxValue - 1] == character) validPasswords++;
            }

            Console.WriteLine($"Valid password count: {validPasswords}");
        }

        static void SetData(string input)
        {
            var dataArray = input.Split(' ');

            minValue = int.Parse(dataArray[0].Split('-')[0]);
            maxValue = int.Parse(dataArray[0].Split('-')[1]);

            character = dataArray[1][0];

            password = dataArray[2].Trim();
        }
    }
}
