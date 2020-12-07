using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var totalDistinctAnswers = 0;
            var totalCommonAnswers = 0;

            using (var streamReader = new StreamReader("input-question.txt"))
            {
                var distinctAnswers = new HashSet<char>();
                string commonAnswers = null;

                while (true)
                {
                    var answers = streamReader.ReadLine();
                    if(string.IsNullOrEmpty(answers))
                    {
                        totalDistinctAnswers += distinctAnswers.Count;
                        totalCommonAnswers += commonAnswers.Length;
                        commonAnswers = null;
                        distinctAnswers.Clear();

                        if(streamReader.EndOfStream) break;
                        continue;
                    }

                    for (int i = 0; i < answers.Length; i++)
                        distinctAnswers.Add(answers[i]);

                    if(commonAnswers == null)
                        commonAnswers = answers;
                    else
                        commonAnswers = new String(commonAnswers.Intersect<char>(answers).ToArray());
                }
            }

            Console.WriteLine($"Total Distinct Answers: {totalDistinctAnswers}");
            Console.WriteLine($"Total Common Answers: {totalCommonAnswers}");
        }
    }
}
