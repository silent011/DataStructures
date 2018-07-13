using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GameSquare
{
    public int X1;
    public int Y1;
    public int X2;
    public int Y2;
    public string Name;
    public static int height = 10;
    public static int width = 10;
    public GameSquare(string name, int x,int y)
    {
        Name = name;
        X1 = x;
        Y1 = y;
        X2 = x + width;
        Y2 = y + height;
    }

    public int CompareTo(GameSquare another)
    {
        int cmpX = X1.CompareTo(another.X1);

        return cmpX;
    }
}

