using System;
using System.Collections.Generic;
using System.Text;

namespace _2d_admination
{
    internal class writeOwnFunction
    {

        public writeOwnFunction(int width, int height, string[,] grid, string formula)
        {
            writeOwnFunctionM(width, height, grid, formula);
        }

        public void writeOwnFunctionM(int width, int height, string[,] grid, string formula)
        {
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
            bool useB = false;
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == 'b')
                {
                    bool beforeIsLetter = i > 0 && char.IsLetter(formula[i - 1]);
                    bool afterIsLetter = i < formula.Length - 1 && char.IsLetter(formula[i + 1]);

                    if (!beforeIsLetter && !afterIsLetter)
                    {
                        useB = true;
                        break;
                    }
                }
            }

            Animation.animation(width, height, grid, formula, 1, 0, 0, 1);


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

                    Animation.animation(width, height, grid, formula, a, b, c, frames);
                }
            }
        }



    }
}
