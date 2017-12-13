using System;
using System.Linq;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;

    public int Count { get; private set; }

    private T[] elements;

    private int startIndex = 0;

    private int endIndex = 0;

    public CircularQueue(int capacity = DefaultCapacity)
    {
        elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if(Count == elements.Length)
        {
            Resize();
        }

        elements[endIndex % elements.Length] = element;
        endIndex++;
        Count++;
    }

    private void Resize()
    {
        T[] newArr = new T[Count * 2];

        CopyAllElements(newArr);

    }

    private void CopyAllElements(T[] newArray)
    {
        //to keep the starting position of the elements the same in the new array. Shows the circularity when
        // written in the console so that's why i do it like this.
       for (int i = 0; i < Count; i++)
        {
            int elIndex = (startIndex + i) % elements.Length;
            int newArrIndex = (startIndex + i) % newArray.Length;

            newArray[newArrIndex] = elements[elIndex];
        }

        endIndex = (startIndex + Count) % newArray.Length;
        elements = newArray;
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if(Count == 0)
        {
            throw new InvalidOperationException();
        }

        // the value of the element about to be removed
        T oldValue = elements[startIndex];

        startIndex = (startIndex + 1) % elements.Length;

        // initializing a new empty array which is going to be filled with the elements without the dequeued one.
        T[] newArr = new T[elements.Length];

        Count--;
        CopyAllElements(newArr);

        return oldValue;
    }

    public T[] ToArray()
    {
        T[] newArr = new T[Count];

        //difference here with the CopyAllMethod's loop is that here the elements in the newArray start at
        // the 0 index but not at the same index they had been in the old array. Also by this method any excessive
        // empty cells are removed.
        for(int i = 0; i < Count; i++)
        {
            int index = (startIndex + i) % elements.Length;
            newArr[i] = elements[index];
        }

        return newArr;
    }
}


public class Example
{
    public static void Main()
    {
        // some manual tests
        //CircularQueue<int> queue = new CircularQueue<int>();

        //queue.Enqueue(1);
        //queue.Enqueue(2);
        //queue.Enqueue(3);
        //queue.Enqueue(4);
        //queue.Enqueue(5);
        //queue.Enqueue(6);

        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //int first = queue.Dequeue();
        //Console.WriteLine("First = {0}", first);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //queue.Enqueue(-7);
        //queue.Enqueue(-8);
        //queue.Enqueue(-9);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //first = queue.Dequeue();
        //Console.WriteLine("First = {0}", first);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //queue.Enqueue(-10);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //first = queue.Dequeue();
        //Console.WriteLine("First = {0}", first);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //int[] array = Enumerable.Range(1, 500).ToArray();
        //CircularQueue<int> queue2 = new CircularQueue<int>();

        //// Act
        //for (int i = 0; i < array.Length; i++)
        //{
        //    queue2.Enqueue(array[i]);
        //}
        //int[] arrayFromQueue = queue2.ToArray();
        //Console.WriteLine("Q length:" + arrayFromQueue.Length);
        //Console.WriteLine("Q Count:" + queue2.Count);
        //Console.WriteLine("Array length:" + array.Length);
        //Console.WriteLine(String.Join(", ", arrayFromQueue));
    }
}
