using System;
using System.Collections.Generic;
using System.Linq;

public class Tree<T>
{
    public T Value;
    public List<Tree<T>> Children { get; private set; }

    public Tree(T value, params Tree<T>[] children)
    {
        Value = value;
        Children = new List<Tree<T>>(children);
    }

    public void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + Value);
        foreach(var child in Children)
        {
            child.Print(indent + 2);
        }
    }

    public void Each(Action<T> action)
    {
        action(Value);
        foreach (var child in Children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
       Queue<T> order = new Queue<T>();

        DFS(order.Enqueue);

        return order;
    }

    public void DFS(Action<T> action)
    {
        foreach (var child in Children)
        {
            child.DFS(action);
        }
        action(Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        List<T> order = new List<T>();

        Queue<Tree<T>> branchesQ = new Queue<Tree<T>>();
        branchesQ.Enqueue(this);
        order.Add(Value);
        
        while(branchesQ.Count > 0)
        {
            var current = branchesQ.Dequeue();

            foreach (var child in current.Children)
            {
                branchesQ.Enqueue(child);
                order.Add(child.Value);
            }
        }

        return order;
    }

    
}
