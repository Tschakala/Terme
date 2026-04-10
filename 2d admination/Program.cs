using System;
using System.Data;
using System.Globalization;

namespace _2d_admination
{
    internal class Program
    {
        static void animation(int width, int height, string[,] grid, string formula, double a)
        {
            double originX = width / 2.0;
            double originY = height / 2.0;

            for (int k = 1; k < 1000; k++)
            {
                Console.WriteLine(k);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        double x = i - originX;
                        double y = originY - j;

                        if (((j == 0) || (j == 149)) && (i != 49))
                        {
                            grid[i, j] = "|";
                        }
                        else if ((i == 0) || (i == 49))
                        {
                            grid[i, j] = "_";
                        }
                        else if (calculate(formula, y * (a * k), x))
                        {
                            grid[i, j] = "#";
                        }
                        else
                        {
                            grid[i, j] = " ";
                        }
                    }
                }

                for (int i = width - 1; i >= 0; i--)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Console.Write(grid[i, j]);
                    }
                    Console.WriteLine();
                }

                Task.Delay(0);
                Console.Clear();
            }
        }

        static bool calculate(string formula, double x, double y)
        {
            string right = formula.Split('=')[1].Trim();

            right = right.Replace(
                "x",
                x.ToString(CultureInfo.InvariantCulture)
            );

            DataTable table = new DataTable();
            double result = Convert.ToDouble(table.Compute(right, ""));

            return Math.Abs(result / 10.0 - y) < 0.5;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                string formula = "";

                while (formula == "")
                {
                    Console.Write("Geben Sie eine Formel ein: ");
                    formula = Console.ReadLine();
                }

                int width = 50;
                int height = 150;

                string[,] grid = new string[width, height];

                double originX = width / 2.0;
                double originY = height / 2.0;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        double x = i - originX;
                        double y = originY - j;

                        if (((j == 0) || (j == 149)) && (i != 49))
                        {
                            grid[i, j] = "|";
                        }
                        else if ((i == 0) || (i == 49))
                        {
                            grid[i, j] = "_";
                        }
                        else if (calculate(formula, y, x))
                        {
                            grid[i, j] = "#";
                        }
                        else
                        {
                            grid[i, j] = " ";
                        }
                    }
                }

                for (int i = width - 1; i >= 0; i--)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Console.Write(grid[i, j]);
                    }
                    Console.WriteLine();
                }

                Console.Write("Do you want to animate the term?: ");
                string animate = Console.ReadLine();
                Console.Clear();

                if (animate.ToLower() == "no")
                {
                    break;
                }
                else
                {
                    Console.Write("Wie stark soll sich a verändern?: ");
                    double a = double.Parse(Console.ReadLine());
                    animation(width, height, grid, formula, a);
                }

                Console.Write("Do you want to go again?: ");
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