using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace osuprofile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("username:");
            string username = Console.ReadLine();
            ReadFile(username);
            Console.ReadKey();
        }

        static void ReadFile(string username)
        {
            using (var sr = new StreamReader(@"e:\test.csv"))
            {
                double weighted = 0;
                int i = 0;
                List<double> weightedPlays = new List<double>();

                var plays = sr.ReadToEnd()
                    .Split('\n')
                    .SelectMany(s => s.Split(',')
                        .Select(x => int.Parse(x)))
                    .ToArray<int>();
                Array.Sort(plays);
                Array.Reverse(plays);
                foreach (var play in plays)
                    // p * 0.95^(n-1)
                    weighted = play * Math.Pow(0.95, i - 1);
                    Console.WriteLine(weighted);
                    ++i;
                    
            }
        }
    }
}
