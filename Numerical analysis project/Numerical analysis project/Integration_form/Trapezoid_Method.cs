using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParserTK;

namespace Integration_form
{
    internal static class TrapezoidMethod
    {
        public static double Evaluate(string function, int a, int b, double h)
        {
            var parser = new MathParser();
            int n = (int) ((b - a)/h);
            var x = new double[n + 1];
            var y = new double[n + 1];
            double res = 0;
            x[0] = a;
            x[n] = b;
            string temp1 = function.Replace("x", x[0].ToString());
            y[0] = parser.Parse(temp1, false);
            temp1 = function.Replace("x", x[n].ToString());
            y[n] = parser.Parse(temp1, false);
            for (int i = 1; i < n; i++)
            {
                x[i] = a + i*h;
                string temp = function.Replace("x", x[i].ToString());
                y[i] = parser.Parse(temp, false);
                res = res + (2*y[i]);
            }
            res += y[0];
            res += y[n];
            res = res*(h/2);
            return res;
        }
    }
}