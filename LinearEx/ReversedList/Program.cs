using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var reversed = new ReversedList<int>();
        reversed.Add(2);
        reversed.Add(2);
        reversed.Add(3);
        reversed.Add(4);

        reversed.Add(5);
        reversed.Add(6);
        reversed.RemoveAt(4);
        reversed[4] = 6;
        //reversed.RemoveAt(4);
        //reversed.RemoveAt(3);
        //reversed.RemoveAt(2);
        foreach (var item in reversed)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine($"capacity -> {reversed.Capacity}");
        Console.WriteLine($"item at 3 -> {reversed[3]} ");
        Console.WriteLine($"item at 2 -> {reversed[2]} ");
    }
}
