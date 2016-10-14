using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace ColorsAndBrushes
{
    class BilinearInterpolation
    {
        public double Cmin { get; set; }
        public double Cmax { get; set; }
        public int Cdivisions { get; set; }
        public int NInterps { get; set; }
        public Canvas ChartCanvas { get; set; }


        private double BilinearCoeff(double x, double y, double X0, double Y0, double X1, double Y1, double C00, double C10, double C01, double C11)
        {
            return (Y1 - y) * ((X1 - x) * C00 + (x - X0) * C10) / (X1 - X0) / (Y1 - Y0) +
                   (y - Y0) * ((X1 - x) * C01 + (x - X0) * C11) / (X1 - X0) / (Y1 - Y0);
        }

        public void SetInterpShading(double X0, double Y0, double X1, double Y1, double C00, double C10, double C01, double C11)
        {
            ColormapBrush cb = new ColormapBrush();
            cb.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Jet;
            cb.Ymin = Cmin;
            cb.Ymax = Cmax;
            cb.Ydivisions = Cdivisions;
            double dx = (X1 - X0) / NInterps;
            double dy = (Y1 - Y0) / NInterps;

            for (int i = 0; i < NInterps; i++)
            {
                double x = X0 + i * dx;
                for (int j = 0; j < NInterps; j++)
                {
                    double y = Y0 + j * dy;
                    double C = BilinearCoeff(x, y, X0, Y0, X1, Y1, C00, C10, C01, C11);
                    Polygon plg = new Polygon();
                    plg.Points.Add(new Point(x, y));
                    plg.Points.Add(new Point(x, y + dy));
                    plg.Points.Add(new Point(x + dx, y + dy));
                    plg.Points.Add(new Point(x + dx, y));
                    plg.Fill = cb.GetBrush(C);
                    ChartCanvas.Children.Add(plg);
                }
            }
        }

        public void SetOriginalShading(double C, double X0, double Y0, double X1, double Y1)
        {
            ColormapBrush cb = new ColormapBrush();
            cb.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Jet;
            Polygon plg = new Polygon();
            cb.Ymin = Cmin;
            cb.Ymax = Cmax;
            cb.Ydivisions = Cdivisions;
            double dx = X1 - X0;
            double dy = Y1 - Y0;
            plg.Points.Add(new Point(X0, Y0));
            plg.Points.Add(new Point(X0, Y0 + dy));
            plg.Points.Add(new Point(X0 + dx, Y0 + dy));
            plg.Points.Add(new Point(X0 + dx, Y0));
            plg.Fill = cb.GetBrush(C);
            ChartCanvas.Children.Add(plg);
        }
    }
}
