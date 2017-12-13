using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }

    private ListNode<T> head;
    private ListNode<T> tail;

    public void AddFirst(T item)
    {

        var newHead = new ListNode<T>(item);
        if (Count == 0)
        {
            head = tail = newHead;
        } 
        else
        {
            newHead.nextNode = head;
            head = newHead;
        }
        Count++;
    }

    public void AddLast(T item)
    {
        var newTail = new ListNode<T>(item);
        if(Count == 0)
        {
            head = tail = newTail;
        }
        else
        {
            tail.nextNode = newTail;
            tail = newTail;
        }
        Count++;
    }

    public T RemoveFirst()
    {
        if(Count > 0)
        {
            var value = head.Value;
            head = head.nextNode;
            Count--;
            return value;
        }
        else
        {
            throw new InvalidOperationException(
                "You can't remove an item from an empty list");
        }
       
    }

    public T RemoveLast()
    {
        if (Count > 0)
        {
            var value = tail.Value;
            if(Count == 1)
            {
                head = tail = null;
            }
            else
            {
                var prevNode = GetPrevToTailNode();
                tail = prevNode;
            }

            Count--;
            return value;
            
        }
        else
        {
            throw new InvalidOperationException(
                "You can't remove an item from an empty list");
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.head;
        for(int i = 0; i < Count; i++)
        {
            yield return current.Value;
            current = current.nextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   private ListNode<T> GetPrevToTailNode()
    {
        var currentNode = head;
        for(int i = 1; i < Count - 1; i++)
        {
            currentNode = currentNode.nextNode;
        }

        currentNode.nextNode = null;
        return currentNode;
    }

    private class ListNode<T>
    {
        public T Value { get; set; }

        public ListNode<T> nextNode { get; set; }

        public ListNode(T value)
        {
            this.Value = value;
        }
    }
}
