using System;
using System.Collections.Generic;

class SweepAndPrune
{
    private static List<GameSquare> list = new List<GameSquare>();
    public static void Add(string name,int x1,int y1)
    {
        list.Add(new GameSquare(name, x1, y1));
    }

    public static void Start()
    {
        int counter = 1;
        while(true)
        {
            string[] input = Console.ReadLine().Split(' ');
            string command = input[0];
            if (command == "end") break;

            if(command == "move")
            {
                string name = input[1];
                int x1 = int.Parse(input[2]);
                int y1 = int.Parse(input[3]);
                MoveSquare(name, x1, y1);
            }
            list = Sweep(counter++, list);
        }

        list = new List<GameSquare>();
        return;
    }

    private static void MoveSquare(string name, int x1, int y1)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name == name)
            {
                list[i] = new GameSquare(name, x1, y1);
                break;
            }
        }
    }

    private static void InsertionSort(List<GameSquare> squares)
    {
        for (int i = 1; i < squares.Count; i++)
        {
            var current = squares[i];
            for (int j = 0; j < i; j++)
            {
                var another = squares[j];
                if (current.CompareTo(another) < 0)
                {
                    squares = Insert(squares, i, j);
                    break;
                }
            }
        }

    }

    private static List<GameSquare> Insert(List<GameSquare> squares, int bigIndex, int smallIndex)
    {
        var old = squares[bigIndex];
        for (int i = smallIndex; i <= bigIndex; i++)
        {
            var temp = squares[i];
            squares[i] = old;
            old = temp;
        }

        return squares;
    }

    public static List<GameSquare> Sweep(int counter, List<GameSquare> squares)
    {
        InsertionSort(squares);
        for(int i = 0;i < squares.Count - 1; i++)
        {
            var current = squares[i];
            for (int j = i + 1; j < squares.Count; j++)
            {
                var another = squares[j];
                if (current.X2 < another.X1)
                    break;
                if (CheckY(current, another))
                    Console.WriteLine($"({counter}) {current.Name} collides with {another.Name}");
            }
        }

        return squares;
    }

    private static bool CheckY(GameSquare current, GameSquare another)
    {
        return current.Y1 <= another.Y2 && current.Y2 >= another.Y1;
    }
}

