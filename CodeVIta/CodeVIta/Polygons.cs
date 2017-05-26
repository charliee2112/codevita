using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeVIta
{
    public static class FormingFigures
    {
        #region Polygons

        public static void TestPolygon(string input, string output)
        {
            string[] cases = output.Split('\n').ToList().Where(x => !x.Equals("")).ToArray();
            string[] inputs = input.Split(';').Take(input.Split(';').Length - 1).ToArray();
            string[] expected = new string[cases.Length];
            int index = 0;
            inputs.ToList().ForEach(x => expected[index++] = Polygons(x).Trim());
            index = 0;
            cases.ToList().ForEach(x => { if (x.Equals(expected[index++])) { Console.WriteLine("YES"); } else { Console.WriteLine("NO"); } });
            Console.WriteLine();
        }

        public static string PolygonTestCases()
        {
            string ret = "";
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
                x.ForEach(y => {
                    ret += (y + ",");
                });
                ret = new string(ret.Take(ret.Count() - 1).ToArray());
                ret += (";");
            });
            return ret;
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
        public static string Polygons(string input)
        {
            int amount = input.Split(',').Length;
            int[] lengths = new int[amount];
            int index = 0;
            input.Split(',').ToList().ForEach(x => { lengths[index++] = int.Parse(x); });
            string ret = "";
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
                ret += ("YES\n");
            }
            else
            {
                ret += ("NO\n");
            }
            return ret + "\n";
        }

        #endregion

    }
}
