using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
    class CountOccurences
    {
        public static void startClass()
        {
            int[] input = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            SortedDictionary<int, int> nums = new SortedDictionary<int, int>();
            foreach (int num in input)
            {
                if (nums.ContainsKey(num))
                    nums[num]++;
                else
                    nums.Add(num, 1);
            }

            foreach (KeyValuePair<int,int> kvp in nums)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value} times");
            }
        }
    }
}
