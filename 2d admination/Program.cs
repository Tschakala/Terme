using System;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace _2d_admination
{
    internal class Program
    {
        static void animation(int width, int height, string[,] grid, string formula, double a, int frames)
        {
            double originX = width / 2.0;
            double originY = height / 2.0;

            for (int k = 1; k <= frames; k++)
            {
                if (frames > 1)
                {
                    Console.WriteLine(k);
                }
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
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = width - 1; i >= 0; i--)
                {
                    for (int j = 0; j < height; j++)
                    {

                        if (grid[i, j] == "#")
                        {
                            Console.Write(grid[i, j]);
                        }
                        else if (grid[i, j] != "")
                        {
                            Console.Write(grid[i, j]);
                        }
                    }
                    Console.WriteLine();
                }
                Console.ForegroundColor= ConsoleColor.Cyan;
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

        static string ReplaceTrig(string expr, double xValue)
        {
            string xStr = xValue.ToString(CultureInfo.InvariantCulture);

            expr = Regex.Replace(expr, @"(?i)(math\.)?sin\s*\((.*?)\)", match =>
            {
                string inner = match.Groups[2].Value.Replace("x", xStr);
                double v = Convert.ToDouble(new DataTable().Compute(inner, ""));
                return Math.Sin(v).ToString(CultureInfo.InvariantCulture);
            });

            expr = Regex.Replace(expr, @"(?i)(math\.)?cos\s*\((.*?)\)", match =>
            {
                string inner = match.Groups[2].Value.Replace("x", xStr);
                double v = Convert.ToDouble(new DataTable().Compute(inner, ""));
                return Math.Cos(v).ToString(CultureInfo.InvariantCulture);
            });

            expr = Regex.Replace(expr, @"(?i)(math\.)?tan\s*\((.*?)\)", match =>
            {
                string inner = match.Groups[2].Value.Replace("x", xStr);
                double v = Convert.ToDouble(new DataTable().Compute(inner, ""));
                return Math.Tan(v).ToString(CultureInfo.InvariantCulture);
            });

            return expr;
        }



        static string ExpandPowers(string expression)
        {
            return Regex.Replace(expression, @"x\^(\d+)", match =>
            {
                int power = int.Parse(match.Groups[1].Value);
                return string.Join("*", Enumerable.Repeat("x", power));
            });
        }




        static bool calculate(string formula, double x, double y)
        {
            string right = formula.Split('=')[1].Trim();

            right = ReplaceTrig(right, x);

            right = ExpandPowers(right);

            right = right.Replace("x", x.ToString(CultureInfo.InvariantCulture));

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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Put in a Function: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    formula = Console.ReadLine();
                }
                formula = ExpandPowers(formula);
                int width = 50;
                int height = 150;

                string[,] grid = new string[width, height];

                double originX = width / 2.0;
                double originY = height / 2.0;

                animation(width, height, grid, formula, 1, 1);

                Console.ForegroundColor = ConsoleColor.Cyan;
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