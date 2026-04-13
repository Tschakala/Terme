using System;
using System.Collections.Generic;
using System.Text;

namespace _2d_admination
{
    internal class Calculator
    {
        public Calculator() 
        {
            calculate();
        }

        public void calculate()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Willkommen im Taschenrechner!");
            Console.WriteLine("Mit diesem Tool können Sie mathematische Ausdrücke berechnen.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Modus: " + (MathParser.UseDegrees ? "Grad (DEG)" : "Radiant (RAD)"));
                Console.Write("Geben Sie einen mathematischen Ausdruck ein (oder 'exit' zum Beenden) (oder 'mode deg'/'mode rad' zum Wechseln des Modus): ");
                Console.ForegroundColor = ConsoleColor.Red;
                string input = Console.ReadLine();

                if (input.ToLower() == "mode deg")
                {
                    MathParser.UseDegrees = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Modus wurde auf Grad (DEG) gesetzt.");
                    continue;
                }

                if (input.ToLower() == "mode rad")
                {
                    MathParser.UseDegrees = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Modus wurde auf Radiant (RAD) gesetzt.");
                    continue;
                }
                if (input.ToLower() == "exit")
                {
                    MathParser.UseDegrees = false;
                    break;
                }
                try
                {
                    var result = MathParser.Parse(input);
                    double value = result(0, 0, 0, 0);
                    
                    Console.Write("Ergebnis: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(value);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Fehler bei der Berechnung: {ex.Message}");
                }
            }
        }





    }
}
