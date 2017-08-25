using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
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

                return this.data[index];
            }
            set
            {
                if (index > this.Count - 1 || index < 0)
                    throw new IndexOutOfRangeException();

                this.data[this.Count++] = value;
                if (this.Count == this.Capacity) this.resize();
            }
        }

        public void RemoveAt(int index)
        {
            if (index > this.Count - 1 || index < 0)
                throw new IndexOutOfRangeException();

            T[] newArr = new T[--this.Count];
            Array.Copy(this.data, newArr, index);

            if (index < this.Count)
                Array.Copy(this.data, index + 1, newArr, index, this.Count - index);

            this.data = newArr;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.data[i];
            }
               
        }

    }
}
