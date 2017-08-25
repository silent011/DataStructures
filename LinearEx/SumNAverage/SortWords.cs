using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
    class SortWords
    {
        static public void startClass()
        {
            List<string> input = Console.ReadLine().Split(' ').OrderBy(x => x).ToList<string>();
            Console.WriteLine(String.Join(" ", input));
        }
    }
}
