using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
        //var q = new LinkedQueue<int>();

        //for(int i = 1; i <= 8; i++)
        //{
        //    q.Enqueue(i);
        //}

        //ShowInfo(q);

        //int val = q.Dequeue();
        //ShowInfo(q);
    }

    public static void ShowInfo(LinkedQueue<int> stack)
    {
        Console.WriteLine("Count: " + stack.Count);
        Console.WriteLine("Array: " + String.Join(", ", stack.ToArray()));
    }
}
//doubly linked queue
public class LinkedQueue<T>
{
    public int Count { get; private set; }
    private Node<T> Head;
    private Node<T> Tail;

    public void Enqueue(T element)
    {
        Node<T> newEntry = new Node<T>(element);
        if(Count == 0)
        {
            Head = Tail = newEntry;
        }
        else if (Count == 1)
        {
            Head.nextNode = newEntry;
            newEntry.prevNode = Head;
            Tail = newEntry;
        }
        else
        {
            Tail.nextNode = newEntry;
            newEntry.prevNode = Tail;
            Tail = newEntry;
        }
        Count++;
    }

    public T Dequeue()
    {
        if(Count == 0)
        {
            throw new InvalidOperationException();
        }

        T value = Head.Value;
        Node<T> newHead = Head.nextNode;

        newHead.prevNode = null;
        Head = newHead;

        Count--;

        return value;
    }

    public T[] ToArray()
    {
        T[] arr = new T[Count];

        Node <T>current = Head;
        int counter = 0;
        while(current != null)
        {
            arr[counter] = current.Value;
            current = current.nextNode;
            counter++;
        }

        return arr;
    }

    private class Node<T>
    {
        public T Value;
        public Node<T> nextNode;
        public Node<T> prevNode;

        public Node(T element)
        {
            Value = element;
        }
    }
}

