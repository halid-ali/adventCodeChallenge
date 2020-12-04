using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace day_04
{
    class Program
    {
        static List<string> eyeColors = new List<string>{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        static Dictionary<string, string> passportData = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            var validPassportCount = 0;

            using (var streamReader = new StreamReader("input-question.txt"))
            {
                while (true)
                {
                    var line = streamReader.ReadLine();
                    if(string.IsNullOrEmpty(line))
                    {
                        if(CheckPassportValidity()) validPassportCount += 1;
                        passportData.Clear();

                        if(streamReader.EndOfStream) break;
                        continue;
                    }

                    var lineData = line.Split(' ');
                    foreach (var data in lineData)
                    {
                        passportData.Add(data.Split(':')[0], data.Split(':')[1]);
                    }
                }
            }

            Console.WriteLine($"Valid Passports: {validPassportCount}");
        }

        static bool CheckPassportValidity()
        {
            if(passportData.Count < 7) return false;
            if(passportData.Count == 7 && passportData.ContainsKey("cid")) return false;

            if(int.Parse(passportData["byr"]) < 1920 || int.Parse(passportData["byr"]) > 2002) return false;
            if(int.Parse(passportData["iyr"]) < 2010 || int.Parse(passportData["iyr"]) > 2020) return false;
            if(int.Parse(passportData["eyr"]) < 2020 || int.Parse(passportData["eyr"]) > 2030) return false;

            var hgt = int.Parse(passportData["hgt"].Replace("in", "").Replace("cm", ""));
            if(passportData["hgt"].Contains("cm"))
            {
                if(hgt < 150 || hgt > 193) return false;
            }
            else
            {
                if(hgt < 59 || hgt > 76) return false;
            }
            
            if(!Regex.Match(passportData["hcl"], "^#(?:[0-9a-f]{6})$").Success) return false;

            if(!eyeColors.Contains(passportData["ecl"])) return false;

            if(passportData["pid"].Length != 9 || !passportData["pid"].All(Char.IsDigit)) return false; 

            return true;
        }
    }
}