using System;
using System.Collections.Generic;

namespace Interpolation
{
    public class InterpolationAlgorithms
    {
        public static double Linear(double[] xarray, double[] yarray, double x)
        {
            double y = double.NaN;
            for (int i = 0; i < xarray.Length - 1; i++)
            {
                if (x >= xarray[i] && x < xarray[i + 1])
                {
                    y = yarray[i] + (x - xarray[i]) * (yarray[i + 1] - yarray[i]) / (xarray[i + 1] - xarray[i]);
                }
            }
            return y;
        }

        public static double[] Linear(double[] xarray, double[] yarray, double[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = Linear(xarray, yarray, x[i]);
            return y;
        }

        public static double Lagrangian(double[] xarray, double[] yarray, double x)
        {
            double y = 0.0;
            double product = yarray[0];
            for (int i = 0; i < xarray.Length; i++)
            {
                product = yarray[i];
                for (int j = 0; j < xarray.Length; j++)
                {
                    if (i != j)
                    {
                        product *= (x - xarray[j]) / (xarray[i] - xarray[j]);
                    }
                }
                y += product;
            }
            return y;
        }

        public static double[] Lagrangian(double[] xarray, double[] yarray, double[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = Lagrangian(xarray, yarray, x[i]);
            return y;
        }

        public static double Barycentric(double[] xarray, double[] yarray, double x)
        {
            double product;
            double dx;
            double c1 = 0;
            double c2 = 0;
            int n = xarray.Length;
            double[] wt = new double[n];

            for (int i = 0; i < n; i++)
            {
                product = 1;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        product *= (xarray[i] - xarray[j]);
                        wt[i] = 1.0 / product;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                dx = wt[i] / (x - xarray[i]);
                c1 += yarray[i] * dx;
                c2 += dx;
            }
            return c1 / c2;
        }

        public static double[] Barycentric(double[] xarray, double[] yarray, double[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = Barycentric(xarray, yarray, x[i]);
            return y;
        }

        public static double NewtonDividedDifference(double[] xarray, double[] yarray, double x)
        {
            double y;
            int n = xarray.Length;
            double[] temp = new double[n];
            for (int i = 0; i < n; i++)
            {
                temp[i] = yarray[i];
            }

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    temp[j] = (temp[j - 1] - temp[j]) / (xarray[j - 1 - i] - xarray[j]);
                }
            }

            y = temp[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                y = temp[i] + (x - xarray[i]) * y;
            }
            return y;
        }

        public static double[] NewtonDividedDifference(double[] xarray, double[] yarray, double[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = NewtonDividedDifference(xarray, yarray, x[i]);
            return y;
        }

        public static double Spline(double[] xarray, double[] yarray, double x)
        {
            double[] xa = new double[xarray.Length + 1];
            double[] ya = new double[yarray.Length + 1];
            xa[0] = (1.0 - 1.0e-6) * xarray[0];
            ya[0] = (1.0 - 1.0e-6) * yarray[0];
            for (int i = 0; i < xarray.Length; i++)
            {
                xa[i + 1] = xarray[i];
                ya[i + 1] = yarray[i];
            }
            xarray = xa;
            yarray = ya;

            double d1, d2;
            double y = double.NaN;
            int n = xarray.Length;
            double[] dx = new double[n];
            double[] derivative = SecondDerivatives(xarray, yarray);

            for (int i = 1; i < n; i++)
            {
                dx[i] = xarray[i] - xarray[i - 1];
            }

            for (int i = 1; i < n - 1; i++)
            {
                if (x >= xarray[i] && x < xarray[i + 1])
                {
                    d1 = x - xarray[i];
                    d2 = xarray[i + 1] - x;
                    y = derivative[i - 1] * d2 * d2 * d2 / (6.0 * dx[i + 1]) +
                        derivative[i] * d1 * d1 * d1 / (6.0 * dx[i + 1]) +
                        (yarray[i + 1] / dx[i + 1] - derivative[i] * dx[i + 1] / 6.0) * d1 +
                        (yarray[i] / dx[i + 1] - derivative[i - 1] * dx[i + 1] / 6.0) * d2;
                }
            }

            return y;
        }

        private static double[] SecondDerivatives(double[] xarray, double[] yarray)
        {
            int n = xarray.Length;
            double[] c1 = new double[n];
            double[] c2 = new double[n];
            double[] c3 = new double[n];
            double[] dx = new double[n];
            double[] derivative = new double[n];

            for (int i = 1; i < n; i++)
            {
                dx[i] = xarray[i] - xarray[i - 1];
                derivative[i] = (yarray[i] - yarray[i - 1]) / dx[i];
            }

            for (int i = 1; i < n - 1; i++)
            {
                c2[i - 1] = 2;
                c3[i - 1] = dx[i + 1] / (dx[i] + dx[i + 1]);
                c1[i - 1] = 1 - c3[i - 1];
                derivative[i - 1] = 6 * (derivative[i + 1] - derivative[i]) / (dx[i] + dx[i + 1]);
            }

            derivative = Tridiagonal(n - 2, c1, c2, c3, derivative);
            return derivative;
        }

        private static double[] Tridiagonal(int n, double[] c1, double[] c2, double[] c3, double[] derivative)
        {
            double tol = 1.0e-12;
            bool isSingular = (c2[0] < tol) ? true : false;

            for (int i = 1; i < n && !isSingular; i++)
            {
                c1[i] = c1[i] / c2[i - 1];
                c2[i] = c2[i] - c1[i] * c3[i - 1];
                isSingular = (c2[i] < tol) ? true : false;
                derivative[i] = derivative[i] - c1[i] * derivative[i - 1];
            }

            if (!isSingular)
            {
                derivative[n - 1] = derivative[n - 1] / c2[n - 1];
                for (int i = n - 2; i >= 0; i--)
                {
                    derivative[i] = (derivative[i] - c3[i] * derivative[i + 1]) / c2[i];
                }
                return derivative;
            }
            else
                return null;
        }

        public static double[] Spline(double[] xarray, double[] yarray, double[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = Spline(xarray, yarray, x[i]);
            return y;
        }

        public static double Bilinear(double[] xarray, double[] yarray, double[,] zarray, double x, double y)
        {
            double z = double.NaN;
            for (int i = 0; i < xarray.Length - 1; i++)
            {
                for (int j = 0; j < yarray.Length - 1; j++)
                {
                    if (x >= xarray[i] && x < xarray[i + 1] && y >= yarray[j] && y < yarray[j + 1])
                    {
                        z = zarray[i, j] * (xarray[i + 1] - x) * (yarray[j + 1] - y) / (xarray[i + 1] - xarray[i]) / (yarray[j + 1] - yarray[j]) +
                            zarray[i + 1, j] * (x - xarray[i]) * (yarray[j + 1] - y) / (xarray[i + 1] - xarray[i]) / (yarray[j + 1] - yarray[j]) +
                            zarray[i, j + 1] * (xarray[i + 1] - x) * (y - yarray[j]) / (xarray[i + 1] - xarray[i]) / (yarray[j + 1] - yarray[j]) +
                            zarray[i + 1, j + 1] * (x - xarray[i]) * (y - yarray[j]) / (xarray[i + 1] - xarray[i]) / (yarray[j + 1] - yarray[j]);
                    }
                }
            }
            return z;
        }
    }
}
