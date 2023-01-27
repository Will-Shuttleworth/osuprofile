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
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            bool displayChoice;
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1: Display all top plays (100)");
            Console.WriteLine("2: Display profile");
            Console.WriteLine("3: Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    displayChoice = true;
                    ReadFile(displayChoice);
                    return true;
                case "2":
                    displayChoice = false;
                    ReadFile(displayChoice);
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }

        static void ReadFile(bool displayChoice)
        {
            List<double> playsList = new List<double>();
            string csvFile = "";
            Console.WriteLine("name of csv file");
            csvFile = Console.ReadLine();
            using (var sr = new StreamReader(@"e:\" + csvFile + ".csv"))
            {
                double weighted = 0;
                int i = 0;

                var plays = sr.ReadToEnd()
                    .Split('\n')
                    .SelectMany(s => s.Split(',')
                        .Select(x => int.Parse(x)))
                    .ToArray<int>();
                Array.Sort(plays);
                Array.Reverse(plays);

                foreach (var play in plays)
                {
                    switch (i)
                    {
                        case 0:
                            playsList.Add(play);
                            ++i;
                            break;
                        default:
                            weighted = play * Math.Pow(0.95, i - 1);
                            playsList.Add(weighted);
                            ++i;
                            break;
                    }
                }
            }
            if(displayChoice == true)
            {
                DisplayTop(playsList);
            }
            else if(displayChoice == false)
            {
                ProfileDisplay(playsList);
            }
            
        }

        static void ProfileDisplay(List<double> playsList)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Decimal points in top plays?");
            string choice = Console.ReadLine();

            int decimalCount = 0;
            bool decimalChoice = true;
            switch (choice)
            {
                case "y":
                    decimalChoice = true;
                    Console.WriteLine("How many decimal places?");
                    decimalCount = Convert.ToInt32(Console.ReadLine());
                    break;
                case "n":
                    decimalChoice = false;
                    break;
                default:
                    Console.WriteLine("y or n only");
                    System.Environment.Exit(1);
                    break;
            }

            Console.WriteLine("Username: " + username);

            double totalPerf = 0;
            foreach (double play in playsList)
            {
                totalPerf = totalPerf + play;
            }
            Console.WriteLine("Total PP: " + totalPerf);

            Console.WriteLine("Top ten plays:");
            if(decimalChoice == true)
            {
                foreach (double play in playsList.Take(10))
                {
                    Console.WriteLine("\n" + Math.Round(play, decimalCount));
                }
            }
            else if(decimalChoice == false)
            {
                foreach (double play in playsList.Take(10))
                {
                    Console.WriteLine("\n" + Convert.ToInt32(play));
                }
            }

            Console.ReadKey();
        }

        static void DisplayTop(List<double> playsList)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Decimal points in top plays?");
            string choice = Console.ReadLine();

            int decimalCount = 0;
            bool decimalChoice = true;
            switch (choice)
            {
                case "y":
                    decimalChoice = true;
                    Console.WriteLine("How many decimal places?");
                    decimalCount = Convert.ToInt32(Console.ReadLine());
                    break;
                case "n":
                    decimalChoice = false;
                    break;
                default:
                    Console.WriteLine("y or n only");
                    System.Environment.Exit(1);
                    break;
            }

            Console.WriteLine("Username: " + username);

            if (decimalChoice == true)
            {
                foreach (double play in playsList)
                {
                    Console.WriteLine("\n" + Math.Round(play, decimalCount));
                }
            }
            else if (decimalChoice == false)
            {
                foreach (double play in playsList)
                {
                    Console.WriteLine("\n" + Convert.ToInt32(play));
                }
            }

            Console.ReadKey();
        }
    }
}
