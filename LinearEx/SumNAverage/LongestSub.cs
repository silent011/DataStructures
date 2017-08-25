using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
    class LongestSub
    {
        public static void startClass()
        {
            List<int> input = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList<int>();
            List<int> longest = new List<int>();
            List<int> temp = new List<int>();

            longest.Add(input[0]);
            temp.Add(input[0]);
            for (int i = 0; i < input.Count-1; i++)
            {
                if(input[i] == input[i + 1])
                {
                    temp.Add(input[i + 1]);

                    if (temp.Count > longest.Count)
                    {
                        longest = new List<int>(temp);
                    }
                }
                else
                {
                    temp.Clear();
                    temp.Add(input[i + 1]);
                }
            }

            Console.WriteLine(String.Join(" ", longest));

        }
    }
}
