using System;

class Program
{
    static void Main(string[] args)
    {
        AVL<int> avl = new AVL<int>();
        for (int i = 1; i < 10; i++)
        {
            avl.Insert(i);
        }

        //avl.Test();

        avl.Delete(4);
        var root = avl.Root;
        root.Left.Left.Height = 4; // 1
        root.Left.Right.Height = 4; // 3
        root.Right.Left.Right.Height = 4; // 7
        root.Right.Right.Height = 4; // 9

        // Nodes of height 2
        root.Left.Height = 4; // 2
        root.Right.Left.Height = 4; //6

        // Nodes of height 3
        root.Right.Height = 4;// 8

        // Nodes of height 4
        root.Height = 4; // 5

        Console.WriteLine();
    }
}
