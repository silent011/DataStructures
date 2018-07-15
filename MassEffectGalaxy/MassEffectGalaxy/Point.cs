using System;
using System.Collections.Generic;

class Point
{
    public int X;
    public int Y;
    public string Name;
    public Point(string name, int x, int y)
    {
        Name = name;
        X = x;
        Y = y;
    }

    public bool isInside(int x1, int y1, int x2, int y2)
    {
        return x1 <= X && X <= x2 && y1 <= Y && Y <= y2;
    }
}
