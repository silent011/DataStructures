using System;
using System.Collections.Generic;
using System.Linq;

class KdTree
{
    public KdNode Root;
    public int MaxRange;
    
    public void InsertRange(List<Point> points)
    {
        Root = InsertRange(points, 0);
    }

    private KdNode InsertRange(List<Point> points, int depth)
    {
        if (points.Count == 0) return null;

        if(depth % 2 == 0)
            points.Sort((a, b) => a.X.CompareTo(b.X));
        else
            points.Sort((a, b) => a.Y.CompareTo(b.Y));

        int midIndex = points.Count / 2;
        KdNode node = new KdNode(points[midIndex]);

        var smaller = points.Take(midIndex).ToList();
        var bigger = points.Skip(midIndex + 1).ToList();

        node.Left = InsertRange(smaller, depth + 1);
        node.Right = InsertRange(bigger, depth + 1);

        return node;
    }

    public int Report(int x1, int y1, int width, int height)
    {
        List<Point> result = new List<Point>();
        int x2 = x1 + width;
        int y2 = y1 + height;
        if (y2 > MaxRange) y2 = MaxRange;
        if (x2 > MaxRange) x2 = MaxRange;
        Report(Root, x1, y1, x2, y2, result,0);
        return result.Count;
    }

    private void Report(KdNode node, int x1, int y1, int x2, int y2, 
        List<Point> result, int depth)
    {
        if (node == null) return;

        int X = node.Point.X;
        int Y = node.Point.Y;

        if (node.Point.isInside(x1, y1, x2, y2)) result.Add(node.Point);
        if (depth % 2 == 0)
        {
            if (x2 < X) Report(node.Left, x1, y1, x2, y2, result, depth + 1);
            else if (X < x1) Report(node.Right, x1, y1, x2, y2, result, depth + 1);
            else 
            {
                Report(node.Left, x1, y1, x2, y2, result, depth + 1);
                Report(node.Right, x1, y1, x2, y2, result, depth + 1);
            }
        }
        else
        {
            if (y2 < Y) Report(node.Left, x1, y1, x2, y2, result, depth + 1);
            else if (Y < y1) Report(node.Right, x1, y1, x2, y2, result, depth + 1);
            else
            {
                Report(node.Left, x1, y1, x2, y2, result, depth + 1);
                Report(node.Right, x1, y1, x2, y2, result, depth + 1);
            }
        }
        
    }
}
