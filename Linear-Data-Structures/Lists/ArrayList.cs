using System;

public class ArrayList<T>
{
	private T[] data = new T[INITIAL_SIZE];

	private const int INITIAL_SIZE = 2;

	public int Count
	{
		get; private set;
	}

    public T this[int index]
    {
        get
        {
			if (index >= this.Count || index < 0)
				throw new ArgumentOutOfRangeException();

			return this.data[index];
        }

        set
        {
			if (index >= this.Count || index < 0)
				throw new ArgumentOutOfRangeException();

			this.data[index] = value;

		}
    }

    public void Add(T item)
    {
		this.data[this.Count++] = item;

		if (this.Count == this.data.Length)
		{
			this.Resize();
		}
    }

    public T RemoveAt(int index)
    {
		if (index >= this.Count || index < 0)
			throw new ArgumentOutOfRangeException();

		T[] newArray = new T[--this.Count];

		T element = this.data[index];
		Array.Copy(this.data, newArray, index);

		if (index < this.Count - 1)
			Array.Copy(this.data, index + 1, newArray, index, this.Count - index);

		this.data = newArray;

		return element;
	}

	public void Resize()
	{
		T[] newArray = new T[this.Count * 2];

		Array.Copy(this.data, newArray, this.Count);

		this.data = newArray;
	}
}
