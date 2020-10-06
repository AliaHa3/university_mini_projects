using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParserTK;

namespace Numerical_analysis
{

    class Non_linear
    {
        private string function;
        private double a, b, epsilon=0.001;

         public  Non_linear(double first_field, double last_field,  string functions)
        {
            a = first_field;
            b = last_field;
            function = functions;
        }
 
        public  Non_linear(double first_field, double last_field, double eps, string functions)
        {
            a = first_field;
            b = last_field;
            epsilon = eps;
            function = functions;
        }

        public double find_bisection()
        {
            double c;
            string f1 = function, f2 = function, f3 = function;
            double fa , fb, fc;
            MathParser parser = new MathParser();
            f2 = replace(f2, b);
            fb = parser.Parse(f2);
            f1 = replace(f1, a);
            fa = parser.Parse(f1);

        start:
           
            c = (a+b)/2.0;
            f3 = replace(function, c);
            fc = parser.Parse(f3);

            if ((fc) == 0.0)
                return c;
            else if ((fa * fc) < 0.0)
            {
                b = c;
                f2 = replace(function, b);
                fb = parser.Parse(f2);

            }
            else
            {
                a = c;
                f1 = replace(function, a);
                fa = parser.Parse(f1);
            }

            if ((Math.Abs(a - b)) <= epsilon)
                return c;
            else
            {
                goto start;
            }


        }

        private string replace(string str, double x)
        {
            string str_temp = null;
            for (int i = 0; i < str.Length; i++)
            {
                if ( (i == str.Length -1 ) && (str[str.Length -1] == 'x') )
                {
                    str_temp += x.ToString();
                }
                
                 else if ((str[i] == 'x') && ( str[i + 1] != 'p'))
                {
                    str_temp += x.ToString();
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
