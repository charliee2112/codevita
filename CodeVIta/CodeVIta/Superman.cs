using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeVIta
{
    public static class SupermanCannotFly
    {
        public static string SupermanTestCases()
        {
            string ret = "";
            string[] data = {
                 "1, 1, 12, 13, 4, 14, 3, 7, 4, 2, 3, 3, 12",
                 "15, 15, 7, 6, 15, 1, 4, 1, 7, 1, 10, 1, 13, 4, 12, 7, 13, 10, 12, 13, 12, 13, 9, 13, 6, 13, 3, 9, 5, 8, 8, 5, 8, 5, 5",
                 "7, 6, 15, 15, 15, 1, 4, 1, 7, 1, 10, 1, 13, 4, 12, 7, 13, 10, 12, 13, 12, 13, 9, 13, 6, 13, 3,  9, 5,  8, 8,  5, 8,  5, 5",
                 "0, 0, 15, 15,0",
                 "1, 1, 15, 15,0",
                 "0, 0, 14, 14,0",
                 "1, 1, 14, 14,0",
                 /*
                new List<int>{ 15, 15, 0, 0,0},
                new List<int>{ 14, 14, 0, 0,0},
                new List<int>{ 15, 15, 1, 1,0},
                new List<int>{ 14, 14, 1, 1,0},
                */

                /*
                new List<int>{ 0, 15, 15, 15,0},
                new List<int>{ 1, 15, 15, 15,0},
                new List<int>{ 1, 14, 15, 15,0},
                new List<int>{ 0, 14, 15, 15,0},

                new List<int>{ 0, 15, 15, 14,0},
                new List<int>{ 1, 15, 15, 14,0},
                new List<int>{ 1, 14, 15, 14,0},
                new List<int>{ 0, 14, 15, 14,0},

                new List<int>{ 0, 15, 14, 15,0},
                new List<int>{ 1, 15, 14, 15,0},
                new List<int>{ 1, 14, 14, 15,0},
                new List<int>{ 0, 14, 14, 15,0},

                new List<int>{ 0, 15, 14, 14,0},
                new List<int>{ 1, 15, 14, 14,0},
                new List<int>{ 1, 14, 14, 14,0},
                new List<int>{ 0, 14, 14, 14,0},
                */
            };
           
            data.ToList().ForEach(x=> {
                
                ret += x+";";
            });
            return ret;
        }

        public static void ShowSupermanInputs(List<int> x)
        {
            Console.Write("S(" + x[0] + ", " + x[1] + "), ");
            Console.Write("N(" + x[2] + ", " + x[3] + "), ");
            Console.WriteLine("Coordinates with K: ");
            Console.Write("{ ");
            for (int i = 5; i < x.Count - 1; i += 2)
            {
                Console.Write("(" + x[i] + "," + x[i + 1] + ") ");
            }
            Console.Write("}");
            Console.WriteLine();
        }

        public static void TestSuperman(string input, string output)
        {
            string[] cases = output.Split('\n').ToList().Where(x => !x.Equals("")).ToArray();
            string[] inputs = input.Split(';').Take(input.Split(';').Length - 1).ToArray();
            string[] expected = new string[cases.Length];
            int index = 0;
            inputs.ToList().ForEach(x => {
                string ret = Superman(x).Trim();
                bool result = true;
                List<Tuple<int, int>> obstacles = new List<Tuple<int, int>>();
                List<Tuple<int, int>> path = new List<Tuple<int, int>>();
                ret.Split(';').ToList().ForEach(y=> {
                    if (y != "")
                    {
                        path.Add(new Tuple<int, int>(Convert.ToInt32(y.Split(',')[0]), Convert.ToInt32(y.Split(',')[1])));
                    }
                });

                if (Convert.ToInt32(x.Split(',')[4].Trim()) > 0)
                {
                    for (int i = 4; i < x.Length - 1; i += 2)
                    {
                        //Here we assume radius 1 in every direction from the center
                        obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2]));

                        obstacles.Add(new Tuple<int, int>(x[i + 1] - 1, x[i + 2] - 1));
                        obstacles.Add(new Tuple<int, int>(x[i + 1] + 1, x[i + 2] + 1));

                        obstacles.Add(new Tuple<int, int>(x[i + 1] - 1, x[i + 2] + 1));
                        obstacles.Add(new Tuple<int, int>(x[i + 1] + 1, x[i + 2] - 1));

                        obstacles.Add(new Tuple<int, int>(x[i + 1] + 1, x[i + 2]));
                        obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2] + 1));

                        obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2] - 1));
                        obstacles.Add(new Tuple<int, int>(x[i + 1] - 1, x[i + 2]));
                    }
                }
                result = IsValidPathWithObstacles(path,obstacles,0,16,0,16);
                if (result)
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }
                expected[index++] = ret.Trim();
            });
            index = 0;
            Console.WriteLine();
        }

        public static string Superman(string data)
        {
            string ret = "";
            List<int> x = new List<int>();
            data.Split(',').ToList().ForEach(y => {
                x.Add(Convert.ToInt32(y.Trim()));
            });
            List<Tuple<int, int>> obstacles = new List<Tuple<int, int>>();
            for (int i = 4; i < x.Count - 1; i += 2)
            {
                //Here we assume radius 1 in every direction from the center
                obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2]));

                obstacles.Add(new Tuple<int, int>(x[i + 1] - 1, x[i + 2] - 1));
                obstacles.Add(new Tuple<int, int>(x[i + 1] + 1, x[i + 2] + 1));

                obstacles.Add(new Tuple<int, int>(x[i + 1] - 1, x[i + 2] + 1));
                obstacles.Add(new Tuple<int, int>(x[i + 1] + 1, x[i + 2] - 1));

                obstacles.Add(new Tuple<int, int>(x[i + 1] + 1, x[i + 2]));
                obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2] + 1));

                obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2] - 1));
                obstacles.Add(new Tuple<int, int>(x[i + 1] - 1, x[i + 2]));
            }
            Tuple<int, int> superman = new Tuple<int, int>(x[0], x[1]);
            Tuple<int, int> ship = new Tuple<int, int>(x[2], x[3]);
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            
            SupermanRecursive(result, ship, obstacles, superman);

            int a = 1;
            result.ForEach(step =>
            {
                ret += (step.Item1 + "," + step.Item2 + "; ");
                a++;
            });
            
            return ret + "\n";
        }

        public static void PrintPathWithObstacles(List<Tuple<int, int>> path, List<Tuple<int, int>> obstacles, int height, int width)
        {
            Console.WriteLine();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (obstacles.Where(x => x.Item1.Equals(i) && x.Item2.Equals(j)).Count() != 0)
                    {
                        Console.Write("▓");
                    }
                    else
                    {
                        if (path.Where(x => x.Item1.Equals(i) && x.Item2.Equals(j)).Count() != 0)
                        {
                            if (path.First().Item1.Equals(i) && path.First().Item2.Equals(j))
                            {
                                Console.Write("A");
                            }
                            else
                            {
                                if (path.Last().Item1.Equals(i) && path.Last().Item2.Equals(j))
                                {
                                    Console.Write("B");
                                }
                                else
                                {
                                    Console.Write("x");
                                }
                            }
                        }
                        else
                        {
                            Console.Write("▒");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        //Clumsy Superman, sticks to obstacles and to borders and already walked paths... Like some drunk guy, afflicted by k
        public static List<Tuple<int, int>> SupermanRecursive(List<Tuple<int, int>> pathSoFar, Tuple<int, int> ship, List<Tuple<int, int>> obstacles, Tuple<int, int> nextStep)
        {
            bool isValidStep = nextStep.Item1 > -1 && nextStep.Item2 > -1 && nextStep.Item2 < 16 && nextStep.Item1 < 16
                && pathSoFar.Where(x => x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)).Count() == 0
                && obstacles.Where(x => x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)).Count() == 0;
            //Can I actually take that step?
            if (isValidStep)
            {
                pathSoFar.Add(nextStep);
                if (ship.Item1.Equals(nextStep.Item1) && ship.Item2.Equals(nextStep.Item2))
                {//This is in case we reached the ship successfully
                    return pathSoFar;
                }
                else
                {//And here I did not yet reach my destination, I need to take another step. The first that comes up with a solution will be good enough
                    List<Tuple<int, int>> aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1 + 1, nextStep.Item2));
                    if (aux == null)
                    {
                        aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 + 1));
                        if (aux == null)
                        {
                            aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1 - 1, nextStep.Item2));
                            if (aux == null)
                            {
                                aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 - 1));
                                if (aux == null)
                                {
                                    pathSoFar.Remove(pathSoFar.Single(x => x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)));
                                    obstacles.Add(nextStep);
                                    return null;
                                }
                                else
                                {
                                    return aux;
                                }
                            }
                            else
                            {
                                return aux;
                            }
                        }
                        else
                        {
                            return aux;
                        }
                    }
                    else
                    {
                        return aux;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public static bool IsValidPathWithObstacles(List<Tuple<int, int>> path, List<Tuple<int, int>> obstacles, int minx, int maxx, int miny, int maxy)
        {
            bool isvalidpath = true;
            path.ForEach(step =>
            {
                if (step.Item1 < miny || step.Item2 < minx || step.Item1 > maxy || step.Item2 > maxx)
                {
                    isvalidpath = false;
                }
                if (obstacles.Where(obs => obs.Item2.Equals(step.Item2) && obs.Item1.Equals(step.Item1)).Count() > 0)
                {
                    isvalidpath = false;
                }
            });
            return isvalidpath;
        }
    }
}
