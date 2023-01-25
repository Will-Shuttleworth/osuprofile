using System;
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
            ChooseDecimal(username);
            Console.ReadKey();
        }

        static void ChooseDecimal(string username)
        {
            
            bool chooseDecimal = false;

            Console.WriteLine("decimal points? y/n");
            string decimalChoice = Console.ReadLine();

            switch (decimalChoice)
            {
                case "y":
                    chooseDecimal = true;
                    ReadFile(username, chooseDecimal);
                    break;
                case "n":
                    chooseDecimal = false;
                    ReadFile(username, chooseDecimal);
                    break;
                default:
                    Console.WriteLine("y or n only");
                    System.Environment.Exit(1);
                    break;
            }
        }

        static void ReadFile(string username, bool chooseDecimal)
        {
            string csvFile = "";
            Console.WriteLine("name of csv file");
            csvFile = Console.ReadLine();
            using (var sr = new StreamReader(@"e:\" + csvFile + ".csv"))
            {
                double weighted = 0;
                int i = 0;
                int decimalCount = 0;
                int playsCount = 0;

                Console.WriteLine("how many plays to display 1-100");
                playsCount = Convert.ToInt32(Console.ReadLine());

                var plays = sr.ReadToEnd()
                    .Split('\n')
                    .SelectMany(s => s.Split(',')
                        .Select(x => int.Parse(x)))
                    .ToArray<int>();
                Array.Sort(plays);
                Array.Reverse(plays);

                switch (chooseDecimal)
                {
                    case true:
                        
                        Console.WriteLine("how many decimal places");
                        decimalCount = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\n" + username);
                        foreach (var play in plays.Take(playsCount))
                        {
                            switch (i)
                            {
                                case 0:
                                    Console.WriteLine(play);
                                    ++i;
                                    break;
                                default:
                                    weighted = play * Math.Pow(0.95, i - 1);
                                    Console.WriteLine(Math.Round(weighted, decimalCount));
                                    ++i;
                                    break;
                            }
                        }

                        break;

                    case false:
                        
                        Console.WriteLine("\n" + username);
                        foreach (var play in plays.Take(playsCount))
                        {
                            switch (i)
                            {
                                case 0:
                                    Console.WriteLine(play);
                                    ++i;
                                    break;
                                default:
                                    weighted = play * Math.Pow(0.95, i - 1);
                                    Console.WriteLine(Convert.ToInt32(weighted));
                                    ++i;
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
}
