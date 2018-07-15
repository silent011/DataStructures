using System;
using System.Collections.Generic;


class KdNode
{
    public Point Point;
    public KdNode Left;
    public KdNode Right;

    public KdNode(Point point)
    {
        Point = point;
    }
}
