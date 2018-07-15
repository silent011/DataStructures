using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int numClusters = int.Parse(Console.ReadLine());
        int numReports = int.Parse(Console.ReadLine());
        int galaxyRange = int.Parse(Console.ReadLine());

        List<Point> clusters = new List<Point>(numClusters);
        for (int i = 0; i < numClusters; i++)
        {
            string[] input = Console.ReadLine().Split(' ');
            string name = input[0];
            int x = int.Parse(input[1]);
            int y = int.Parse(input[2]);

            clusters.Add(new Point(name, x, y));
        }

        KdTree tree = new KdTree();
        tree.MaxRange = galaxyRange;
        tree.InsertRange(clusters);

        for (int i = 0; i < numReports; i++)
        {
            string[] input = Console.ReadLine().Split(' ');
            int x1 = ParseToInt(input[1]);
            int y1 = ParseToInt(input[2]);
            int width = ParseToInt(input[3]);
            int height = ParseToInt(input[4]);

            int report = tree.Report(x1, y1, width, height);
            Console.WriteLine(report);
        }
    }

    private static int ParseToInt(string str) => int.Parse(str);
}

