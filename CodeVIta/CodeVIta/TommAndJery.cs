using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVIta
{
    public static class TommAndJery
    {
        public static string TJTestCases()
        {
            string ret = "";
            string[] data = {
                "A,Z,12,24,A,B,C,D,E,F,G,H,I,J,K,Z," +
                "A,B,B,A,A,D,D,A,A,C,C,A,F,D,D,F," +
                "F,E,E,F,F,G,G,F,C,E,E,C,I,E,E,I," +
                "J,K,K,J,J,I,I,J,K,H,H,K,K,Z,Z,K",

            };
            data.ToList().ForEach(x =>
            {
                x.Split(',').ToList().ForEach(y =>
                {
                    ret += (y + ",");
                });
            });
            return ret;
        }

        public static void TestTJ(string input, string output)
        {
            output = new string(output.Take(output.Count() - 1).ToArray());
            string[] dataInput = input.Split(',');
            List<Tuple<string, string>> inputJumps = new List<Tuple<string, string>>();
            List<string> platforms = new List<string>();
            string jery = dataInput[0];
            string cheese = dataInput[1];
            int platformsAmount = Convert.ToInt16(dataInput[2]);
            int totalJumps = Convert.ToInt16(dataInput[3]);

            for (int i = 4; i < platformsAmount + 4; i++)
            {
                platforms.Add(dataInput[i]);
            }
            for (int i = 4 + platformsAmount; i < dataInput.Length - 1; i += 2)
            {
                inputJumps.Add(new Tuple<string, string>(dataInput[i], dataInput[i + 1]));
            }

            List<Tuple<string, string>> outputJumps = new List<Tuple<string, string>>();
            for (int i = 2; i < output.Split(' ').Length - 2; i+=2)
            {
                outputJumps.Add(new Tuple<string, string>(output.Split(' ')[i], output.Split(' ')[i + 2]));
            }

            bool valid = true;

            //Check every step exists within the input and that Jery does not step Tomm
            outputJumps.ForEach(jump =>
            {
                if (!inputJumps.Contains(jump))
                {
                    valid = false;
                }
            });

            //Check you dont repeat any step
            if (outputJumps.Count != outputJumps.Distinct().Count())
            {
                valid = false;
            }

            //Check beginning and ending
            if (!(outputJumps.First().Item1.Equals(jery) && (outputJumps.Last().Item2.Equals(cheese))))
            {
                valid = false;
            }

            if (valid)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
            Console.WriteLine();
        }

        public static string TJ(string input)
        {
            string ret = "";
            string[] data = input.Split(',');
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
            TJRecursive("", lex, jumps, result, new Tuple<string, string>("", superman));
            result.Add(new Tuple<string, string>(result.Last().Item2, lex));
            result.ForEach(x =>
            {
                ret += ("  " + x.Item2);
            });
            ret += "\n";
            return ret;
        }

        public static List<Tuple<string, string>> TJRecursive(string currentPlatform, string finalPlatform, List<Tuple<string, string>> possibleJumps, List<Tuple<string, string>> path, Tuple<string, string> lastJump)
        {
            if (path.Where(x => x.Item2.Equals(lastJump.Item2)).Count() > 0)
            {
                path.Remove(path.Last());
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
                        aux = TJRecursive(lastJump.Item2, finalPlatform, possibleJumps, path, immediateJumps[i]);
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
                    return null;
                }
            }
        }
    }
}
