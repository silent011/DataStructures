namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T>: IHierarchy<T> where T: IComparable<T>
    {
        private Node Root;
        private Dictionary<T, Node> allNodes = new Dictionary<T, Node>();
        public Hierarchy(T root)
        {
            Root = new Node(root);
            allNodes.Add(root, Root);
        }

        public int Count
        {
            get
            {
                return allNodes.Count;
            }
        }

        public void Add(T element, T child)
        {
            if (Contains(child)) throw new ArgumentException();

            Node parent = Search(element);

            Node childNode = new Node(child);
            childNode.ParentVal = parent.Value;

            parent.Children.Add(childNode);
            allNodes.Add(child, childNode);
        }

        private Node Search(T element)
        {
            if (!allNodes.ContainsKey(element)) throw new ArgumentException();
            return allNodes[element];
        }

        public void Remove(T element)
        {
            Node node = Search(element);

            if (element.CompareTo(Root.Value) ==0) throw new InvalidOperationException();

            Node parent = Search(node.ParentVal);

            parent.Children.Remove(node);

            if(node.Children.Count > 0)
            {
                foreach(Node child in node.Children)
                {
                    child.ParentVal = parent.Value;
                    parent.Children.Add(child);
                }
            }

            allNodes.Remove(node.Value);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            Node parent = Search(item);

            return parent.Children.Select(x => x.Value);
        }

        public T GetParent(T item)
        {
            if (!allNodes.ContainsKey(item)) throw new ArgumentException();
            Node child = allNodes[item];
            if (child == Root) return default(T);

            return child.ParentVal;
        }

        public bool Contains(T value)
        {
            return allNodes.ContainsKey(value);
        }


        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            Stack<T> result = new Stack<T>();
            foreach(T item in other)
            {
                if (allNodes.ContainsKey(item)) result.Push(item);
            }

            return result;
        } 

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(Root);

            while(nodes.Count > 0)
            {
                Node current = nodes.Dequeue();
                yield return current.Value;
                foreach (Node child in current.Children)
                {
                    nodes.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public Node(T val)
            {
                Value = val;
                Children = new List<Node>();
            }

            public T ParentVal;
            public List<Node> Children;
           
            public T Value;
        }
    }
}