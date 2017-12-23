using System;
using System.Collections.Generic;
using System.Linq;

public class Tree<T>
{
    public T Value;
    public Tree<T> Parent { get; private set; }
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

    public void TreeGrow(T[][] entries)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            T[] entry = entries[i];
            if(Compare(entry[0], Value))
            {
                if(entry.Length > 1)
                {
                    Tree<T> child = new Tree<T>(entry[1]);
                    child.Parent = this;
                    Children.Add(child);
                }

            }
          
        }
        
        foreach(var child in Children)
        {
            child.TreeGrow(entries);
        }

    }

    private bool Compare<T>(T x, T y) 
    {
        return EqualityComparer<T>.Default.Equals(x, y);
    }

    public void GetLeafs(List<T> leafsList)
    {
        if(Children.Count == 0)
        {
            leafsList.Add(Value);
        } 
        else
        {
            foreach (var child in Children)
            {
                child.GetLeafs(leafsList);
            }
        }
    }

    public void EachNodeBFS(Action<Tree<T>> action)
    {
        Queue<Tree<T>> q = new Queue<Tree<T>>();
        q.Enqueue(this);
        action(this);

        while(q.Count > 0)
        {
            Tree<T> current = q.Dequeue();
            foreach (var child in current.Children)
            {
                action(child);
                q.Enqueue(child);
            }
        }
    }

}
