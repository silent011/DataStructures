using System;
using System.Collections.Generic;

public class AStar
{
    private Node Start;
    private Node Goal;
    private PriorityQueue<Node> Path = new PriorityQueue<Node>();
    private char[,] Map;
    private Dictionary<Node, Node> Parents;
    private Dictionary<Node, int> GCosts;

    public AStar(char[,] map)
    {
        Map = map;
        Parents = new Dictionary<Node, Node>();
        GCosts = new Dictionary<Node, int>();
    }


    public static int GetH(Node currentPos, Node goalPos)
    {
        int XDiff = Math.Abs(currentPos.Col - goalPos.Col);
        int YDiff = Math.Abs(currentPos.Row - goalPos.Row);

        return XDiff + YDiff;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        if (start == null || goal == null) throw new InvalidOperationException("goal or start missing");

        if (Parents.ContainsKey(goal) && Parents.ContainsKey(start))
        {
            return FillAlreadyKnownPath(start, goal);
        }

        Start = start;
        Goal = goal;
        Parents = new Dictionary<Node, Node>();
        GCosts = new Dictionary<Node, int>();
        Path = new PriorityQueue<Node>();

        Stack<Node> shortestPath = new Stack<Node>();
        Parents.Add(Start, null);
        GCosts.Add(Start, 0);
        Path.Enqueue(Start);

        Node endOfPath = SearchShortestPath();
     
        if (endOfPath == null)
        {
            shortestPath.Push(Start);
            return shortestPath;
        }

        Fill(shortestPath, endOfPath);

        return shortestPath;
    }

    private IEnumerable<Node> FillAlreadyKnownPath(Node start, Node goal)
    {
        Stack<Node> resultPath = new Stack<Node>();
        resultPath.Push(goal);
        Node current = Parents[goal];
        while(current != null)
        {
            resultPath.Push(current);
            if (current.Equals(start)) break;
            current = Parents[current];
        }

        return resultPath;
    }

    private void Fill(Stack<Node> shortestPath, Node current)
    {
        if (current == null) return;

        shortestPath.Push(current);
        Fill(shortestPath, Parents[current]);
    }

    private Node SearchShortestPath()
    {
        while(Path.Count > 0)
        {
            Node current = Path.Dequeue();
            if (current.Equals(Goal)) return current;

            GetNeighbours(current);
        }

        return null;
    }

    private void GetNeighbours(Node current)
    {
        int x = current.Col;
        int y = current.Row;
        GetUpper(current, x, y);
        GetRight(current, x, y);
        GetDowner(current, x, y);
        GetLeft(current, x, y);
    }

    private void GetLeft(Node current, int x, int y)
    {
        if (x - 1 >= 0 && !Map.GetValue(y, x - 1).Equals('W'))
        {
            Node left  = new Node(y, x - 1);
            if (GCosts.ContainsKey(left)) return;

            Parents.Add(left, current);
            GCosts.Add(left, GCosts[current] + 1);

            left.F = CalcFCost(left, current);
            Path.Enqueue(left);
        }
    }

    private void GetDowner(Node current, int x, int y)
    {
        if (y + 1 < Map.GetLength(0) && !Map.GetValue(y+1, x).Equals('W'))
        {
            Node down = new Node(y + 1, x);
            if (GCosts.ContainsKey(down)) return;

            Parents.Add(down, current);
            GCosts.Add(down, GCosts[current] + 1);

            down.F = CalcFCost(down, current);
            Path.Enqueue(down);
        }
    }

    private int CalcFCost(Node target, Node current)
    {
        return GCosts[current] + GetH(current, Goal);
    }

    private void GetRight(Node current, int x, int y)
    {
        if (x + 1 < Map.GetLength(1) && !Map.GetValue(y, x + 1).Equals('W'))
        {
            Node right = new Node(y, x + 1);
            if (GCosts.ContainsKey(right)) return;

            Parents.Add(right, current);
            GCosts.Add(right, GCosts[current] + 1);

            right.F = CalcFCost(right, current);
            Path.Enqueue(right);
        }
    }

    private void GetUpper(Node current, int x, int y)
    {
        if (y - 1 >= 0 && !Map.GetValue(y - 1, x).Equals('W'))
        {
            Node up = new Node(y - 1, x);
            if (GCosts.ContainsKey(up)) return;

            Parents.Add(up, current);
            GCosts.Add(up, GCosts[current] + 1);

            up.F = CalcFCost(up, current);
            Path.Enqueue(up);
        }
    }
}

