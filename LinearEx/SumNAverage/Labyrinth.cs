using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumNAverage
{
    class Labyrinth
    {
        public static void startClass()
        {
            int size = int.Parse(Console.ReadLine());
            int startX = -1;
            int startY = -1;

            string[][] arr = new string[size][];
            for (int i = 0; i < size; i++)
            {
                arr[i] = new string[size];
                string[] input = Console.ReadLine().ToCharArray().Select(c => c.ToString()).ToArray();
                for(int i1 = 0; i1< size; i1++)
                {
                    arr[i][i1] = input[i1];
                    if (input[i1] == "*")
                    {
                        startX = i1;
                        startY = i;
                    }
                }

            }


            FillAround(startY, startX, 1);
            goThroughMatrix(1);

            for (int i = 0; i < size; i++)
            {
                for (int i1 = 0; i1 < size; i1++)
                {
                    if (arr[i][i1] == "0") arr[i][i1] = "u";
                }
                Console.WriteLine(String.Join("  ", arr[i]));
            }


            void FillAround(int y, int x, int value)
            {
                if (x + 1 < size && arr[y][x + 1] == "0")
                {
                    arr[y][x + 1] = "" + value;
                }
                if (x - 1 >= 0 && arr[y][x - 1] == "0")
                {
                    arr[y][x - 1] = "" + value;
                }

                if (y + 1 < size && arr[y + 1][x] == "0")
                {
                    arr[y + 1][x] = "" + value;
                }
                if (y - 1 >= 0 && arr[y - 1][x] == "0")
                {
                    arr[y - 1][x] = "" + value;
                }

            }

            void goThroughMatrix(int value)
            {
                bool foundOne = false;
                List<Coords> coords = new List<Coords>();
                for (int i = 0; i < size; i++)
                {
                    for(int i1 = 0; i1< size; i1++)
                    {
                        if(arr[i][i1] == ""+value)
                        {
                            foundOne = true;
                            coords.Add(new Coords(i, i1));
                        }
                    }
                }

                foreach (var coord in coords)
                {
                    FillAround(coord.Y, coord.X, value + 1);
                }

                if (foundOne) goThroughMatrix(value + 1);
            }

            Labyrinth.startClass();
        }

        class Coords
        {
            public int X;
            public int Y;

            public Coords(int coordsY, int coordsX)
            {
                Y = coordsY;
                X = coordsX;
            }
        }
    }

}
