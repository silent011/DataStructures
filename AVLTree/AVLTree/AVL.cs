using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = Balance(node);
        UpdateHeight(node);

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private int Height(Node<T> node)
    {
        if (node == null) return 0;

        return node.Height;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> temp = node.Left;
        node.Left = temp.Right;
        temp.Right = node;

        UpdateHeight(node);

        return temp;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> temp = node.Right;
        node.Right = temp.Left;
        temp.Left = node;

        UpdateHeight(node);

        return temp;
    }

    private Node<T> Balance(Node<T> node)
    {
        int balance = Height(node.Left) - Height(node.Right);
        if(balance < -1)
        {
            balance = Height(node.Right.Left) - Height(node.Right.Right);
            if (balance <= 0) node = RotateLeft(node);
            else
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
            }
        }
        else if(balance > 1)
        {
            balance = Height(node.Left.Left) - Height(node.Left.Right);
            if (balance <= 0)
            {
                node.Left = RotateLeft(node.Left);
                node = RotateRight(node);
            }
            else
            {
                node = RotateRight(node);
            }
        }

        return node;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}
