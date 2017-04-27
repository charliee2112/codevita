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
                    Console.WriteLine("2-> Other");
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
                        case 'x':
                            { }
                            break;
                        case 'X':
                            { }
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

        #region Polygons

        private static void RunPolygonsExamples()
        {
            #region data
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
            #endregion
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
            #region data
            List<int>[] data = {
                new List<int>{ 1, 1, 12, 13, 4, 14, 3, 7, 4, 2, 3, 3, 12 },
            };
            data.ToList().ForEach(x =>
            {
                Console.WriteLine("For input: ");
                ShowSupermanInputs(x);
                Console.WriteLine("The output is: ");
                Superman(x);
            });
            #endregion
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
            for (int i = 4; i < x.Count-1; i += 2)
            {
                obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2]));

                obstacles.Add(new Tuple<int, int>(x[i + 1]-1, x[i + 2]-1));
                obstacles.Add(new Tuple<int, int>(x[i + 1]+1, x[i + 2]+1));

                obstacles.Add(new Tuple<int, int>(x[i + 1]-1, x[i + 2]+1));
                obstacles.Add(new Tuple<int, int>(x[i + 1]-1, x[i + 2]+1));

                obstacles.Add(new Tuple<int, int>(x[i + 1]+1, x[i + 2]));
                obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2]+1));

                obstacles.Add(new Tuple<int, int>(x[i + 1], x[i + 2]-1));
                obstacles.Add(new Tuple<int, int>(x[i + 1]-1, x[i + 2]));
            }
            Tuple<int, int> superman = new Tuple<int, int>(x[0], x[1]);
            Tuple<int, int> ship = new Tuple<int, int>(x[2], x[3]);
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            SupermanRecursive(result, ship, obstacles, superman);
            Console.WriteLine("The steps to follow in this case are: ");
            Console.Write("{ ");
            int a = 1;
            result.ForEach(step => {
                if (a % 5 == 0) Console.WriteLine();
                Console.Write("(" + step.Item1 + "," + step.Item2 + ") ");
                a++;
            });
            Console.Write("}");
            Console.WriteLine();
        }

        //Clumsy Superman, sticks to obstacles and to borders and already walked paths... Like some drunk guy, afflicted by k
        private static List<Tuple<int, int>> SupermanRecursive(List<Tuple<int, int>> pathSoFar, Tuple<int, int> ship, List<Tuple<int, int>> obstacles, Tuple<int, int> nextStep){
            bool isValidStep = (nextStep.Item1 > -1 && nextStep.Item2 > -1 && nextStep.Item2 < 16 && nextStep.Item1 < 16) && pathSoFar.Where(x=> x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)).Count()==0;
            //Can I actually take that step?
            obstacles.ForEach(x => { if (x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)) isValidStep = false; });
            if (isValidStep){
                pathSoFar.Add(nextStep);
                if (ship.Item1.Equals(nextStep.Item1) && ship.Item2.Equals(nextStep.Item2))
                {//This is in case we reached the ship successfully
                    return pathSoFar;
                }else{//And here I did not yet reach my destination, I need to take another step. The first that comes up with a solution will be good enough
                    List<Tuple<int, int>> aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1 + 1, nextStep.Item2));
                    if (aux == null){
                        aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 + 1));
                        if (aux == null){
                            aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1 - 1, nextStep.Item2));
                            if (aux == null){
                                aux = SupermanRecursive(pathSoFar, ship, obstacles, new Tuple<int, int>(nextStep.Item1, nextStep.Item2 - 1));
                                if (aux == null){
                                    pathSoFar.Remove(pathSoFar.Single(x => x.Item1.Equals(nextStep.Item1) && x.Item2.Equals(nextStep.Item2)));
                                    return null;
                                }else{
                                    pathSoFar = aux;
                                }
                            }else{
                                pathSoFar = aux;
                            }
                        }else{
                            pathSoFar = aux;
                        }
                    }else{
                        pathSoFar = aux;
                    }
                }
                return pathSoFar;
            }else{
                return null;
            }
        }        
        #endregion
    }
}
