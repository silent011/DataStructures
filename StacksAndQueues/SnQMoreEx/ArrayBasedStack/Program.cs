using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        //unit tests another day >:)
        var ArrSt = new ArrayStack<int>(4);

        ArrSt.Push(1);
        ArrSt.Push(2);
        ArrSt.Push(3);
        ArrSt.Push(4);

        ShowInfo(ArrSt);

        ArrSt.Push(5);
        ShowInfo(ArrSt);

        ArrSt.Pop();
        ShowInfo(ArrSt);

        ArrSt.Pop(); ArrSt.Pop();
        ShowInfo(ArrSt);

        ArrSt.Pop(); ArrSt.Pop();
        ShowInfo(ArrSt);
        ArrSt.Push(-8);
        ShowInfo(ArrSt);

        int pop = ArrSt.Pop();

        Console.WriteLine("pop" + pop);

        for(int i = 0; i< 500; i++)
        {
            ArrSt.Push(i);
        }

        ShowInfo(ArrSt);

        for (int i = 0; i < 250; i++)
        {
            ArrSt.Pop();
        }

        ShowInfo(ArrSt);
    }

    private static void ShowInfo(ArrayStack<int> ArrSt)
    {
        Console.WriteLine("Count: " + ArrSt.Count);
        Console.WriteLine("Arr: "
                            + String.Join(", ", ArrSt.ToArray()));
    }
}

public class ArrayStack<T>
{
    private T[] elements;
    public int Count { get; private set; }
    private const int InitialCapacity = 16;

    public ArrayStack(int capacity = InitialCapacity)
    {
        elements = new T[capacity];
    }

    public void Push(T element)
    {
        if (Count == elements.Length)
        {
            Resize();
        }

        elements[Count] = element;
        Count++;
    }

    public T Pop()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException();
        }

        T[] newArr = new T[Count - 1];
        T oldValue = elements[Count - 1];
       
        Array.Copy(elements, newArr, Count - 1);
        Count--;

        return oldValue;
    }

    public T[] ToArray()
    {
        T[] newArr = new T[Count];
        
        for(int i = Count - 1; i >= 0; i--)
        {
            newArr[Count - i - 1] = elements[i];
        }

        return newArr;
    }

    private void Resize()
    {
        elements = elements.Concat(new T[Count]).ToArray();
    }
}

