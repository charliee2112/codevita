using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVIta
{
    public class Program
    {
        static void Main(string[] args)
        {
            char option;
            bool leaving;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the examples. Please select an option:");
                    Console.WriteLine("1-> Polygons");
                    Console.WriteLine("2-> Superman");
                    Console.WriteLine("3-> Jumping Superman");
                    Console.WriteLine("4-> Snake");
                    Console.WriteLine("x-> Exit");
                    option = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    switch (option)
                    {
                        case '1':
                            {
                                RunPolygonsExamples();
                            }
                            break;
                        case '2':
                            {
                                RunSupermanExamples();
                            }
                            break;
                        case '3':
                            {
                                RunSupermanJumpExamples();
                            }
                            break;
                        case '4':
                            {
                                RunSnakeExamples();
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
                catch (Exception)
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

        #region SupermanJump

        private static void RunSupermanJumpExamples()
        {
            List<string>[] data = {
                new List<string>{ "A", "Z", "12", "24",
                    "A","B","C","D","E","F","G","H","I","J","K","Z",
                    "A","B","B","A",
                    "A","D","D","A",
                    "A","C","C","A",
                    "F","D","D","F",
                    "F","E","E","F",
                    "F","G","G","F",
                    "C","E","E","C",
                    "I","E","E","I",
                    "J","K","K","J",
                    "J","I","I","J",
                    "K","H","H","K",
                    "K","Z","Z","K",
                },

            };
            data.ToList().ForEach(x =>
            {
                Console.WriteLine("For input: ");
                ShowSupermanJumpInputs(x);
                Console.WriteLine("The output is: ");
                SupermanJump(x.ToArray());
            });
        }

        private static void ShowSupermanJumpInputs(List<string> x)
        {
            Console.Write("{ ");
            x.ForEach(y =>
            {
                Console.Write(y + " ");
            });
            Console.Write("}");
            Console.WriteLine();
        }

        //No length is bigger or equal to the sum of all lengths
        public static void SupermanJump(string[] data)

        {
            string superman = data[0];
            string lex = data[1];
            int platformsAmount = Convert.ToInt16(data[2]);
            int totalJumps = Convert.ToInt16(data[3]);

            List<Tuple<string, string>> jumps = new List<Tuple<string, string>>();
            List<string> platforms = new List<string>();

            for (int i = 4; i < platformsAmount + 4; i++)
            {
                platforms.Add(data[i]);
            }

            for (int i = 4 + platformsAmount; i < data.Length - 1; i += 2)
            {
                jumps.Add(new Tuple<string, string>(data[i], data[i + 1]));
            }

            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            SupermanJumpRecursive("", lex, jumps, result, new Tuple<string, string>("", superman));
            result.Add(new Tuple<string, string>(result.Last().Item2, lex));

            result.ForEach(x =>
            {
                Console.Write("  " + x.Item2);
            });
            Console.WriteLine();
        }

        private static List<Tuple<string, string>> SupermanJumpRecursive(string currentPlatform, string finalPlatform, List<Tuple<string, string>> possibleJumps, List<Tuple<string, string>> path, Tuple<string, string> lastJump)
        {

            if (path.Where(x => x.Item2.Equals(lastJump.Item2)).Count() > 0)
            {
                return null;
            }
            else
            {
                path.Add(lastJump);
                currentPlatform = lastJump.Item2;
                List<Tuple<string, string>> immediateJumps = possibleJumps.Where(x => x.Item1.Equals(currentPlatform)).ToList();
                immediateJumps.RemoveAll(x => x.Item1.Equals(lastJump.Item1) && x.Item2.Equals(lastJump.Item2));
                immediateJumps.RemoveAll(x => x.Item2.Equals(lastJump.Item1) && x.Item1.Equals(lastJump.Item2));

                if (immediateJumps.Count > 0)
                {
                    List<Tuple<string, string>> aux;
                    int i = 0;
                    do
                    {
                        aux = SupermanJumpRecursive(lastJump.Item2, finalPlatform, possibleJumps, path, immediateJumps[i]);
                        if (aux != null)
                        {
                            return aux;
                        }
                        i++;
                    } while (i < immediateJumps.Count);
                    return null;
                }
                else
                {
                    path.Remove(lastJump);
                    possibleJumps.RemoveAll(x => x.Item1.Equals(lastJump.Item1) && x.Item2.Equals(lastJump.Item2));
                    possibleJumps.RemoveAll(x => x.Item2.Equals(lastJump.Item1) && x.Item1.Equals(lastJump.Item2));
                    return null;
                }
            }

        }

        #endregion


        #region Polygons

        private static void RunPolygonsExamples()
        {
            List<int>[] data = {
                new List<int>{ 1, 1, 1 },
                new List<int>{ 1, 2, 1 },
                new List<int>{ 1, 1, 3 },
                new List<int>{ 1, 2, 2 },
                new List<int>{ 1, 2, 3 },
                new List<int>{ 1, 2, 2, 3 },
                new List<int>{ 1, 2, 2, 4 },
                new List<int>{ 1, 2, 2, 5 },
                new List<int>{ 1, 2, 2, 5, 1 },
                new List<int>{ 5,2 },
                new List<int>{ 10 }
            };
            data.ToList().ForEach(x =>
            {
                Console.WriteLine("For input: ");
                ShowPolygonsInputs(x);
                Console.WriteLine("The output is: ");
                Polygons(x.Count, x.ToArray());
            });
        }

        private static void ShowPolygonsInputs(List<int> x)
        {
            Console.Write("{ ");
            x.ForEach(y =>
            {
                Console.Write(y + " ");
            });
            Console.Write("}");
            Console.WriteLine();
        }

        //No length can be bigger or equal than the sum of the others
        public static void Polygons2(int amount, int[] lengths)
        {
            bool result = true;
            for (int length = 0; length < amount; length++)
            {
                int sum = 0;
                for (int otherLength = 0; otherLength < amount; otherLength++)
                {
                    if (length != otherLength)
                    {
                        sum += lengths[otherLength];
                    }
                }
                if (sum <= lengths[length])
                {
                    result = false;
                }
            }
            if (result)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
            Console.WriteLine();
        }

        //No length is bigger or equal to the sum of all lengths
        public static void Polygons(int amount, int[] lengths)
        {
            bool result = true;
            double sum = 0;
            for (int i = 0; i < amount; i++)
            {
                sum += lengths[i];
            }
            for (int i = 0; i < amount; i++)
            {
                if (lengths[i] >= (sum / 2))
                {
                    result = false;
                }
            }
            if (result)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
            Console.WriteLine();
        }

        #endregion

        #region Superman

        private static void RunSupermanExamples()
        {
            List<int>[] data = {
                new List<int>{ 1, 1, 12, 13,
                    4,
                    14, 3,
                    7, 4,
                    2, 3,
                    3, 12
                },

                new List<int>{ 15, 15, 7, 6,
                    15,
                    1, 4,
                    1,7,
                    1,10,
                    1,13,
                    4,12,
                    7,13,
                    10,12,
                    13,12,
                    13,9,
                    13,6,
                    13,3,
                    9,5,
                    8,8,
                    5,8,
                    5,5
                },

                new List<int>{ 7, 6, 15, 15,
                    15,     1, 4,  1,7,  1,10,  1,13,  4,12,  7,13,  10,12,  13,12,  13,9,  13,6,  13,3,  9,5,  8,8,  5,8,  5,5  },
                new List<int>{ 0, 0, 15, 15,0},
                new List<int>{ 1, 1, 15, 15,0},
                new List<int>{ 0, 0, 14, 14,0},
                new List<int>{ 1, 1, 14, 14,0},

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
            data.ToList().ForEach(x =>
            {
                Console.WriteLine("For input: ");
                ShowSupermanInputs(x);
                Console.WriteLine("The output is: ");
                Superman(x);
                Console.ReadKey();
            });
        }

        private static void ShowSupermanInputs(List<int> x)
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

        public static void Superman(List<int> x)
        {
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


            Console.WriteLine("The steps to follow in this case are: ");
            Console.Write("{ ");
            int a = 1;
            result.ForEach(step =>
            {
                if (a % 5 == 0) Console.WriteLine();
                Console.Write("(" + step.Item1 + "," + step.Item2 + ") ");
                a++;
            });
            Console.Write("}");


            PrintPathWithObstacles(result, obstacles, 16, 16);
            Console.WriteLine(IsValidPathWithObstacles(result, obstacles, 0, 16, 0, 16));

            Console.WriteLine();
        }

        private static void PrintPathWithObstacles(List<Tuple<int, int>> path, List<Tuple<int, int>> obstacles, int height, int width)
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
        private static List<Tuple<int, int>> SupermanRecursive(List<Tuple<int, int>> pathSoFar, Tuple<int, int> ship, List<Tuple<int, int>> obstacles, Tuple<int, int> nextStep)
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

        private static bool IsValidPathWithObstacles(List<Tuple<int, int>> path, List<Tuple<int, int>> obstacles, int minx, int maxx, int miny, int maxy)
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
        #endregion

        #region Snake

        private static void Superman(string[] data)
        {
            string[] hangar = data.Take(2).ToArray();
            List<Tuple<int, int>> aux = SnakeJump(hangar);
            List<List<Tuple<int, int>>> table = new List<List<Tuple<int, int>>>();
            for (int i = 2; i< data.Length - 1; i += 2)
            {

                int index = aux.IndexOf(new Tuple<int, int>(int.Parse(data[i]), int.Parse(data[i + 1])));
                table.Add(aux.Take(index).Reverse().ToList());
            }
            int j = 0;
            table.ForEach(x=> 
            {
                Console.Write((j++)+": ");
                x.ForEach(y =>
                {
                    Console.Write(y.Item1 + "," + y.Item2 + "; ");
                });
                Console.WriteLine();
            });
        }

        private static void RunSnakeExamples()
        {
            List<string> data = new List<string>();
            data.Add("2,2,2,1,2,0,1,1,1,2");
            data.Add("3,3,0,0");
            data.ToList().ForEach(x =>
            {
                Console.WriteLine("For input: ");
                ShowSnakeJumpInputs(x);
                Console.WriteLine("The output is: ");
                Superman(x.Split(',').ToArray());
            });
        }

        private static void ShowSnakeJumpInputs(string x)
        {
            Console.Write("{ ");
            x.ToList().ForEach(y =>
            {
                Console.Write(y + " ");
            });
            Console.Write("}");
            Console.WriteLine();
        }

        //No length is bigger or equal to the sum of all lengths
        public static List<Tuple<int, int>> SnakeJump(string[] data)
        {
            Tuple<int, int> hangar = new Tuple<int, int>(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            if (hangar.Item2 % 2 == 0)
            {
                SnakeRecursive1(result, hangar);
            }
            else
            {
                SnakeRecursive2(result, hangar);
            }
            return result;
        }

        private static List<Tuple<int, int>> SnakeRecursive1(List<Tuple<int, int>> pathSoFar, Tuple<int, int> nextStep)
        {
            bool isValidStep = nextStep.Item1 > -1 && nextStep.Item2 > -1 && nextStep.Item2 < 6 && nextStep.Item1 < 6 && !pathSoFar.Contains(nextStep);
            //Can I actually take that step?
            if (isValidStep)
            {
                pathSoFar.Add(nextStep);
                //And here I did not yet reach my destination, I need to take another step. The first that comes up with a solution will be good enough
                List<Tuple<int, int>> aux = SnakeRecursive1(pathSoFar, new Tuple<int, int>(nextStep.Item1 + 1, nextStep.Item2));
                if (aux == null)
                {
                    aux = SnakeRecursive1(pathSoFar, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 + 1));
                    if (aux == null)
                    {
                        aux = SnakeRecursive1(pathSoFar, new Tuple<int, int>(nextStep.Item1 - 1, nextStep.Item2));
                        if (aux == null)
                        {
                            aux = SnakeRecursive1(pathSoFar, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 - 1));
                            if (aux == null)
                            {
                                pathSoFar.Remove(pathSoFar.Single(x => x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)));
                                return pathSoFar;
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
            else
            {
                return null;
            }
        }
        private static List<Tuple<int, int>> SnakeRecursive2(List<Tuple<int, int>> pathSoFar, Tuple<int, int> nextStep)
        {
            bool isValidStep = nextStep.Item1 > -1 && nextStep.Item2 > -1 && nextStep.Item2 < 6 && nextStep.Item1 < 6 && !pathSoFar.Contains(nextStep);
            //Can I actually take that step?
            if (isValidStep)
            {
                pathSoFar.Add(nextStep);
                //And here I did not yet reach my destination, I need to take another step. The first that comes up with a solution will be good enough
                List<Tuple<int, int>> aux = SnakeRecursive2(pathSoFar, new Tuple<int, int>(nextStep.Item1 + 1, nextStep.Item2));
                if (aux == null)
                {
                    aux = SnakeRecursive2(pathSoFar, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 - 1));
                    if (aux == null)
                    {
                        aux = SnakeRecursive2(pathSoFar, new Tuple<int, int>(nextStep.Item1 - 1, nextStep.Item2));
                        if (aux == null)
                        {
                            aux = SnakeRecursive2(pathSoFar, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 + 1));
                            if (aux == null)
                            {
                                pathSoFar.Remove(pathSoFar.Single(x => x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)));
                                return pathSoFar;
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
            else
            {
                return null;
            }
        }
        #endregion
    }
}
