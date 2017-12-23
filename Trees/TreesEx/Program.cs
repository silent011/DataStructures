using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            var tree = TreeMethods.Read();
            //Console.WriteLine("Root node: " + tree.Value);

            //tree.Print();
            //List<int> list = new List<int>();
            //tree.GetLeafs(list);
            //list.Sort();
            //Console.WriteLine("Leaf nodes: " +
            //    string.Join(" ", list.ToArray()));

            //List<int> middleNodes = TreeMethods.GetMiddleList(tree);
            //middleNodes.Sort();
            //Console.WriteLine("Middle nodes: " + string.Join(" ",
            //    middleNodes));

            //Tree<int> deepestNode = TreeMethods.FindDeepestNode(tree);
            //Console.WriteLine("Deepest node: " + deepestNode.Value);

            //Stack<int> longestPath = TreeMethods.LongestPath(tree);
            //Console.WriteLine("Longest path: " + string.Join(" ",
            //    longestPath));


            //int sum = int.Parse(Console.ReadLine());
            //List<Stack<int>> pathsWithSum = TreeMethods.AllPathsWithSum(tree, sum);

            //Console.WriteLine("Paths of sum " + sum + ":");
            //foreach (var path in pathsWithSum)
            //{
            //    Console.WriteLine(string.Join(" ", path));
            //}

            //int sum = int.Parse(Console.ReadLine());
            //List<Stack<int>> pathsWithSum = TreeMethods.SubtreesWithSum(tree, sum);

            //Console.WriteLine("Paths of sum " + sum + ":");
            //foreach (var path in pathsWithSum)
            //{
            //    Console.WriteLine(string.Join(" ", path));
            //}

            int sum = int.Parse(Console.ReadLine());
            List<List<int>> subtrees = TreeMethods.SubtreesWithSum(tree, sum);

            Console.WriteLine("Subtrees of sum " + sum + ":");
            foreach (var subtree in subtrees)
            {
                Console.WriteLine(string.Join(" ", subtree));
            }
        }
    }
}
