using System;
using System.Collections.Generic;
using System.Text;

namespace _2d_admination
{
    internal class Animation
    {

        public Animation(int width, int height, string[,] grid, string formula, double a, double b, double c, int frames) 
        {
            animation(width, height, grid, formula, a, b, c, frames);
        }

        public static void animation(int width, int height, string[,] grid, string formula, double a, double b, double c, int frames)
        {
            int delay = 5;
            double originX = width / 2.0;
            double originY = height / 2.0;

            var func = MathParser.Parse(formula.Split('=')[1]);

            Console.CursorVisible = false;
            for (int k = 1; k <= frames; k++)
            {
                double aK = a * k;
                double bK = b * k;
                double cK = c * k;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        grid[i, j] = " ";
                    }
                }

                if (frames > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string info = $"Frame: {k}   Formel: {formula}";
                    if (a != 0)
                    {
                        info += $"   a: {aK:F2}";
                    }
                    if (b != 0)
                    {
                        info += $"   b: {bK:F2}";
                    }
                    if (c != 0)
                    {
                        info += $"   c: {cK:F2}";
                    }
                    info += $"   Delay between each Frame: {delay}ms";

                    double yValue = func(0, aK, bK, cK);
                    info += $"   y: {yValue:F3}";

                    Console.WriteLine(info);
                }


                for (int i = 0; i < width; i++)
                {
                    grid[i, (int)originY] = "-";
                }

                for (int j = 0; j < height; j++)
                {
                    grid[(int)originX, j] = "|";
                }

                double prevX = double.NaN;
                double prevY = double.NaN;

                for (double x = -originX; x < originX; x += 0.025)
                {
                    double y = func(x, aK, bK, cK);

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

                if (frames == 1)
                {
                    Console.Clear();

                    string info = $"  Formel: {formula}";
                    double yValue = func(0, aK, bK, cK);
                    info += $"   y: {yValue:F3}";
                    Console.Write(info);
                }

                Console.ForegroundColor = ConsoleColor.Red;

                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        if (j != 0)
                        {
                            Console.Write(grid[i, j]);
                        }
                    }
                    Console.WriteLine();
                }

                if (frames > 1)
                {
                    Thread.Sleep(delay);
                    Console.SetCursorPosition(0, 0);
                }
            }
            Console.CursorVisible = true;
            Task.Delay(2500).Wait();
            Console.Clear();
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
                if ((x0 >= 0) && (x0 < grid.GetLength(0)) && (y0 >= 0) && (y0 < grid.GetLength(1)))
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
                    err += dx;
                    y0 += sy;
                }
            }
        }

    }
}
