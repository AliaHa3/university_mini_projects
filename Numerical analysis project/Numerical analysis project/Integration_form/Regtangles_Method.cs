using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParserTK;

namespace Integration_form
{
    internal static class RegtanglesMethod
    {
        public static double Evaluate(string function, int a, int b, double h)
        {
            var parser = new MathParser();
            int n = (int) ((b - a)/h);
            var x = new double[n];
            var y = new double[n];
            double res = 0;
            for (int i = 0; i < n; i++)
            {
                x[i] = a + i*h;
                string temp = function.Replace("x", x[i].ToString());
                y[i] = parser.Parse(temp, false);
                res += y[i];
            }
            res = res*h;
            return res;
        }
    }
}