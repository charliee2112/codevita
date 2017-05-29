using System;
using System.Linq;

namespace CodeVIta
{
    public class Program
    {
        public delegate string Exercise(string input);
        static void Main(string[] args)
        {
            char option;
            bool leaving;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the examples. Please select an option:\n");
                    Console.WriteLine("1 → Polygons");
                    Console.WriteLine("2 → Superman");
                    Console.WriteLine("3 → Tomm & Jery");
                    Console.WriteLine("4 → Chaos in the Hangar");
                    Console.WriteLine("x → Exit\n");
                    option = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    switch (option)
                    {
                        case '1':
                            {
                                FormingFigures.TestPolygon(FormingFigures.PolygonTestCases(),RunExamples(FormingFigures.Polygons, FormingFigures.PolygonTestCases()));
                            }
                            break;
                        case '2':
                            {
                                SupermanCannotFly.TestSuperman(SupermanCannotFly.SupermanTestCases(), RunExamples(SupermanCannotFly.Superman, SupermanCannotFly.SupermanTestCases()));
                            }
                            break;
                        case '3':
                            {
                                TommAndJery.TestTJ(TommAndJery.TJTestCases(), RunExamples(TommAndJery.TJ, TommAndJery.TJTestCases()));
                            }
                            break;
                        case '4':
                            {
                                ChaosInTheHangar.TestHangar(ChaosInTheHangar.HangarTestCases(),RunExamples(ChaosInTheHangar.Hangar, ChaosInTheHangar.HangarTestCases()));
                            }
                            break;
                        case 'x':
                            {

                            }
                            break;
                        case 'X':
                            {

                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Still nothing here");
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    option = '0';
                }
                leaving = option.ToString().ToLower().Equals("x");
                if (!leaving)
                {
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
            while (!leaving);
        }

        private static string RunExamples(Exercise exercise, string cases)
        {
            string output = "";
            cases = cases.Substring(0, cases.Length - 1);
            cases.Split(';').ToList().ForEach(x => { output += (exercise(x) + ""); });
            return output;
        }
    }
}
