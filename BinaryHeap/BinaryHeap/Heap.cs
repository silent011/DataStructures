using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int n = arr.Length;
        for (int i = n / 2; i >= 0; i--)
        {
            BubbleDown(arr, i, n);
        }
       
        for(int i = n - 1; i > 0; i--)
        {
            Swap(arr, i, 0);
            BubbleDown(arr, 0, i);
        }
    }

    private static void BubbleDown(T[] arr, int current, int border)
    {
        while(current < border / 2)
        {
            int child = Left(current);
            if(child + 1 < border && IsLess(arr,child, child + 1))
            {
                child = child + 1;
            }

            if (IsLess(arr,child, current)) break;

            Swap(arr,current, child);
            current = child;
        }
    }

    private static void Swap(T[] arr, int current, int child)
    {
        T token = arr[current];
        arr[current] = arr[child];
        arr[child] = token;
    }

    private static bool IsLess(T[] arr, int index, int index2)
    {
        return arr[index].CompareTo(arr[index2]) < 0;
    }

    private static bool HasChild(T[] arr, int index)
    {
        return index < arr.Length;
    }

    private static int Left(int index)
    {
        return 2 * index + 1;
    }
}
