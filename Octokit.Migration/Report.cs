using System;
using System.Collections.Generic;

namespace Octokit.Migration
{
    public class Report
    {
        private readonly List<string> inconclusive = new List<string>();
        public void AddInconclusive(string route)
        {
            inconclusive.Add(route);
        }

        private readonly List<string> missing = new List<string>();
        public void AddMissing(string route)
        {
            missing.Add($"{route} - * ");
        }

        public void AddMissing(string route, string verb)
        {
            missing.Add($"{route} - {verb}");
        }

        private readonly List<string> matches = new List<string>();
        public void AddMatch(string route)
        {
            matches.Add($"{route} - *");
        }

        public void AddMatch(string route, string verb)
        {
            matches.Add($"{route} - {verb}");
        }

        public void OutputSummary()
        {
            var total = inconclusive.Count + missing.Count + matches.Count;

            Console.WriteLine($"{inconclusive.Count}/{total} inconclusive");

            if (missing.Count > 0 )
            {
                Console.WriteLine("Missing routes");
                foreach(var m in missing)
                {
                    Console.WriteLine($" ❔ {m}");
                }
            }

            if (matches.Count > 0)
            {
                Console.WriteLine("Found routes");
                foreach (var m in matches)
                {
                    Console.WriteLine($" ✔ {m}");
                }
            }

        }
    }
}
