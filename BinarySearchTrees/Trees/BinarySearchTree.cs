using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node Root;

    public BinarySearchTree()
    {
        Root = null;
    }

    private BinarySearchTree(Node root)
    {
        Copy(root);
    }

    private void Copy(Node root)
    {
        if (root == null) return;

        Insert(root.Value);
        Copy(root.Left);
        Copy(root.Right);
    }

    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new Node(value);
            return;
        }

        Node current = Root;
        while (current != null)
        {
            int valueComparison = ComparedTo(current.Value, value);
            if (valueComparison > 0)
            {
                if (current.Left == null)
                {
                    current.Left = new Node(value);
                    break;
                }

                current = current.Left;
            }
            else if (valueComparison < 0)
            {
                if (current.Right == null)
                {
                    current.Right = new Node(value);
                }

                current = current.Right;
            }
            else
            {
                Console.WriteLine("element already exists in the tree");
                return;
            }
        }
    }

    public BinarySearchTree<T> Search(T value)
    {
        Node current = Root;
        while (current != null)
        {
            int valueCompared = ComparedTo(current.Value, value);
            if (valueCompared > 0) current = current.Left;
            else if (valueCompared < 0) current = current.Right;
            else break;
        }

        return new BinarySearchTree<T>(current);
    }


    public bool Contains(T value)
    {
        Node current = Root;
        while (current != null)
        {
            int valueComparison = ComparedTo(current.Value, value);

            if (valueComparison > 0) current = current.Left;
            else if (valueComparison < 0) current = current.Right;
            else return true;
        }

        return false;
    }

    public void DeleteMin()
    {
        if (Root == null) return;

        Node parent = null;
        Node current = Root;
        while(current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null) Root = Root.Right;
        else parent.Left = current.Right;
    }

    public void EachInOrder(Action<T> action)
    {
        if (Root == null)
        {
            Console.WriteLine("no root element");
            return;
        }

        EachInOrder(Root, action);
    }

    private void EachInOrder(Node currentNode, Action<T> action)
    {
        if (currentNode.Left != null) EachInOrder(currentNode.Left, action);
        action(currentNode.Value);
        if (currentNode.Right != null) EachInOrder(currentNode.Right, action);
    }

    public void Print()
    {
        if (Root == null) return;

        PrintRecursion(0, Root);
    }

    public IEnumerable<T> Range(T minValue, T maxValue)
    {
        Queue<T> q = new Queue<T>();

        Range(Root, q, minValue, maxValue);

        return q;
    }

    private void Range(Node root, Queue<T> q, T minValue, T maxValue)
    {
        if (root == null) return;
        int lowValueComparison = ComparedTo(root.Value, minValue);
        int highValueComparison = ComparedTo(root.Value, maxValue);

        if (lowValueComparison > 0)
        {
            Range(root.Left, q, minValue, maxValue);
        }
        if (lowValueComparison >= 0 && highValueComparison <= 0)
        {
            q.Enqueue(root.Value);
        }
        if(highValueComparison < 0)
        {
            Range(root.Right, q, minValue, maxValue);
        }

    }

    private int ComparedTo(T a, T b)
    {
        return a.CompareTo(b);
    }

    private void PrintRecursion(int indent, Node currentNode)
    {
        Console.WriteLine(new String(' ', indent) + currentNode.Value);
        if (currentNode.Left != null) PrintRecursion(indent + 2, currentNode.Left);
        if (currentNode.Right != null) PrintRecursion(indent + 2, currentNode.Right);
    }

    private class Node
    {
        public Node(T value)
        {
            if (value != null)
            {
                Value = value;
            }
        }

        public T Value;
        public Node Left;
        public Node Right;
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        bst.Print();
        Console.WriteLine(string.Join(", ", bst.Range(4, 37)));
    }
}
