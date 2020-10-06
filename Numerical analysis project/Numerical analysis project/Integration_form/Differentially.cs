using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParserTK;

namespace Numerical_analysis
{
    class Differentially
    {
        private double h, x0, y0, a, b;// [a,b] 
        private string function;
        public int n;

        public Differentially(double height, double xzero, double yzero, double first_field, double last_field, string functions)
        {
            h = height;
            x0 = xzero;
            y0 = yzero;
            a = first_field;
            b = last_field;
            function = functions;
            n = Convert.ToInt16((b - a) / h);
          
        }

        public void calculate_RongCuta(double[,] s)
        { 

            double k1, k2, k3, k4;
            double xnew = 0, ynew, x = x0, y = y0;
         

            int i = 0;
            while (i <= n)
            {
                string f = function;
                f = replace(function, x, y);
                MathParser parser = new MathParser();

                k1 = h * (parser.Parse(f, true));

                f = replace(function, (x + (h / 2)), (y + (k1 / 2)));
                k2 = h * (parser.Parse(f, true));

                f = replace(function, (x + (h / 2)), (y + (k2 / 2)));
                k3 = h * (parser.Parse(f, true));

                f = replace(function, (x + h), (y + k3));
                k4 = h * (parser.Parse(f, true));

                s[i, 0] = x;
                s[i, 1] = y;
                ynew = y + (k1 + (2 * k2) + (2 * k3) + k4) / 6;
                xnew = xnew + h;

                x = xnew;
                y = ynew;

                i++;


            }


        }


        public void calculate_uler(double[,] s)
        {
            
            int i = 0;
            double xnew = 0, ynew, x = x0, y = y0;
            string dfY = function;
            MathParser parser = new MathParser();
            
            while (i <= n)
            {

                dfY = replace(function, x, y);

               
                ynew = y + (h*(parser.Parse(dfY, true)));
                s[i, 0] = x;
                s[i, 1] = ynew;
                xnew = xnew + h;

                x = xnew;
                y = ynew;


                i++;

            }

        }


        private string replace(string str, double x, double y = 1)
        {
            string str_temp = null;
            for (int i = 0; i < str.Length; i++)
            {
                if ((i == str.Length - 1) && (str[str.Length - 1] == 'x'))
                {
                    str_temp += x.ToString();
                }
                else if ((str[i] == 'x') && (str[i + 1] != 'p'))
                {
                    str_temp += x.ToString();
                }

                else if (str[i] == 'y')
                {
                    str_temp += y.ToString();
                }
                else
                {
                    str_temp += str[i];
                }
            }
            return str_temp;

        }

    }
}
