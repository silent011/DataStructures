﻿using System;
using System.Collections.Generic;

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

    public void Test()
    {
        Node<T> item = root.Left;
        root = null;
        Console.WriteLine(item.Value);
    }

    public void Delete(T item)
    {
        root = Delete(root,item);
    }

    private Node<T> Delete(Node<T> node, T item)
    {
        if (node == null) return null;

        int compare = node.Value.CompareTo(item);
        if(compare < 0)
        {
             node.Right = Delete(node.Right, item);
        }
        else if(compare > 0)
        {
             node.Left = Delete(node.Left, item);
        }
        else
        {
            if (node.Right == null) return node.Left;
            if (node.Left == null) return node.Right;

            Node<T> minNode = FindLeftMost(node.Right);
            node.Right = DeleteMin(node.Right);
            minNode.Left = node.Left;
            minNode.Right = node.Right;
            UpdateHeight(minNode);

            return minNode;
        }

        node = Balance(node);
        UpdateHeight(node);

        return node;
    }

    private Node<T> FindLeftMost(Node<T> node)
    {
        if (node == null) return null;
        if (node.Left == null) return node;

        return FindLeftMost(node.Left);
    }

    private Node<T> FindRightMost(Node<T> node)
    {
        if (node == null) return null;
        if (node.Right == null) return node;

        return FindRightMost(node.Right);
    }

    public void DeleteMin()
    {
        root = DeleteMin(root);
    }

    private Node<T> DeleteMin(Node<T> node)
    {
        if (node == null) return null;

        if (node.Left != null) node.Left = DeleteMin(node.Left);
        else return node.Right;

        node = Balance(node);
        UpdateHeight(node);

        return node;
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

    private Node<T> Balance(Node<T> node)
    {
        var balance = Height(node.Left) - Height(node.Right);
        if (balance > 1)
        {
            var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
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

    private void EachPreOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        action(node.Value);
        this.EachPreOrder(node.Left, action);
        this.EachPreOrder(node.Right, action);
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

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }
}
