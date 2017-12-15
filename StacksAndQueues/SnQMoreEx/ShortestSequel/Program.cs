using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            int[] input = Console.ReadLine().Split(' ')
                .Select(x => int.Parse(x)).ToArray();
            int n = input[0];
            int m = input[1];

            Queue<Node> nums = new Queue<Node>();
            Node initialNode = new Node(n);
            nums.Enqueue(initialNode);

            while(nums.Count > 0)
            {
                Node node = nums.Dequeue();
                if(node.Value == m)
                {
                    Console.WriteLine(String.Join(" -> ",
                    node.History.ToArray()));
                    break;
                }

                Node node1 = node.Copy();
                node1.Value++;
                if(node1.Value <= m)
                {
                    
                    node1.History.Enqueue(node1.Value);
                    nums.Enqueue(node1);
                }

                Node node2 = node.Copy();
                node2.Value += 2;
                if(node2.Value <= m)
                {
                    node2.History.Enqueue(node2.Value);
                    nums.Enqueue(node2);
                }

                Node node3 = node.Copy();
                node3.Value *= 2;
                if (node3.Value <= m)
                {
                    node3.History.Enqueue(node3.Value);
                    nums.Enqueue(node3);
                }

            }

          
        }

    }

    public class Node
    {
        public int Value;
        public Queue<int> History = new Queue<int>(); // can use
        // just a prevNode and make it
        // like linked list but w/e.
        //No interpolation at the end at least.

        public Node(int element)
        {
            Value = element;
            History.Enqueue(element);
        }

        public Node Copy()
        {
            Node copy = new Node(0);
            copy.Value = Value;
            copy.History = new Queue<int>(History.ToArray());

            return copy;
        }
    }
}

