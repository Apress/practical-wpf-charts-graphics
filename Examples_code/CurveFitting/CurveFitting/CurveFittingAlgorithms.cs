using System;
using System.Collections;

namespace CurveFitting
{
    public class CurveFittingAlgorithms
    {
        public static double[] StraightLineFit(double[] xarray, double[] yarray)
        {
            int n = xarray.Length;
            double xm = 0.0;
            double ym = 0.0;
            double b1 = 0.0;
            double b2 = 0.0;
            double a = 0.0;
            double b = 0.0;
            double s = 0.0;
            double sigma = 0.0;

            for (int i = 0; i < n; i++)
            {
                xm += xarray[i] / n;
                ym += yarray[i] / n;
            }

            for (int i = 0; i < n; i++)
            {
                b1 += yarray[i] * (xarray[i] - xm);
                b2 += xarray[i] * (xarray[i] - xm);
            }
            b = b1 / b2;
            a = ym - xm * b;

            for (int i = 0; i < n; i++)
            {
                s += (yarray[i] - a - b * xarray[i]) * (yarray[i] - a - b * xarray[i]);
            }
            sigma = Math.Sqrt(s / (n - 2));

            return new double[] {a, b, sigma };
        }

        public delegate double ModelFunction(double x);
        public static VectorR LinearRegression(double[] xarray, double[] yarray, ModelFunction[] f, out double sigma)
        {
            int m = f.Length;
            MatrixR A = new MatrixR(m, m);
            VectorR b = new VectorR(m);
            int n = xarray.Length;

            for (int k = 0; k < m; k++)
            {
                b[k] = 0.0;
                for (int i = 0; i < n; i++)
                {
                    b[k] += f[k](xarray[i]) * yarray[i];
                }
            }

            for (int j = 0; j < m; j++)
            {
                for (int k = 0; k < m; k++)
                {
                    A[j, k] = 0.0;
                    for (int i = 0; i < n; i++)
                    {
                        A[j, k] += f[j](xarray[i]) * f[k](xarray[i]);

                    }
                }
            }

            //LinearSystem ls = new LinearSystem();
            //VectorR coef = ls.GaussJordan(A, b);
            VectorR coef = GaussJordan(A, b);

            // Calculate the standard deviation:
            double s = 0.0;

            for (int i = 0; i < n; i++)
            {
                double s1 = 0.0;
                for (int j = 0; j < m; j++)
                {
                    s1 += coef[j] * f[j](xarray[i]);
                }
                s += (yarray[i] - s1) * (yarray[i] - s1);
            }
            sigma = Math.Sqrt(s / (n - m));

            return coef;
        }

        public static VectorR PolynomialFit(double[] xarray, double[] yarray, int m, out double sigma)
        {
            m++;
            MatrixR A = new MatrixR(m, m);
            VectorR b = new VectorR(m);
            int n = xarray.Length;

            for (int k = 0; k < m; k++)
            {
                b[k] = 0.0;
                for (int i = 0; i < n; i++)
                {
                    b[k] += Math.Pow(xarray[i], k) * yarray[i];
                }
            }

            for (int j = 0; j < m; j++)
            {
                for (int k = 0; k < m; k++)
                {
                    A[j, k] = 0.0;
                    for (int i = 0; i < n; i++)
                    {
                        A[j, k] += Math.Pow(xarray[i], j + k);
                    }
                }
            }

            VectorR coef = GaussJordan(A, b);

            // Calculate the standard deviation:
            double s = 0.0;

            for (int i = 0; i < n; i++)
            {
                double s1 = 0.0;
                for (int j = 0; j < m; j++)
                {
                    s1 += coef[j] * Math.Pow(xarray[i], j);
                }
                s += (yarray[i] - s1) * (yarray[i] - s1);
            }
            sigma = Math.Sqrt(s / (n - m));

            return coef;
        }

        public static double[] WeightedLinearRegression(double[] xarray, double[] yarray, double[] warray)
        {
            int n = xarray.Length;
            double xw = 0.0;
            double yw = 0.0;
            double b1 = 0.0;
            double b2 = 0.0;
            double a = 0.0;
            double b = 0.0;

            for (int i = 0; i < n; i++)
            {
                xw += xarray[i] / n;
                yw += yarray[i] / n;
            }

            for (int i = 0; i < n; i++)
            {
                b1 += warray[i] * warray[i] * yarray[i] * (xarray[i] - xw);
                b2 += warray[i] * warray[i] * xarray[i] * (xarray[i] - xw);
            }
            b = b1 / b2;
            a = yw - xw * b;

            return new double[] { a, b };
        }

        public static VectorR GaussJordan(MatrixR A, VectorR b)
        {
            Triangulate(A, b);
            int n = b.GetSize();
            VectorR x = new VectorR(n);
            for (int i = n - 1; i >= 0; i--)
            {
                double d = A[i, i];
                if (Math.Abs(d) < 1.0e-500)
                    throw new ArgumentException("Diagonal element is too small!");
                x[i] = (b[i] - VectorR.DotProduct(A.GetRowVector(i), x)) / d;
            }
            return x;
        }

        private static void Triangulate(MatrixR A, VectorR b)
        {
            int n = A.GetRows();
            VectorR v = new VectorR(n);
            for (int i = 0; i < n - 1; i++)
            {
                double d = Pivot(A, b, i);
                if (Math.Abs(d) < 1.0e-500)
                    throw new ArgumentException("Diagonal element is too small!");
                for (int j = i + 1; j < n; j++)
                {
                    double dd = A[j, i] / d;
                    for (int k = i + 1; k < n; k++)
                    {
                        A[j, k] -= dd * A[i, k];
                    }
                    b[j] -= dd * b[i];
                }
            }
        }

        private static double Pivot(MatrixR A, VectorR b, int q)
        {
            int n = b.GetSize();
            int i = q;
            double d = 0.0;
            for (int j = q; j < n; j++)
            {
                double dd = Math.Abs(A[j, q]);
                if (dd > d)
                {
                    d = dd;
                    i = j;
                }
            }
            if (i > q)
            {
                A.GetRowSwap(q, i);
                b.GetSwap(q, i);
            }
            return A[q, q];
        }
    }
}
