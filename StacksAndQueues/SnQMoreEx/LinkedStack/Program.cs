using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
        //var stack = new LinkedStack<int>();

        //stack.Push(2);
        //ShowInfo(stack);

        //stack.Push(3);
        //stack.Push(4);
        //stack.Push(5);
        //ShowInfo(stack);
    }

    public static void ShowInfo(LinkedStack<int> stack)
    {
        Console.WriteLine("Count: " + stack.Count);
        Console.WriteLine("Array: " + String.Join(", ", stack.ToArray()));
    }
}

public class LinkedStack<T>
{
    public int Count { get; private set; }
    private Node<T> Tail;

    public void Push(T element)
    {
        Node<T> newEntry = new Node<T>(element);
        if(Count == 0)
        {
            Tail = newEntry;
        }
        else
        {
            newEntry.prevNode = Tail;
            Tail = newEntry;
        }
        Count++;
    }

    public T Pop()
    {
        if(Count == 0)
        {
            throw new InvalidOperationException();
        }

        T oldVal = Tail.Value;

        Tail = Tail.prevNode;
        Count--;

        return oldVal;
    }

    public T[] ToArray()
    {
        T[] arr = new T[Count];
        Node<T> current = Tail;
        int counter = 0;

        while(current != null)
        {
            arr[counter] = current.Value;
            counter++;
            current = current.prevNode;
        }

        return arr;
    }

    private class Node<T>
    {
        public T Value;
        public Node<T> prevNode;

        public Node(T element)
        {
            Value = element;
        }
    }
}

