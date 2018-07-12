using System;
using System.Collections;
using System.Collections.Generic;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private LinkedList<KeyValue<TKey, TValue>>[] List;

    private const int InitialCapacity = 16;

    public int Count { get; private set; }

    public HashTable(int capacity = InitialCapacity)
    {
        List = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        Count = 0;
    }

    public int Capacity
    {
        get
        {
            return List.Length;
        }
    }

    public void Add(TKey key, TValue value)
    {
        GrowIfNeeded();
        int index = GetIndexValue(key);
        if (List[index] == null)
            List[index] = new LinkedList<KeyValue<TKey, TValue>>();
        foreach (var item in List[index])
        {
            if (item.Key.Equals(key))
            {
                throw new ArgumentException("Key already exists");
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        List[index].AddLast(newElement);
        Count++;
    }

    private void GrowIfNeeded()
    {
        if(Capacity == Count)
        {
            var newList = new LinkedList<KeyValue<TKey, TValue>>[2 * Capacity];
            for (int i = 0; i < Capacity; i++)
            {
                if(List[i] != null && List[i].Count > 0)
                {
                    int newIndex = Math.Abs(i.GetHashCode()) % newList.Length;
                    newList[newIndex] = List[i];
                }
            }
            List = newList;
        }
    }

    private int GetIndexValue(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % Capacity;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        throw new NotImplementedException();
    }

    public TValue Get(TKey key)
    {
        var element = Find(key);
        if (element == null) throw new KeyNotFoundException();

        return element.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            throw new NotImplementedException();
            // Note: throw an exception on missing key
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var element = Find(key);
        if(element != null)
        {
            value = element.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int index = GetIndexValue(key);
        var items = List[index];
        if(items != null)
        {
            foreach (var item in items)
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        int index = GetIndexValue(key);
        if(List[index] != null)
        {
            foreach (var item in List[index])
            {
                if (item.Key.Equals(key)) return true;
            }
        }

        return false;
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var item in List)
        {
            if(item != null)
            {
                foreach (var elem in item)
                {
                    yield return elem;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
