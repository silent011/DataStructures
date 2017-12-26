using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node Root;

    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node node)
    {
        Copy(node);
    }

    private void Copy(Node node)
    {
        if(node == null)
        {
            return;
        }

        Insert(node.Value);
        Copy(node.Left);
        Copy(node.Right);
    }

    public void Insert(T value)
    {
        //if the binary tree does not have any nodes, node with this value shall be its first one
        if(Root == null)
        {
            Root = new Node(value);
            return;
        }

        bool isThere = false;
        bool shouldBeLeft = false; 

        Node parent = null;
        Node current = Root;

        //looks for appropriate empty node and checks if the value is already in existence
        while(current != null)
        {
            T val = current.Value;
            if(value.CompareTo(val) < 0)
            {
                if(current.Left == null)
                {
                    parent = current;
                    shouldBeLeft = true;
                    break;
                }
                else
                    current = current.Left;
            }
            else if(value.CompareTo(val) > 0)
            {
                if (current.Right == null)
                {
                    parent = current;
                    break;
                }
                else
                    current = current.Right;
            }
            else
            {
                isThere = true;
                break;
            }
        }

        //if there isn't a node with the same value
        if (!isThere)
        {
            if (shouldBeLeft)
                parent.Left = new Node(value);
            else
                parent.Right = new Node(value);
        }
    }

    public bool Contains(T value)
    {
        //nonrecursive implementation
        bool isThere = false;

        Node current = Root;
        while(current != null)
        {
            T currVal = current.Value;
            if(value.CompareTo(currVal) > 0)
                current = current.Right;
            else if(value.CompareTo(currVal) < 0)
                current = current.Left;
            else
            {
                isThere = true;
                break;
            }
        }

        return isThere;
    }

    public void DeleteMin()
    {
        if (Root == null) return;

        Node parent = null;
        var current = Root;
        while(current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null) Root = null;
        else parent.Left = current.Right; 
    }

    public BinarySearchTree<T> Search(T item)
    {
        var current = Root;
        while(current != null)
        {
            T val = current.Value;
            if(val.CompareTo(item) < 0)
            {
                current = current.Right;
            }
            else if(val.CompareTo(item) > 0)
            {
                current = current.Left;
            }
            else
            {
                break;
            }
        }

        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> q = new Queue<T>();

        Range(Root, q, startRange, endRange);

        return q;
    }

    private void Range(Node node, Queue<T> q, T startRange, T endRange)
    {
        if (node == null) return;

        int nodeInLower = startRange.CompareTo(node.Value);
        int nodeInHigher = endRange.CompareTo(node.Value);
       
        if(nodeInLower < 0)
        {
            Range(node.Left, q, startRange, endRange);
        }
        if(nodeInLower <=0 && nodeInHigher >= 0)
        {
            q.Enqueue(node.Value);
        }
        if (nodeInHigher > 0)
        {
            Range(node.Right, q, startRange, endRange);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        if(Root == null)
        {
            return;
        }

        GoDeep(Root);
        void GoDeep(Node root)
        {
            if (root.Left != null)
            {
                GoDeep(root.Left);
            }

            action(root.Value);

            if (root.Right != null)
            {
                GoDeep(root.Right);
            }

            
        }
    }

    private class Node
    {
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(T val, 
            Node left = null, Node right = null)
        {
            Value = val;
            Left = left;
            Right = right;
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        //var bst = new BinarySearchTree<int>();

        //bst.Insert(10);
        //bst.Insert(5);
        //bst.Insert(3);
        //bst.Insert(1);
        //bst.Insert(4);
        //bst.Insert(8);
        //bst.Insert(9);
        //bst.Insert(37);
        //bst.Insert(39);
        //bst.Insert(45);

        //// Act
        //BinarySearchTree<int> result = bst.Search(5);
        //List<int> nodes = new List<int>();
        //result.EachInOrder(nodes.Add);

        //Console.WriteLine(string.Join(" ", nodes));
    }
}
