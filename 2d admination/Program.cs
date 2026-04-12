
using System;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace _2d_admination
{
    internal class Program
    {
        static void animation(int width, int height, string[,] grid, string formula, double a, double b, double c, int frames)
        {
            double originX = width / 2.0;
            double originY = height / 2.0;

            var func = MathParser.Parse(formula.Split('=')[1]);

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

                Console.ForegroundColor = ConsoleColor.Red;

                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Console.Write(grid[i, j]);
                    }
                    Console.WriteLine();
                }

                if (frames > 1)
                {
                    Console.SetCursorPosition(0, 0);
                }
            }
            Task.Delay(1500).Wait();
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

        static void Main(string[] args)
        {
            int width = 150;
            int height = 50;
            string[,] grid = new string[width, height];

            functionsListe list = new functionsListe();

            function f1 = new function("y = (3 * (tan(x) + sin(a * x^2)) * cos(x))^2", 0.01, 0, 0, 100);
            list.AddFunction(f1);
            function f2 = new function("y = x * sin(tan(ln(a * x^2))) * cos(x)", 0.01, 0, 0, 100);
            list.AddFunction(f2);
            function f3 = new function("y = a * x^2 + (b * c) * sin( b * x )", 0.01, 0.02, 10, 100);
            list.AddFunction(f3);


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hallo, in diesem Programm können Sie Mathematische Funktionen rendern und animieren.");
            Console.WriteLine("Supportet werden bis zu 3 variablen [a|b|c], sin, cos, tan, polynomfunktionen und logarithmusfunktion.");

            while (true)
            {
                string input = "";
                while (input != "1" && input != "2")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Wollen Sie in die FunktionenBibliothek[1] schauen oder selber eine Funktion[2] eingeben? : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    input = Console.ReadLine();
                }
                if (input == "1")
                {
                    Console.Clear();
                    string userinput = "";
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(list.getfunctionlist);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Welche Funktion möchten Sie auswählen?: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    userinput = Console.ReadLine();

                    animation(width, height, grid, list.GetFormula(int.Parse(userinput)), list.GetA(int.Parse(userinput)), list.GetB(int.Parse(userinput)), list.GetC(int.Parse(userinput)), list.GetFrames(int.Parse(userinput)));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Geben Sie eine Funktion ein: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string formula = Console.ReadLine();

                    bool useA = false;
                    for (int i = 0; i < formula.Length; i++)
                    {
                        if (formula[i] == 'a')
                        {
                            bool beforeIsLetter = i > 0 && char.IsLetter(formula[i - 1]);
                            bool afterIsLetter = i < formula.Length - 1 && char.IsLetter(formula[i + 1]);

                            if (!beforeIsLetter && !afterIsLetter)
                            {
                                useA = true;
                                break;
                            }
                        }
                    }

                    bool useC = false;
                    for (int i = 0; i < formula.Length; i++)
                    {
                        if (formula[i] == 'c')
                        {
                            bool beforeIsLetter = i > 0 && char.IsLetter(formula[i - 1]);
                            bool afterIsLetter = i < formula.Length - 1 && char.IsLetter(formula[i + 1]);

                            if (!beforeIsLetter && !afterIsLetter)
                            {
                                useC = true;
                                break;
                            }
                        }
                    }
                    bool useB = formula.Contains("b");

                    animation(width, height, grid, formula, 1, 0, 0, 1);


                    if (useA || useB || useC)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Wollen Sie die Funktion Animieren?[y/n]: ");
                        Console.ForegroundColor = ConsoleColor.Green;

                        string animate = Console.ReadLine();
                        Console.Clear();

                        if (animate.ToLower() != "n")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Wieviele Frames soll die Animation haben?: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            int frames = int.Parse(Console.ReadLine());
                            Console.Clear();

                            double a = 0, b = 0, c = 0;

                            if (useA)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("Welcher Wert soll a haben?: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                a = double.Parse(Console.ReadLine());
                            }

                            if (useB)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("Welcher Wert soll b haben?: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                b = double.Parse(Console.ReadLine());
                            }

                            if (useC)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("Welcher Wert soll a haben?: ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                c = double.Parse(Console.ReadLine());
                            }

                            animation(width, height, grid, formula, a, b, c, frames);
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Wollen Sie nocheinmal das Programm ausführen?[y/n]: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string again = Console.ReadLine();
                if (again.ToLower() == "n")
                {
                    break;
                }

                Console.Clear();
            }
        }
    }
}
