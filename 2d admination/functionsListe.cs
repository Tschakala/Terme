using System;
using System.Collections.Generic;
using System.Text;

namespace _2d_admination
{
    internal class functionsListe
    {
        List<function> functions = new List<function>();

        public string getfunctionlist
        {
            get
            {
                if (functions.Count != 0)
                {
                    string result = " ----- FunktionenBibliothek ----- ";
                    int c = 1;
                    foreach (function f in functions)
                    {
                        result += "\n" + c + ": " + f + "\n--------------------------------------------------------------------------------";
                        c++;
                    }
                    return result;
                }
                return "Keine Funktionen in der Liste vorhanden.";
            }
        }
        public void AddFunction(function f)
        {
            functions.Add(f);
        }
        public string GetFormula(int index)
        {
            return functions[index-1].getformula();
        }
        public double GetA(int index)
        {
            return functions[index-1].geta();
        }
        public double GetB(int index)
        {
            return functions[index-1].getb();
        }
        public double GetC(int index)
        {
            return functions[index-1].getc();
        }
        public int GetFrames(int index)
        {
            return functions[index-1].getframes();
        }   
    }
}
