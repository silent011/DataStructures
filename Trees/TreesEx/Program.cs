using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
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

        Stack<int> longestPath = TreeMethods.LongestPath(tree);
        Console.WriteLine("Longest path: " + string.Join(" ",
            longestPath));
    }
}
