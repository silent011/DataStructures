using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public void PrintHead()
    {
        Console.WriteLine(string.Join(" ", heap));
    }

    public BinaryHeap()
    {
        heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return heap.Count;
        }
    }

    public void Insert(T item)
    {
        heap.Add(item);
        BubbleUp(heap.Count - 1);
    }

    private void BubbleUp(int index)
    {
        while(index > 0 && isLess(Parent(index), index))
        {
            int parentIndex = Parent(index);
            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void Swap(int index, int parentIndex)
    {
        T token = heap[index];
        heap[index] = heap[parentIndex];
        heap[parentIndex] = token;
    }

    private bool isLess(int index, int index2)
    {
        return heap[index2].CompareTo(heap[index]) > 0;
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    public T Peek()
    {
        if(heap.Count > 0) return heap[0];

        throw new InvalidOperationException();
    }
        
    public T Pull()
    {
        if (Count == 0) throw new InvalidOperationException();

        T item = heap[0];

        Swap(Count - 1, 0);
        heap.RemoveAt(Count - 1);
        BubbleDown(0);

        return item;
    }

    private void BubbleDown(int index)
    {
        while(index < Count / 2)
        {
            int child = Left(index);
            if(HasChild(child + 1) && isLess(child, child + 1))
            {
                child = child + 1;
            }

            if(isLess(child, index))
            {
                break;
            }

            Swap(child, index);
            index = child;
        }  
    }

    private bool HasChild(int index)
    {
        return index < Count;
    }

    private int Left(int index)
    {
        return 2 * index + 1;
    }
}
