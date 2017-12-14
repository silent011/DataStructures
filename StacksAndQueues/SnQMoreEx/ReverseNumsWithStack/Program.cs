using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine().Split(' ')
                                        .Select(s => int.Parse(s))
                                        .ToArray();
        Stack<int> numsStack = new Stack<int>(nums);

        //not using pop
        foreach (int num in numsStack)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}
