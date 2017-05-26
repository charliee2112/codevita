using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeVIta
{
    public static class ChaosInTheHangar
    {
        #region Snake

        public static void TestHangar(string input, string output)
        {
            string[] cases = output.Split('\n').ToList().Where(x => !x.Equals("")).ToArray();
            string[] inputs = input.Substring(0, input.Length - 1).Split(';');
            int inputindex = 0;
            cases.ToList().ForEach(path=> {
                TestPaths(inputs[inputindex++],path);
            });
            
            Console.WriteLine();
        }

        public static void TestPaths(string input, string outputPath)
        {
            //Parsing

            List<List<Tuple<int, int>>> table = new List<List<Tuple<int, int>>>();

            List<string> planes = outputPath.Split(':').Skip(1).ToList();
            List<string> steps = new List<string>();
            planes.ForEach(x => steps.Add(x.Substring(0,x.Length-2)));

            steps.ForEach(plane => {
                List<Tuple<int, int>> aux = new List<Tuple<int, int>>();
                    plane.Split(';').ToList().ForEach(step => {
                        if (step.Length > 1)
                        {
                            aux.Add(new Tuple<int, int>((step[1] - '0'), (step[3] - '0')));
                        }
                    }
                );
                table.Add(aux);
            });
            List<int> pathlengths = new List<int>();
            table.ForEach(x => pathlengths.Add(x.Count));
            int finaltime = pathlengths.Max();

            bool valid = true;

            for (int i = 0; i < finaltime; i++)
            {
                List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
                for(int j = 0; j < table.Count; j++)
                {                    
                    if (i<table[j].Count) //Here I no longer take those planes that have already reached the Hangar
                    {
                        positions.Add(new Tuple<int, int>(table[j][i].Item1, table[j][i].Item2));
                    }
                }
                valid &= positions.Count == positions.Distinct().Count();
            }
            
            //Check if each is a path with only horizontal and vertical moves

            table.ForEach(path => {
                for (int i = 1; i < path.Count; i++)
                {
                    if (!(path[i - 1].Item1 - path[i].Item1 == 0 && Math.Abs(path[i - 1].Item2 - path[i].Item2) == 1
                    ||  path[i - 1].Item2 - path[i].Item2 == 0 && Math.Abs(path[i - 1].Item1 - path[i].Item1) == 1))
                    {
                        valid = false;
                    }
                }
            });

            //Check each plane never repeats a step

            if (valid)
            {
                table.ForEach(path => {
                    if(path.Count != path.Distinct().Count())
                    {
                        valid = false;
                    }
                });
            }

            //Check ending and beginning of each plane

            if (valid)
            {
                int planeindex = 2;
                table.ForEach(path =>
                {
                    int planeInputX = Convert.ToInt32(input.Split(',')[planeindex]);
                    int planeInputY = Convert.ToInt32(input.Split(',')[planeindex + 1]);
                    int hangarInputX = Convert.ToInt32(input.Split(',')[0]);
                    int hangarInputY = Convert.ToInt32(input.Split(',')[0]);
                    int firstStepOutputX = path[0].Item1;
                    int firstStepOutputY = path[0].Item2;
                    int lastStepOutputX = path[path.Count - 1].Item1;
                    int lastStepOutputY = path[path.Count - 1].Item2;                    
                    if(planeInputX != firstStepOutputX && planeInputY != firstStepOutputY
                    && hangarInputX != lastStepOutputX && hangarInputY != lastStepOutputY)
                    {
                        valid = false;
                    }
                });
            }            
            Console.WriteLine(valid);
        }

        public static string Hangar(string data)
        {
            string ret = "";
            
            string[] hangar = data.Split(',').Take(2).ToArray();
            List<Tuple<int, int>> aux = SnakeJump(hangar);
            List<List<Tuple<int, int>>> table = new List<List<Tuple<int, int>>>();
            for (int i = 2; i < data.Split(',').Length - 1; i += 2)
            {
                int index = aux.IndexOf(new Tuple<int, int>(int.Parse(data.Split(',')[i]), int.Parse(data.Split(',')[i + 1])));
                table.Add(aux.Take(index-1).Reverse().ToList());
            }
                        
            int j = 0;
            table.ForEach(x =>
            {
                ret+=((j++) + ": ");
                x.ForEach(y =>
                {
                    ret+=(y.Item1 + "," + y.Item2 + "; ");
                });
            });
            ret += "\n";
            return ret;
        }

        public static string HangarTestCases()
        {
            string ret ="";
            List<string> data = new List<string>();
            data.Add("2,2,2,1,2,0,1,1,1,2");
            data.Add("3,3,0,0");
            data.ToList().ForEach(x =>
            {
                ret += (x + ";");
            });
            
            return ret;
        }

        //No length is bigger or equal to the sum of all lengths
        public static List<Tuple<int, int>> SnakeJump(string[] input)
        {
            Tuple<int, int> hangar = new Tuple<int, int>(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]));
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

        public static List<Tuple<int, int>> SnakeRecursive1(List<Tuple<int, int>> pathSoFar, Tuple<int, int> nextStep)
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

        public static List<Tuple<int, int>> SnakeRecursive2(List<Tuple<int, int>> pathSoFar, Tuple<int, int> nextStep)
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
