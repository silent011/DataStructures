using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
    class RemoveOdd
    {
        public static void startClass()
        {
            List<int> input = Console.ReadLine().Split(' ')
                .Select(x => int.Parse(x)).ToList<int>();
            Dictionary<int, int> nums = new Dictionary<int, int>();

            foreach (int num in input)
            {
                if (nums.ContainsKey(num))
                {
                    nums[num]++;
                }
                else
                {
                    nums.Add(num, 1);
                }
            }

            foreach (var kvp in nums)
            {
                if(kvp.Value % 2 != 0)
                {
                    input.RemoveAll(x => x == kvp.Key);
                }
            }

            Console.WriteLine(String.Join(" ", input));
        }
    }
}
