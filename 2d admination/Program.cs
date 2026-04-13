using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;

namespace _2d_admination
{
    internal class Program
    {
        static void PrintRules()
        {
            Console.Clear();
            Console.WriteLine(" <---> Regeln <---> ");
            Console.WriteLine("Die Funktionen müssen immer folgender masen aufgebaut sein: \n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t y = <Funktion> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n <---> Unterstützte Funktionen <---> ");
            Console.Write("-> Pi und Eulersche Zahl: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("pi, e");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Variablen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("a, b, c");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Grundfunktionen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("+  -  *  /  ^  (Klammern)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> trigonometrische Funktionen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("sin, cos, tan");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> hyperbolische Funktionen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("sinh, cosh, tanh");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> inverse trigonometrische Funktionen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("asin, acos, atan, atan2");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Exponential- & Logarithmusfunktionen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("exp, ln, log10, log(x,base)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Wurzeln: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("sqrt, cbrt");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Hilfsfunktionen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("abs, round, floor, ceil");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Zwei-Argument-Funktionen: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("min(x,y), max(x,y), mod(x,y)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Zufallsfunktion: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("random(min,max)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-> Zahlen (positiv & negativ), polynomische Ausdrücke, verschachtelte Ausdrücke\n");
            Console.WriteLine(" <---> Beispiele <---> ");
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("sin(x+a)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  |  2. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("max(tanh(x)*10");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  |  3. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("-2)}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  |  4. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("random(0,5)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  |  5. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("sqrt(abs(x))");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  |  6. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("log(x,2)");
            Console.ReadLine();
        }
        static void addFunctionToList(functionsListe list1, functionsListe list2)
        {
            function f1 = new function("y = (3 * (tan(x) + sin(a * x^2)) * cos(x))^2", 0.01, 0, 0, 100);
            list1.AddFunction(f1);
            function f2 = new function("y = x * sin(tan(ln(a * x^2))) * cos(x)", 0.01, 0, 0, 100);
            list1.AddFunction(f2);
            function f3 = new function("y = a * x^2 + (b * c) * sin( b * x )", 0.01, 0.02, 10, 100);
            list1.AddFunction(f3);
            function f4 = new function("y = a * x * cos(b * x) * cos(c * x * 0.3)", 0.01, 0.025, 0.3, 100);
            list1.AddFunction(f4);
            function f5 = new function("y = 15 * cos(a * x - b) * sin(0.01 * x + c)", 0.01, 0.02, 0.03, 200);
            list1.AddFunction(f5);
            function f6 = new function("y = ln(25) * sin(x^2) * a * ln(x^2 + 1 + b * sin(c * x))", 0.01, 0.0025, 0.001, 75);
            list1.AddFunction(f6);
            function f7 = new function("y = a * sin(b * x) + c * cos(x / 2)", 0.09, 0.3, 0.1, 120);
            list1.AddFunction(f7);
            function f8 = new function("y = 0.25 * (sin(x) + cos(x)) * (a * x^2 + b * x + c)", 0.001, 0.01, 0.01, 100);
            list1.AddFunction(f8);
            function f9 = new function("y = a * (0.005 * x^3) + b * (0.02 * x^2) + c * sin(0.1 * x)", 0.01, -0.1, 0.0025, 50);
            list1.AddFunction(f9);
            function f10 = new function("y = a * sin(b * x + sin(c * x^2))", 0.1, 0.01, 0.02, 100);
            list1.AddFunction(f10);
            function f11 = new function("y = a * sin( ln(b * x^2 + 10) + c * x )", 0.3, 0.04, 0.02, 100);
            list1.AddFunction(f11);
            function f12 = new function("y = 0.1 * a * sin(b * x^2) + c * ln(cos(x))", 0.002, 0.003, 0.1, 100);
            list1.AddFunction(f12);
            function f13 = new function("y = a * cos(b * x) * cos(c * x^2 * 0.01)", 5, 0.2, 0.2, 25);
            list1.AddFunction(f13);
            function f14 = new function("y = ln(x) * a * cos(x)", 0.1, 0, 0, 50);
            list1.AddFunction(f14);
            function f15 = new function("y = 0.05 * a * x^3 + b^c", 0.001, 0.25, 0.05, 35);
            list1.AddFunction(f15);
            function f16 = new function("y = round(abs(sin((x + a) * 0.01) * 20))", 10, 0, 0, 100);
            list1.AddFunction(f16);
            function f17 = new function("y = 20*sin(tan(x^a))", 0.005, 0, 0, 150);
            list1.AddFunction(f17);
            function f18 = new function("y = 20 * sin(cos(x^(a*b*c)))", 0.01, 0.005, 0.001, 300);
            list1.AddFunction(f18);




            function f101 = new function("y = 10 * (sin( (x+a) * 0.1 ) + cos( x+b ) * 0.2 + cos( (x+c) * 7 ) * 0.08)", 2, 0.2, 1.4, 200);
            list2.AddFunction(f101);
            function f102 = new function("y = 12 * (sin((x + a) * 0.2) + cos((x + b) * 0.13) * 0.4 + sin((x + c) * 1.5) * 0.2)", 3, 0.2, 1.4, 200);
            list2.AddFunction(f102);
            function f103 = new function("y = 9 * sin((x + a) * 0.05 + sin(b)) + 5 * sin((x + b) * 0.04 + cos(c)) + 3 * cos((x + c) * 0.2)", 2, 0.08, 0.15, 180);
            list2.AddFunction(f103);
            function f104 = new function("y = 7 * cos((x + a) * 0.02) + 5 * cos((x + b) * 0.5) + 2 * cos((x + c) * 10)", 2.5, 1.04, 0.55, 160);
            list2.AddFunction(f104);
            function f105 = new function("y = 10 * sin((x + a) * 0.04) + 6 * sin((x + b) * 0.07) + 2 * cos((x + c) * 0.3)", 1.3, 0.07, 0.1, 160);
            list2.AddFunction(f105);
            function f106 = new function("y = 4 * cosh(sin((x + a) * 0.04)) + 3 * sin((x + b) * 0.3)", 1.9, 0.1, 0, 120);
            list2.AddFunction(f106);
            function f107 = new function("y = 8 * (sin((x + a) * 0.04) + sin((x + b) * 0.05 + sin(c)) + 0.5 * cos((x + c) * 0.9))", 3.6, 0.05, 0.1, 180);
            list2.AddFunction(f107);
            function f108 = new function("y = 14 * (sin((x + a) * 0.06) + cos((x + b) * (0.2 + 0.1 * sin(c))) * 0.4 + tanh(sin((x + c) * 0.015) * 2) * 0.7 + exp(-abs(x + a) * 0.03) * sin((x + b) * random(0.5, 1.5)) * 0.3)", 2.4, 0.05, 0.2, 220);
            list2.AddFunction(f108);
            function f109 = new function("y = 12 * (sin((x + a) * 0.1) + tanh((x + b) * 0.02) * 5 + 0.1 * cos((x + c) * 8))", 5, 0.01, 0.2, 200);
            list2.AddFunction(f109);
            function f110 = new function("y = 12 * (atan((x + a) * 0.05) + 0.5 * sin((x + b) * 0.3))", 2.5, 0.05, 0, 150);
            list2.AddFunction(f110);
            function f111 = new function("y = 10 * (sin((x + a) * 0.05)  + sin((x + a) * 0.052))", 2, 0, 0, 180);
            list2.AddFunction(f111);
            function f112 = new function("y = mod(x + a, 40) - 20 + 5 * sin((x + b) * 0.1) + 3 * cos((x + c) * 0.3)", 3, 0.1, 0.15, 120);
            list2.AddFunction(f112);
            function f113 = new function("y = 10 * (sin((x + a) * random(0.01, 0.1)) + cos((x + b) * random(0.01, 0.1)))", 1.5, 0.08, 0, 150);
            list2.AddFunction(f113);
            function f114 = new function("y = 5 * sinh(sin((x + a) * 0.1)) + 3 * cos((x + b) * 0.2) + 2 * sin((x + c) * 1.5)", 4.5, 0.1, 0.25, 200);
            list2.AddFunction(f114);
            function f115 = new function("y = 12 * (sin((x + a) * 0.1) * random(0.5, 1.5) + cos((x + b) * 0.05) + sin((x + c) * 4) * 0.2)", 1, 0.05, 0.3, 200);
            list2.AddFunction(f115);
            function f116 = new function("y = 10 * (sin((x + a) * (0.05 + 0.01 * sin(b))) + cos((x + b) * (0.03 + 0.02 * sin(c))) + 0.5 * sin((x + c) * 0.5))", 3, 0.05, 0.1, 220);
            list2.AddFunction(f116);
            function f117 = new function("y = 15 * (tanh(sin((x + a) * 0.1)) + 0.5 * sin((x + b) * 0.05) + 0.2 * cos((x + c) * 5))", 2, 0.05, 0.2, 200);
            list2.AddFunction(f117);
            function f118 = new function("y = 12 * (sin((x + a) * 0.2) + cos((x + b) * random(0.5, 3)) * 0.4 + sin((x + c) * 1.5) * random(0.3, 1.0))", 1.5, 0.15, 0.25, 200);
            list2.AddFunction(f118);

        }
        static void Main(string[] args)
        {
            int width = 150;
            int height = 50;
            string[,] grid = new string[width, height];

            functionsListe list1 = new functionsListe();
            functionsListe list2 = new functionsListe();
            addFunctionToList(list1, list2);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hallo, in diesem Programm können Sie Mathematische Funktionen rendern und animieren.");
            while (true)
            {
                int input = 0;
                bool valid = false;

                while (!valid)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. Wollen Sie in die FunktionenBibliothek schauen?");
                    Console.WriteLine("2. Wollen Sie selber eine Funktion eingeben?");
                    Console.WriteLine("3. Wollen Sie die Taschenrechnerfunktion nutzen?");
                    Console.WriteLine("4. Wollen Sie die Regeln des Programmes Lesen?");
                    Console.Write("Eingabe: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string raw = Console.ReadLine();

                    if (int.TryParse(raw, out input))
                    {
                        if (input == 1 || input == 2 || input == 3 || input == 4)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Bitte nur 1, 2, 3 oder 4 eingeben!");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ungültige Eingabe! Bitte eine ganze Zahl zwischen 1 und 9 eingeben.");
                    }
                }

                if (input == 1)
                {
                    Console.ForegroundColor= ConsoleColor.Yellow;
                    Console.Clear();
                    string userinput = "";
                    int input1 = 0;
                    Console.WriteLine("Welche Liste wollen Sie auswählen?");
                    Console.WriteLine("Liste 1: random Functions");
                    Console.WriteLine("Liste 2: Wave Functions");
                    Console.Write("Eingabe: ");
                    input1 = int.Parse(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Magenta;

                    if (input1 == 1)
                    {
                        Console.WriteLine(list1.getfunctionlist);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Welche Funktion möchten Sie auswählen?: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        userinput = Console.ReadLine();

                        Console.Clear();
                        Animation.animation(width, height, grid, list1.GetFormula(int.Parse(userinput)), list1.GetA(int.Parse(userinput)), list1.GetB(int.Parse(userinput)), list1.GetC(int.Parse(userinput)), list1.GetFrames(int.Parse(userinput)));
                    }
                    else if (input1 == 2)
                    {
                        Console.WriteLine(list2.getfunctionlist);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Welche Funktion möchten Sie auswählen?: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        userinput = Console.ReadLine();

                        Console.Clear();
                        Animation.animation(width, height, grid, list2.GetFormula(int.Parse(userinput)), list2.GetA(int.Parse(userinput)), list2.GetB(int.Parse(userinput)), list2.GetC(int.Parse(userinput)), list2.GetFrames(int.Parse(userinput)));
                    }
                }
                else if (input == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Geben Sie eine Funktion ein: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string formula = Console.ReadLine();

                    Console.Clear();
                    writeOwnFunction writeFunction = new writeOwnFunction(width, height, grid, formula);
                }
                else if (input == 3)
                {
                    Console.Clear();
                    Calculator calculator = new Calculator();
                }
                else if (input == 4)
                {
                    PrintRules();
                }

                if (input != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Wollen Sie nocheinmal das Programm ausführen?[y/n]: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string again = Console.ReadLine();
                    if (again.ToLower() == "n")
                    {
                        break;
                    }
                }

                Console.Clear();
            }
        }
    }
}
