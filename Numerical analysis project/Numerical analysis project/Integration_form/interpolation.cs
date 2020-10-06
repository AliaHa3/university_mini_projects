using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polynomials;

namespace Numerical_analysis
{
    public class InterpolationClass
    {
        public InterpolationClass()
        {

        }
        public void Setvalue(int N, double[] xx, double[] yy)
        {
            NumPoint = N;
            Xaxis = new double[N];
            Yaxis = new double[N];
            for (int i = 0; i < NumPoint; i++)
            {
                Xaxis[i] = xx[i]; 
                Yaxis[i] = yy[i];
            }
        }
        public double[] Xaxis;
        public double[] Yaxis;
        public int NumPoint;
        public Polynomial poly = new Polynomial();
        public double CalculteSplinePoint(double x)
        {
            double numerator = 1, denominator = 1, res = 1;
            for (int i = 0; i < NumPoint; i++)
            {
                if ((x > Xaxis[i]) && (x < Xaxis[i + 1]))
                {
                    numerator = Yaxis[i + 1] - Yaxis[i];
                    denominator = Xaxis[i + 1] - Xaxis[i];
                    res = (numerator / denominator) * (x - Xaxis[i]);
                    res += Yaxis[i];
                }
            }
            return res;
        }
        public string CalculateLagrangePolynomial()
        {
            Polynomial numerator, term;
            Polynomial LagrangePolynomial = new Polynomial("0");
            for (int j = 0; j < NumPoint; j++)
            {
                numerator = new Polynomial("1");
                double denominator = 1;
                for (int i = 0; i < NumPoint; i++)
                {
                    if (j != i)
                    {
                        if (Xaxis[i] > 0)
                            term = new Polynomial("x-" + Xaxis[i].ToString());
                        else
                        {
                            double t = -1 * Xaxis[i];
                            term = new Polynomial("x+" + t.ToString());
                        }
                        numerator = numerator.Mul(term);
                        denominator *= (Xaxis[j] - Xaxis[i]);
                    }
                }
                double[] coefficients = { Yaxis[j] / denominator };
                Polynomial final_term = new Polynomial(coefficients);
                final_term = final_term.Mul(numerator);
                LagrangePolynomial = LagrangePolynomial.Add(final_term);
            }
            poly = LagrangePolynomial;
            return LagrangePolynomial.ToString();
        }
        public double CalculateLagrangePoint(double x)
        {
            double res = 0;
            for (int j = 0; j < NumPoint; j++)
            {
                double numerator = 1, denominator = 1;
                for (int i = 0; i < NumPoint; i++)
                {

                    if (j != i)
                    {
                        numerator *= (x - Xaxis[i]);
                        denominator *= (Xaxis[j] - Xaxis[i]);
                    }
                }

                res += (Yaxis[j] * (numerator / denominator));
            }
            return res;
        }
        public double CalculateNewtonPoint(double xValue)
        {
            int n = NumPoint - 1;
            double numerator = 1, denominator = 1, h, p;
            h = Xaxis[1] - Xaxis[0];
            p = (xValue - Xaxis[0]) / h;
            var differences = new double[10, 10];
            double finalAnswer = Yaxis[0];
            for (int i = 0; i < n; i++)
                differences[i, 1] = Yaxis[i + 1] - Yaxis[i];
            for (int j = 2; j < n + 1; j++)
                for (int i = 0; i <= n - j + 1; i++)
                        differences[i, j] = differences[i + 1, j - 1] - differences[i, j - 1];
            for (int k = 1; k < n; k++)
            {

                numerator *= (p - k + 1);
                denominator *= k;
                finalAnswer += ((numerator / denominator) * differences[0, k]);

            }
            return finalAnswer;
        }
        public string CalculateNewtonPolynomial()
        {
            Polynomial numerator, temp, p, pp;

            pp = new Polynomial("1");
            Polynomial NewtonPolynomial = new Polynomial(Yaxis[0].ToString());
            if (Xaxis[0] > 0)
                p = new Polynomial("x-" + Xaxis[0].ToString());
            else
                p = new Polynomial("x+" + ((-1 * Xaxis[0]).ToString()));

            double[] h = { Xaxis[1] - Xaxis[0] };
            temp = new Polynomial(h);
            p = p.Div(temp);
            double denominator = 1;
            var differences = new double[NumPoint + 1, NumPoint + 1];
            var temp1 = new double[NumPoint + 1];
            temp[0] = Yaxis[0];
            for (int i = 0; i < NumPoint - 1; i++)
                differences[i, 1] = Yaxis[i + 1] - Yaxis[i];
            for (int j = 2; j < NumPoint + 1; j++) // diffrences matrix
            {
                for (int i = 0; i <= NumPoint - j + 1; i++)
                    differences[i, j] = differences[i + 1, j - 1] - differences[i, j - 1];
                temp1[j - 1] = differences[0, j - 1];

            }
            for (int j = 1; j < NumPoint; j++)
            {
                var calj = new Polynomial((j - 1).ToString());
                temp = p.Sub(calj);
                pp.MulA(temp);
                denominator *= j;
                double[] coefficients = { temp1[j] / denominator };
                var finalTerm = new Polynomial(coefficients);
                finalTerm = finalTerm.Mul(pp);
                NewtonPolynomial = NewtonPolynomial.Add(finalTerm);
            }
            this.poly = NewtonPolynomial;
            return NewtonPolynomial.ToString();
        }
    }
}
