using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
    class SumNAverageClass
    {
        public static void startSum()
        {
            string[] strInput = Console.ReadLine().Split(' ');
            List<int> input = Array.ConvertAll(strInput, item => int.Parse(item)).ToList<int>();

            Console.WriteLine($"Sum={input.Sum()}; Average={input.Average().ToString("0.00")}");
        }
    }
}
