
using System;
using System.Globalization;
using System.Linq.Expressions;

public static class MathParser
{
    public static Func<double, double, double, double, double> Parse(string formula)
    {
        var xParam = Expression.Parameter(typeof(double), "x");
        var aParam = Expression.Parameter(typeof(double), "a");
        var bParam = Expression.Parameter(typeof(double), "b");
        var cParam = Expression.Parameter(typeof(double), "c");

        Expression body = ParseExpression(formula.Replace(" ", ""), xParam, aParam, bParam, cParam);

        return Expression.Lambda<Func<double, double, double, double, double>>(body, xParam, aParam, bParam, cParam).Compile();
    }


    private static Expression ParseExpression(string expr, ParameterExpression x, ParameterExpression a, ParameterExpression b, ParameterExpression c)
    {
        return ParseAddSubtract(ref expr, x, a, b, c);
    }


    private static Expression ParseAddSubtract(ref string expr, ParameterExpression x, ParameterExpression a, ParameterExpression b, ParameterExpression c)
    {
        var left = ParseMultiplyDivide(ref expr, x, a, b, c);

        while (expr.Length > 0)
        {
            if (expr[0] == '+')
            {
                expr = expr[1..];
                left = Expression.Add(left, ParseMultiplyDivide(ref expr, x, a, b, c));
            }
            else if (expr[0] == '-')
            {
                expr = expr[1..];
                left = Expression.Subtract(left, ParseMultiplyDivide(ref expr, x, a, b, c));
            }
            else break;
        }

        return left;
    }


    private static Expression ParseMultiplyDivide(ref string expr, ParameterExpression x, ParameterExpression a, ParameterExpression b, ParameterExpression c)
    {
        var left = ParsePower(ref expr, x, a, b, c);

        while (expr.Length > 0)
        {
            if (expr[0] == '*')
            {
                expr = expr[1..];
                left = Expression.Multiply(left, ParsePower(ref expr, x, a, b, c));
            }
            else if (expr[0] == '/')
            {
                expr = expr[1..];
                left = Expression.Divide(left, ParsePower(ref expr, x, a, b, c));
            }
            else break;
        }

        return left;
    }


    private static Expression ParsePower(ref string expr, ParameterExpression x, ParameterExpression a, ParameterExpression b, ParameterExpression c)
    {
        var left = ParseAtom(ref expr, x, a, b, c);

        while (expr.Length > 0 && expr[0] == '^')
        {
            expr = expr[1..];
            left = Expression.Power(left, ParseAtom(ref expr, x, a, b, c));
        }
        return left;
    }

    private static Expression ParseAtom(ref string expr, ParameterExpression x, ParameterExpression a, ParameterExpression b, ParameterExpression c)
    {
        if (expr.StartsWith("sin")) return ParseFunction(ref expr, x, a, b, c, "sin");
        if (expr.StartsWith("cos")) return ParseFunction(ref expr, x, a, b, c, "cos");
        if (expr.StartsWith("tan")) return ParseFunction(ref expr, x, a, b, c, "tan");
        if (expr.StartsWith("ln")) return ParseFunction(ref expr, x, a, b, c, "ln");
        if (expr.StartsWith("log10")) return ParseFunction(ref expr, x, a, b, c, "log10");

        if (expr.StartsWith("log"))
        {
            expr = expr[3..];
            expr = expr[1..];

            Expression baseA = ParseAddSubtract(ref expr, x, a, b, c);
            expr = expr[1..];
            Expression valueB = ParseAddSubtract(ref expr, x, a, b, c);
            expr = expr[1..];

            return Expression.Call(typeof(Math).GetMethod("Log", new[] { typeof(double), typeof(double) })!, 
                valueB, baseA);
        }

        if (expr.StartsWith("a")) 
        { 
            expr = expr[1..]; return a; 
        }
        if (expr.StartsWith("b")) 
        {
            expr = expr[1..]; return b;
        }
        if (expr.StartsWith("c")) 
        { 
            expr = expr[1..]; return c; 
        }
        if (expr.StartsWith("x")) 
        {
            expr = expr[1..]; return x;
        }

        if (char.IsDigit(expr[0]))
        {
            int i = 0;
            while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.')) i++;
            {
                double value = double.Parse(expr[..i], CultureInfo.InvariantCulture);
                expr = expr[i..];
                return Expression.Constant(value);
            }
        }

        if (expr.StartsWith("("))
        {
            expr = expr[1..];
            Expression inside = ParseAddSubtract(ref expr, x, a, b, c);
            expr = expr[1..];
            return inside;
        }

        throw new Exception("Unbekannter Ausdruck: " + expr);
    }



    private static Expression ParseFunction(ref string expr,
        ParameterExpression x, ParameterExpression a, ParameterExpression b, ParameterExpression c,
        string func)
    {
        expr = expr[func.Length..];
        expr = expr[1..];           

        Expression inside = ParseAddSubtract(ref expr, x, a, b, c);
        expr = expr[1..]; 

        return func switch
        {
            "sin" => Expression.Call(typeof(Math).GetMethod("Sin")!, inside),
            "cos" => Expression.Call(typeof(Math).GetMethod("Cos")!, inside),
            "tan" => Expression.Call(typeof(Math).GetMethod("Tan")!, inside),
            "ln" => Expression.Call(typeof(Math).GetMethod("Log", new[] { typeof(double) })!, inside),
            "log10" => Expression.Call(typeof(Math).GetMethod("Log10")!, inside),
            _ => throw new Exception("Unbekannte Funktion: " + func)
        };
    }
}
