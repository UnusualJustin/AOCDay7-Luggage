using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOCDay7_Luggage
{
    class Program
    {
        static Dictionary<string, List<Tuple<int, string>>> Rules = new Dictionary<string, List<Tuple<int, string>>>();

        static void Main(string[] args)
        {
            LoadInput();

            DoChallengeOne();
            DoChallengeTwo();
        }

        static void LoadInput()
        {
            string[] lines = File.ReadAllLines("input.txt");

            foreach (string line in lines)
            {
                string color = line.Substring(0, line.IndexOf(" bags"));
                MatchCollection contentsMatch = Regex.Matches(line, @"(?<bagCount>\d+) (?<bagColor>.+?) bag");
                List<Tuple<int, string>> bagContains = new List<Tuple<int, string>>();
                foreach (Match match in contentsMatch)
                {
                    bagContains.Add(new Tuple<int, string>(int.Parse(match.Groups["bagCount"].Value), match.Groups["bagColor"].Value));
                }
                Rules.Add(color, bagContains);
            }
        }

        static void DoChallengeOne()
        {
            Console.Write("Challenge One: ");
            Console.WriteLine(Rules.Count(kvp => CanContainColor(kvp.Key)));
        }

        static bool CanContainColor(string color)
        {
            var rule = Rules[color];
            if (rule.Count(c => c.Item2 == "shiny gold") > 0)
            {
                return true;
            }
            else 
            {
                return rule.Any(r => CanContainColor(r.Item2)); 
            }
        }

        static void DoChallengeTwo()
        {
            Console.Write("Challenge Two: ");
            Console.WriteLine(CountNestedBags("shiny gold") - 1);
        }

        static int CountNestedBags(string color)
        {
            var rule = Rules[color];
            return rule.Aggregate(1, (result, item) => result + (item.Item1 * CountNestedBags(item.Item2)));           
        }        
    }
}
