using System;
using System.Collections.Generic;
using System.Linq;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;

    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }

    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfs(this.root, action);
    }

    public bool Insert(T item)
    {
        if (!item.Bounds.IsInside(Bounds)) return false;

        int depth = 1;
        var currentNode = root;
        while (currentNode.Children != null)
        {
            var quadrant = GetQuadrant(currentNode, item.Bounds);
            if (quadrant == -1) break;
            currentNode = currentNode.Children[quadrant];
            depth++;
        }

        currentNode.Items.Add(item);
        Split(currentNode, depth);
        Count++;

        return true;
    }

    private void Split(Node<T> node, int nodeDepth)
    {
        if (!(node.ShouldSplit && nodeDepth < MaxDepth))
            return;

        var leftWidth = node.Bounds.Width / 2;
        var rightWidth = node.Bounds.Width - leftWidth;
        var topHeight = node.Bounds.Height / 2;
        var bottomHeight = node.Bounds.Height - topHeight;

        if(node.Children == null)
        {
            node.Children = new Node<T>[4];
            node.Children[0] = new Node<T>(node.Bounds.MidX, node.Bounds.Y1,
                rightWidth, topHeight);
            node.Children[1] = new Node<T>(node.Bounds.X1, node.Bounds.Y1,
                leftWidth, topHeight);
            node.Children[2] = new Node<T>(node.Bounds.X1, node.Bounds.MidY,
                leftWidth, bottomHeight);
            node.Children[3] = new Node<T>(node.Bounds.MidX, node.Bounds.MidY,
                rightWidth, bottomHeight);
        }
       

        ChildrenToQuads(node);

        foreach (var child in node.Children)
        {
            Split(child, nodeDepth + 1);
        }
    }

    private void ChildrenToQuads(Node<T> node)
    {
        for (int i = 0; i < node.Items.Count; i++)
        {
            var item = node.Items[i];
            var quadrant = GetQuadrant(node, item.Bounds);
            if(quadrant != -1)
            {
                node.Children[quadrant].Items.Add(item);
                node.Items.Remove(item);
            }
        }
    }

    private int GetQuadrant(Node<T> node, Rectangle bounds)
    {
        var verticalMidPoint = node.Bounds.MidX;
        var horizontalMidPoint = node.Bounds.MidY;

        var inTopQuadrant = node.Bounds.Y1 <= bounds.Y1 &&
            bounds.Y2 <= horizontalMidPoint;
        var inBottomQuadrant = horizontalMidPoint <= bounds.Y1 &&
            bounds.Y2 <= node.Bounds.Y2;
        var inLeftQuadrant = node.Bounds.X1 <= bounds.X1 &&
            bounds.X2 <= verticalMidPoint;
        var inRightQuadrant = verticalMidPoint <= bounds.X1 &&
            bounds.X2 <= node.Bounds.X2;

        if (inRightQuadrant)
        {
            if (inTopQuadrant) return 0;
            if (inBottomQuadrant) return 3; 
        }
        else if (inLeftQuadrant)
        {
            if (inTopQuadrant) return 1;
            if (inBottomQuadrant) return 2;
        }

        return -1;
    }

    public List<T> Report(Rectangle bounds)
    {
        var collisionOptions = new List<T>();
        GetCollisionOptions(root, bounds, collisionOptions);

        return collisionOptions;
    }

    private void GetCollisionOptions(Node<T> node, Rectangle bounds, List<T> results)
    {
        var quadrant = GetQuadrant(node, bounds);

        if (quadrant == -1) GetSubtreeContents(node, bounds, results);
        else
        {
            if (node.Children != null)
                GetCollisionOptions(node.Children[quadrant], bounds, results);
            results.AddRange(node.Items);
        }
    }

    private void GetSubtreeContents(Node<T> node, Rectangle bounds, List<T> results)
    {
       if(node.Children != null)
        {
            foreach (var child in node.Children)
            {
                if (child.Bounds.Intersects(Bounds))
                {
                    GetSubtreeContents(child, bounds, results);
                }
            }
        }

        results.AddRange(node.Items);
    }

    private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
    {
        if (node == null)
        {
            return;
        }

        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                ForEachDfs(node.Children[i], action, depth + 1, i);
            }
        }
    }
}
