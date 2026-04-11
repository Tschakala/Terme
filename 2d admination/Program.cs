using System;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _2d_admination
{
    internal class Program
    {


        static void animation(int width, int height, string[,] grid, string formula, double a, int frames)
        {
            double originX = width / 2.0;
            double originY = height / 2.0;

            var func = MathParser.Parse(formula.Split('=')[1]);

            for (int k = 1; k <= frames; k++)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        grid[i, j] = " ";
                    }
                }

                if (frames > 1)
                {
                    Console.WriteLine("Frame: " + k + "\tFormel: " + formula);
                }

                if (originY >= 0 && originY < height)
                {
                    for (int i = 0; i < width; i++)
                    {
                        grid[i, (int)originY] = "-";
                    }
                }

                if (originX >= 0 && originX < width)
                {
                    for (int j = 0; j < height; j++)
                    {
                        grid[(int)originX, j] = "|";
                    }
                }

                double prevX = double.NaN;
                double prevY = double.NaN;

                for (double x = -originX; x < originX; x += 0.05)
                {
                    double y = func(x * (a * k));

                    int px = (int)(originX + x);
                    int py = (int)(originY - y);

                    if (!double.IsNaN(prevX))
                    {
                        int pxPrev = (int)(originX + prevX);
                        int pyPrev = (int)(originY - prevY);

                        PlotLine(pxPrev, pyPrev, px, py, grid);
                    }

                    prevX = x;
                    prevY = y;
                }

                Console.ForegroundColor = ConsoleColor.Red;

                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Console.Write(grid[i, j]);
                    }
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Cyan;

                if (frames > 1)
                {
                    Console.SetCursorPosition(0, 0);
                }
            }

            if (frames > 1)
            {
                Console.Clear();
            }
        }

        static void PlotLine(int x0, int y0, int x1, int y1, string[,] grid)
        {
            int dx = Math.Abs(x1 - x0);
            int sx = x0 < x1 ? 1 : -1;
            int dy = -Math.Abs(y1 - y0);
            int sy = y0 < y1 ? 1 : -1;
            int err = dx + dy;

            while (true)
            {
                if (x0 >= 0 && x0 < grid.GetLength(0) &&
                    y0 >= 0 && y0 < grid.GetLength(1))
                {
                    grid[x0, y0] = "#";
                }

                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                int e2 = 2 * err;
                if (e2 >= dy) 
                {
                    err += dy; 
                    x0 += sx;
                }
                if (e2 <= dx) 
                {
                    err += dx; y0 += sy;
                }
            }
        }

        static void Main(string[] args)
        {


            while (true)
            {
                string formula = "";

                while (formula == "")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Put in a Function: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    formula = Console.ReadLine();
                }
                int width = 150;
                int height = 50;


                string[,] grid = new string[width, height];

                double originX = width / 2.0;
                double originY = height / 2.0;

                animation(width, height, grid, formula, 1, 1);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Task.Delay(2500).Wait();
                Console.Clear();
                Console.Write("Do you want to animate the term?: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string animate = Console.ReadLine();
                Console.Clear();

                if (animate.ToLower() != "no")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Wieviele Frames soll die Animation haben?: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    int frames = int.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("What should be a?: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    double a = double.Parse(Console.ReadLine());
                    animation(width, height, grid, formula, a, frames);
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Do you want to go again?: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string answer = Console.ReadLine();
                Console.Clear();

                if (answer.ToLower() == "no")
                {
                    break;
                }
            }
        }
    }
}