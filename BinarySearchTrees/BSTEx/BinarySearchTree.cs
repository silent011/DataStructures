using System;
using System.Collections.Generic;

public class BinarySearchTree<T>:IBinarySearchTree<T> where T : IComparable
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

    public T Ceiling(T val)
    {
        return Ceiling(val, Root);
    }

    private T Ceiling(T val, Node node)
    {
        if (node == null) return default(T);

        int nodeIsBigger = ComparedTo(node.Value, val);

        if(nodeIsBigger < 0)
        {
            return Ceiling(val, node.Right);
        }
        else if(nodeIsBigger > 0)
        {
            return Ceiling(val, node.Left);
        }
        else
        {
            if(node.Right == null)
            {
                return Ceiling(val, node.Left);
            }

            return node.Right.Value;
        }
    }

    private void Copy(Node root)
    {
        if (root == null) return;

        Insert(root.Value);
        Copy(root.Left);
        Copy(root.Right);
    }

    public int Count()
    {
        if (Root == null) return 0;

        return Root.Count;
    }

    public void Delete(T elem)
    {
        if (Root == null) return;

        Queue<Node> nodesCountToBeDec = new Queue<Node>();
        Node parent = null;
        Node current = Root;

        while(current != null)
        {
            int nodeIsBigger = ComparedTo(current.Value, elem);
            
            nodesCountToBeDec.Enqueue(current);


            if (nodeIsBigger > 0)
            {
                parent = current;
                current = current.Left;

            }
            else if (nodeIsBigger < 0)
            {
                parent = current;
                current = current.Right;
            }
            else break;
            
        }

        if(parent == null)
        {
            if (Root.Value.CompareTo(elem) == 0) Root = null;
            else return;
        }
        else
        {
            DeleteElem(elem, parent, current, nodesCountToBeDec);
        }
        
    }

    private void DeleteElem(T elem, Node parent, Node targetNode, Queue<Node> nodesCountToBeDec)
    {

        if(parent.Value.CompareTo(elem) > 0)
        {
            parent.Left = null;
        }
        else
        {
            parent.Right = null;
        }

        EachPreOrder(Insert, targetNode.Left);
        EachPreOrder(Insert, targetNode.Right);

        foreach (Node node in nodesCountToBeDec)
        {
            node.Count-=targetNode.Count;
        }
        
    }

    private void EachPreOrder(Action<T> action, Node node)
    {
        if (node == null) return;

        action(node.Value);
        EachPreOrder(action, node.Left);
        EachPreOrder(action, node.Right);
    }

    public void DeleteMax()
    {
        if (Root == null) throw new InvalidOperationException();

        Node parent = null;
        Node current = Root;
        while (current.Right != null)
        {
            parent = current;
            current = current.Right;
            parent.Count--;
        }

        if (parent == null) Root = Root.Left;
        else
        {
            parent.Right = current.Left;
        }
    }

    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new Node(value);
            return;
        }

        Queue<Node> nodesCountToBeIncreased = new Queue<Node>();
        Node current = Root;
        while (current != null)
        {
            int valueComparison = ComparedTo(current.Value, value);
            if (valueComparison > 0)
            {
                nodesCountToBeIncreased.Enqueue(current);
                if (current.Left == null)
                {
                    current.Left = new Node(value);
                    break;
                }
                
                current = current.Left;
            }
            else if (valueComparison < 0)
            {
                nodesCountToBeIncreased.Enqueue(current);
                if (current.Right == null)
                {
                    current.Right = new Node(value);
                    break;
                }

                current = current.Right;
            }
            else
            {
                //if there is a duplicate the function will exit without doing anything
                Console.WriteLine("Element already exists in this bst");
                return;
            }
        }

        //if there is a duplcate element this part won't be reached thanks to the return in the
        //else statement in the while loop
        foreach (Node node in nodesCountToBeIncreased)
        {
            node.Count++;
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
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
            parent.Count--;
        }

        if (parent == null) Root = Root.Right;
        else
        {
            parent.Left = current.Right;
        }
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

    public T Floor(T val)
    {
        return Floor(val, Root);
    }

    private T Floor(T val, Node node)
    {
        if(node == null)
        {
            return default(T);
        }

        int nodeIsBigger = ComparedTo(node.Value, val);
        if(nodeIsBigger > 0)
        {
            return Floor(val, node.Left);
        }
        else if(nodeIsBigger < 0)
        {
            return Floor(val, node.Right);
        }
        else
        {
            if (node.Left == null)
            {
                return Floor(val, node.Right);
            }
            else
            {
                return node.Left.Value;
            }
        }

       
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
        if (highValueComparison < 0)
        {
            Range(root.Right, q, minValue, maxValue);
        }

    }

    public int Rank(T value)
    {
        return Rank(value, Root);
    }

    private int Rank(T value, Node current)
    {
        if (current == null) return 0;

        int nodeIsBigger = ComparedTo(current.Value, value);

        if(nodeIsBigger >= 0)
        {
            return Rank(value, current.Left);
        }

        return 1 + Rank(value, current.Left) + Rank(value, current.Right);
    }

    public T Select(int n)
    {
        Stack<T> resultElems = new Stack<T>();
        resultElems.Push(default(T));

        Select(n, Root, resultElems);

        return resultElems.Pop();
    }

    private void Select(int n, Node node, Stack<T> resultElems)
    {
        if (node == null || resultElems.Count > 1) return;

        Select(n, node.Left, resultElems);

        int smallerCount = Rank(node.Value);
        if (smallerCount == n) resultElems.Push(node.Value);
        else Select(n, node.Right, resultElems);
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
                Count = 1;
            }
        }

        public T Value;
        public Node Left;
        public Node Right;

        public int Count;
    }
}

public class Launcher
{
    public static void Main()
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
        //bst.Insert(15);
        //bst.Insert(13);
        //bst.Insert(20);
        //bst.Insert(11);
        //bst.Insert(13);
        //bst.Insert(34);

        //bst.Print();
        //Console.WriteLine(bst.Count());

        //    bst.Insert(12);
        //    bst.Insert(11);
        //    bst.Print();
        //    Console.WriteLine(bst.Count());

        //    bst.DeleteMin();
        //    Console.WriteLine(bst.Count());
        //    bst.DeleteMax();
        //    Console.WriteLine(bst.Count());

        //Console.WriteLine(bst.Rank(40));
        //Console.WriteLine(bst.Rank(5));

        //Console.WriteLine(bst.Select(3));

        //bst.Delete(5);
        //bst.Print();
        //Console.WriteLine(bst.Count());

    }
}