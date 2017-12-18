using System;
using System.Collections.Generic;
using System.Linq;


class TreeMethods
{
    public static Tree<int> Read()
    {
        int N = int.Parse(Console.ReadLine());

       int[][] entries = new int[N - 1][];

        for (int i = 0; i < N - 1; i++)
        {
            int[] entryArr = Console.ReadLine().Split(' ').
                Select(x => int.Parse(x)).ToArray();

            entries[i] = entryArr;
        }

        int[] firstEntry = entries.First();
        Tree<int> tree = new Tree<int>(firstEntry[0]);

        tree.TreeGrow(entries);

        return tree;
    }

    public static List<int> GetMiddleList(Tree<int> tree)
    {
        List<int> list = new List<int>();

        tree.EachNodeBFS(AddToList);

        void AddToList(Tree<int> node)
        {
            if (node.Parent != null && node.Children.Count > 0)
                list.Add(node.Value);
        }

        return list;
    }

    public static Tree<int> FindDeepestNode(Tree<int> tree)
    {
        int maxCounter = 0;
        Tree<int> deepest = tree;

        GoDeep(tree);

        void GoDeep(Tree<int> node, int count = 1)
        {
            if(node.Children.Count == 0 && count > maxCounter)
            {
                maxCounter = count;
                deepest = node;
            }
            else
            {
                foreach (var child in node.Children)
                {
                    GoDeep(child, count + 1);
                }
            }
        }

        return deepest;
        
    }

    public static Stack<int> LongestPath(Tree<int> tree)
    {
        Stack<int> path = new Stack<int>();
        Tree<int> deepestNode = TreeMethods.FindDeepestNode(tree);

        Tree<int> currentNode = deepestNode;
        while(currentNode != null)
        {
            path.Push(currentNode.Value);
            currentNode = currentNode.Parent;
        }

        return path;
    }

    public static List<Stack<int>> AllPathsWithSum(Tree<int> tree, int sum)
    {
        List<Stack<int>> sumPaths = new List<Stack<int>>();

        GoDeep(tree, tree.Value);

        void GoDeep(Tree<int> node,int nodeSum = 0)
        {
            if(node.Children.Count == 0 && sum == nodeSum)
            {
                Stack<int> nodePathStack = new Stack<int>();
                Tree<int> current = node;
                while(current != null)
                {
                    nodePathStack.Push(current.Value);
                    current = current.Parent;
                }
                sumPaths.Add(nodePathStack);
            }
            else
            {
                foreach (var child in node.Children)
                {
                    GoDeep(child, nodeSum + child.Value);
                }
            }
        }

        return sumPaths;
    }


    public static List<Stack<int>> SubtreesWithSum(Tree<int> tree,
                                                            int sum)
    {
        List<Stack<int>> sumPaths = new List<Stack<int>>();

        GoDeep(tree, tree.Value);

        void GoDeep(Tree<int> node, int nodeSum = 0)
        {
            if (sum == nodeSum)
            {
                Stack<int> nodePathStack = new Stack<int>();
                Tree<int> current = node;
                while (current != null)
                {
                    nodePathStack.Push(current.Value);
                    current = current.Parent;
                }
                sumPaths.Add(nodePathStack);
            }
            else
            {
                foreach (var child in node.Children)
                {
                    GoDeep(child, nodeSum + child.Value);
                }
            }
        }

        return sumPaths;
    }
}