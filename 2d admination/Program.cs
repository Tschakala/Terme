using System;
using System.Data;
using System.Runtime.InteropServices;

namespace _2d_admination
{
    internal class Program
    {
        static bool calculate(string formula, int x, int y)
        {
            string right = formula.Split('=')[1].Trim();

            right = right.Replace("x", x.ToString());

            DataTable table = new DataTable();
            double result = Convert.ToDouble(table.Compute(right, ""));

            return Math.Round(result / 10.0) == y;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                string formula = "";
                while (formula == "")
                {
                    Console.Write("Geben Sie eine Formel ein: "); // y = x^2  100x100
                    formula = Console.ReadLine();
                }

                int width = 150;
                int height = 150;

                string[,] grid = new string[width, height];

                int originX = width / 2;
                int originY = height / 2;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        int x = i - originX;
                        int y = originY - j;

                        if (((j == 0) || (j == 149)) && (i != 149))
                        {
                            grid[i, j] = "|";
                        }
                        else if ((i == 0) || (i == 149))
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

                for (int i = width - 1;i >= 0; i--)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Console.Write(grid[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.Write("Do you want to go again?: ");
                string answer = Console.ReadLine();
                if (answer.ToLower() != "yes")
                {
                    break;
                }
            }
        }
    }
}