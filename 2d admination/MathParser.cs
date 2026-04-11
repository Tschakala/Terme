
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

public static class MathParser
{
    public static Func<double, double> Parse(string formula)
    {
        var xParam = Expression.Parameter(typeof(double), "x");
        Expression body = ParseExpression(formula, xParam);
        return Expression.Lambda<Func<double, double>>(body, xParam).Compile();
    }

    private static Expression ParseExpression(string expr, ParameterExpression x)
    {
        expr = expr.Replace(" ", "");
        return ParseAddSubtract(ref expr, x);
    }

    private static Expression ParseAddSubtract(ref string expr, ParameterExpression x)
    {
        var left = ParseMultiplyDivide(ref expr, x);

        while (expr.Length > 0)
        {
            if (expr[0] == '+')
            {
                expr = expr.Substring(1);
                left = Expression.Add(left, ParseMultiplyDivide(ref expr, x));
            }
            else if (expr[0] == '-')
            {
                expr = expr.Substring(1);
                left = Expression.Subtract(left, ParseMultiplyDivide(ref expr, x));
            }
            else break;
        }

        return left;
    }

    private static Expression ParseMultiplyDivide(ref string expr, ParameterExpression x)
    {
        var left = ParsePower(ref expr, x);

        while (expr.Length > 0)
        {
            if (expr[0] == '*')
            {
                expr = expr.Substring(1);
                left = Expression.Multiply(left, ParsePower(ref expr, x));
            }
            else if (expr[0] == '/')
            {
                expr = expr.Substring(1);
                left = Expression.Divide(left, ParsePower(ref expr, x));
            }
            else break;
        }

        return left;
    }

    private static Expression ParsePower(ref string expr, ParameterExpression x)
    {
        var left = ParseAtom(ref expr, x);

        while (expr.Length > 0 && expr[0] == '^')
        {
            expr = expr.Substring(1);
            left = Expression.Power(left, ParseAtom(ref expr, x));
        }

        return left;
    }


    private static Expression ParseAtom(ref string expr, ParameterExpression x)
    {
        if (expr.StartsWith("x"))
        {
            expr = expr.Substring(1);
            return x;
        }

        if (char.IsDigit(expr[0]))
        {
            int i = 0;
            while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.')) i++;
            double val = double.Parse(expr[..i], CultureInfo.InvariantCulture);
            expr = expr[i..];
            return Expression.Constant(val);
        }

        if (expr.StartsWith("("))
        {
            expr = expr.Substring(1);
            var inside = ParseAddSubtract(ref expr, x);
            expr = expr.Substring(1);
            return inside;
        }

        if (expr.StartsWith("sin")) return ParseFunc(ref expr, x, "sin");
        if (expr.StartsWith("cos")) return ParseFunc(ref expr, x, "cos");
        if (expr.StartsWith("tan")) return ParseFunc(ref expr, x, "tan");
        if (expr.StartsWith("ln")) return ParseFunc(ref expr, x, "ln");
        if (expr.StartsWith("log10")) return ParseFunc(ref expr, x, "log10");

        if (expr.StartsWith("log"))
        {
            expr = expr.Substring(3); 
            expr = expr.Substring(1); 
            Expression a = ParseAddSubtract(ref expr, x);
            expr = expr.Substring(1);
            Expression b = ParseAddSubtract(ref expr, x);
            expr = expr.Substring(1);

            return Expression.Call(
                typeof(Math).GetMethod("Log", new[] { typeof(double), typeof(double) })!,
                b, a
            );
        }

        throw new Exception("Unbekannter Ausdruck: " + expr);
    }



    private static Expression ParseFunc(ref string expr, ParameterExpression x, string func)
    {
        expr = expr.Substring(func.Length);
        expr = expr.Substring(1); 

        Expression inside = ParseAddSubtract(ref expr, x);
        expr = expr.Substring(1); 

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
