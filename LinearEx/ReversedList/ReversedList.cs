using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReversedList<T>
{
    private T[] data = new T[2];

    public int Count { get; private set; }

    public int Capacity
    {
        get
        {
            return this.data.Length;
        }
    }

    public void Add(T item)
    {
        data[this.Count++] = item;
        if (this.Count == this.Capacity) this.resize();
    }

    public void resize()
    {
        T[] newArr = new T[this.Count * 2];
        Array.Copy(this.data, newArr, this.Count);
        this.data = newArr;
    }

    public T this[int index]
    {
        get
        {
            if (index > this.Count - 1 || index < 0)
                throw new IndexOutOfRangeException();

           
            return this.data[this.getReverseIndex(index)];
        }
        set
        {
            if (index > this.Count-1 || index < 0)
                throw new IndexOutOfRangeException();

            //if (index == this.Count)
            //{
            //    this.Count++;
            //    if (this.Count == this.Capacity) this.resize();
            //}

            this.data[this.getReverseIndex(index)] = value;
            
        }
    }

    public void RemoveAt(int index)
    {
        if (index > this.Count - 1 || index < 0)
            throw new IndexOutOfRangeException();

        T[] newArr = new T[this.Capacity];

        int reversedIndex = this.getReverseIndex(index);
        Array.Copy(this.data, newArr, reversedIndex);
        if (reversedIndex < this.Count - 1)
            Array.Copy(this.data, reversedIndex + 1, newArr, reversedIndex, this.Count - 1 - reversedIndex);

        this.Count--;

        if (this.Count <= this.Capacity / 4) this.Shrink();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0 ; i--)
        {
            yield return this.data[i];
        }
               
    }

    private int getReverseIndex(int index)
    {
        return Math.Abs(this.Count - 1 - index);
    }

    private void Shrink()
    {
        T[] newArr = new T[this.Capacity / 2];
        Array.Copy(this.data, newArr, this.Count);
        this.data = newArr;
    }

}
