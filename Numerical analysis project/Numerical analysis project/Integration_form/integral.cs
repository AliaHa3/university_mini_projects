using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParserTK;

namespace Numerical_analysis
{
    public class IntegralClass
    {
          public string FunctionExp { set; get; }
          public double[] XAxis;
          public double[] YAxis;
          public double Start { set; get; }
          public double End { set; get; }
          public double Accuracy { set; get; }
          public int NumSubdivisions { set; get; }
        public double RegtanglesMethod (string function)
        {
            FunctionExp = function;
            var parser = new MathParser();
            XAxis = new double[NumSubdivisions + 1];
            YAxis = new double[NumSubdivisions + 1];
            double res = 0;
            XAxis[0] = this.Start;
            XAxis[NumSubdivisions] = this.End;
            string temp1 = function.Replace("x", XAxis[0].ToString());
            YAxis[0] = parser.Parse(temp1, false);
            temp1 = function.Replace("x", XAxis[NumSubdivisions].ToString());
            YAxis[NumSubdivisions] = parser.Parse(temp1, false);
            for (int i = 1; i < NumSubdivisions; i++)
            {
                XAxis[i] = this.Start + i * this.Accuracy;
                string temp = function.Replace("x", XAxis[i].ToString());
                YAxis[i] = parser.Parse(temp, false);
                res += YAxis[i];
            }
            res += YAxis[0];

            res = res * this.Accuracy;
            return res;
        }
        public  double TrapezoidMethod (string function)
        {
            FunctionExp = function;
            var parser = new MathParser();
            XAxis = new double[NumSubdivisions+1];
            YAxis = new double[NumSubdivisions+1];
            double res = 0;
            XAxis[0] = this.Start;
            XAxis[NumSubdivisions] = this.End;
            string temp1 = function.Replace("x", XAxis[0].ToString());
            YAxis[0] = parser.Parse(temp1, false);
            temp1 = function.Replace("x", XAxis[NumSubdivisions].ToString());
            YAxis[NumSubdivisions] = parser.Parse(temp1, false);
            for (int i = 1; i < NumSubdivisions; i++)
            {
                XAxis[i] = this.Start + i * this.Accuracy;
                string temp = function.Replace("x", XAxis[i].ToString());
                YAxis[i] = parser.Parse(temp, false);
                res = res + (2 * YAxis[i]);
            }
            res += YAxis[0];
            res += YAxis[NumSubdivisions];
            res = res * (this.Accuracy / 2);
            return res;
        }
        public double SimpsonMethod(string function)
        {
            FunctionExp = function;
            var parser = new MathParser();
            XAxis = new double[NumSubdivisions + 1];
            YAxis = new double[NumSubdivisions + 1];
            double res = 0;
            XAxis[0] = this.Start;
            XAxis[NumSubdivisions] = this.End;
            string temp1 = function.Replace("x", XAxis[0].ToString());
            YAxis[0] = parser.Parse(temp1, false);
            temp1 = function.Replace("x", XAxis[NumSubdivisions].ToString());
            YAxis[NumSubdivisions] = parser.Parse(temp1, false);
            for (int i = 1; i < NumSubdivisions; i++)
            {
                XAxis[i] = this.Start + i * Accuracy;
                string temp = function.Replace("x", XAxis[i].ToString());
                YAxis[i] = parser.Parse(temp, false);
                if (i % 2 == 0)
                    res = res + (2 * YAxis[i]);
                else
                    res = res + (4 * YAxis[i]);
            }
            res += YAxis[0];
            res += YAxis[NumSubdivisions];
            res = res * (Accuracy / 3);
            return res;
        }
        public void SetN(double a,double b,double h)
        {
            NumSubdivisions = (int)((b - a) / h);
            
            Start = a;
            End = b;
            Accuracy = h;
        }
        public void SetH(double a,double b,int n)
        {
            Accuracy = (b - a) / Convert.ToDouble(n);
            Start = a;
            End = b;
            NumSubdivisions = n;
            
        }
    }
}
