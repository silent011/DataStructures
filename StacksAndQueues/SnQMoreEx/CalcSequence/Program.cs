using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // using only queues to output an array of numbers who follow a certain pattern
        int num = int.Parse(Console.ReadLine());
        Queue<int> seq = new Queue<int>(33);
        Queue<int> resultQ = new Queue<int>(50);
        seq.Enqueue(num);
        resultQ.Enqueue(num);

        int elToUse = seq.Dequeue();
        for(int i = 0; i < 16; i++)
        {
            int first = elToUse + 1;
            int sec = 2 * elToUse + 1;
            int third = first + 1;

            EnqueTheTwoQues(first, sec, third);

            elToUse = seq.Dequeue();
        }

        resultQ.Enqueue(elToUse + 1);

        Console.WriteLine(String.Join(", ", resultQ.ToArray()));

        void EnqueTheTwoQues(int first, int sec, int third)
        {
            seq.Enqueue(first); resultQ.Enqueue(first);
            seq.Enqueue(sec); resultQ.Enqueue(sec);
            seq.Enqueue(third); resultQ.Enqueue(third);
        }
    }
}
