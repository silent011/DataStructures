using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            string[] input = Console.ReadLine().Split(' ');
            string command = input[0];
            if (command == "start")
            {
                SweepAndPrune.Start();
                break;
            }

            string name = input[1];
            int x1 = int.Parse(input[2]);
            int y1 = int.Parse(input[3]);
            SweepAndPrune.Add(name, x1, y1);
        }


        
    }
}

