using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Specialized3DChart
{
    public class DrawSurfaceChart
    {
        private SurfaceChartTypeEnum surfaceChartType = SurfaceChartTypeEnum.Mesh;
        private bool isColormap = true;
        private bool isHiddenLine = false;
        private bool isInterp = false;
        private int numberInterp = 2;
        private ColormapBrush colormap = new ColormapBrush();

        public ColormapBrush Colormap
        {
            get { return colormap; }
            set { colormap = value; }
        }

        public SurfaceChartTypeEnum SurfaceChartType
        {
            get { return surfaceChartType; }
            set { surfaceChartType = value; }
        }

        public bool IsColormap
        {
            get { return isColormap; }
            set { isColormap = value; }
        }

        public bool IsHiddenLine
        {
            get { return isHiddenLine; }
            set { isHiddenLine = value; }
        }

        public bool IsInterp
        {
            get { return isInterp; }
            set { isInterp = value; }
        }

        public int NumberInterp
        {
            get { return numberInterp; }
            set { numberInterp = value; }
        }

        public enum SurfaceChartTypeEnum
        {
            Surface = 1,
            Mesh = 2,
            MeshZ = 3,
            Waterfall =4
        }

        public void AddSurfaceChart(ChartStyle cs, DataSeriesSurface ds)
        {
            switch (SurfaceChartType)
            {
                case SurfaceChartTypeEnum.Mesh:
                    AddMesh(cs, ds);
                    break;
                case SurfaceChartTypeEnum.MeshZ:
                    AddMeshZ(cs, ds);
                    break;
                case SurfaceChartTypeEnum.Waterfall:
                    AddWaterfall(cs, ds);
                    break;
                case SurfaceChartTypeEnum.Surface:
                    AddSurface(cs, ds);
                    break;
           }
        }

        public void AddColorBar(ChartStyle cs, DataSeriesSurface ds, double zmin, double zmax)
        {
            TextBlock tb;
            tb = new TextBlock();
            tb.Text = "A";
            tb.FontFamily = cs.TickFont;
            tb.FontSize = cs.TickFontSize;
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size tickSize = tb.DesiredSize;

            double x = 6 * cs.ChartCanvas.Width / 7;
            double y = cs.ChartCanvas.Height / 10;
            double width = cs.ChartCanvas.Width / 25;
            double height = 8 * cs.ChartCanvas.Height / 10;
            Point3D[] pts = new Point3D[64];
            double dz = (zmax - zmin) / 63;

            // Create the color bar:
            Polygon plg;
            for (int i = 0; i < 64; i++)
            {
                pts[i] = new Point3D(x, y, zmin + i * dz);
            }
            for (int i = 0; i < 63; i++)
            {
                SolidColorBrush brush = GetBrush(pts[i].Z, zmin, zmax);
                double y1 = y + height - (pts[i].Z - zmin) * height / (zmax - zmin);
                double y2 = y + height - (pts[i + 1].Z - zmin) * height / (zmax - zmin);
                plg = new Polygon();
                plg.Points.Add(new Point(x, y2));
                plg.Points.Add(new Point(x + width, y2));
                plg.Points.Add(new Point(x + width, y1));
                plg.Points.Add(new Point(x, y1));
                plg.Fill = brush;
                plg.Stroke = brush;
                cs.ChartCanvas.Children.Add(plg);
            }
            Rectangle rect = new Rectangle();
            rect.Width = width + 2;
            rect.Height = height + 2;
            rect.Stroke = Brushes.Black;
            Canvas.SetLeft(rect, x - 1);
            Canvas.SetTop(rect, y - 1);
            cs.ChartCanvas.Children.Add(rect);

            // Add ticks and labels to the color bar:
            double tickLength = 0.15 * width;
            for (double z = zmin; z <= zmax; z = z + (zmax - zmin) / 6)
            {
                double yy = y + height - (z - zmin) * height / (zmax - zmin);
                AddTickLine(cs, new Point(x, yy), new Point(x + tickLength, yy));
                AddTickLine(cs, new Point(x + width, yy), new Point(x + width - tickLength, yy));
                tb = new TextBlock();
                tb.Text = (Math.Round(z, 2)).ToString();
                tb.FontFamily = cs.TickFont;
                tb.FontSize = cs.TickFontSize;
                cs.ChartCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, x + width + 5);
                Canvas.SetTop(tb, yy - tickSize.Height / 2);
            }
        }

        public void AddTickLine(ChartStyle cs, Point pt1, Point pt2)
        {
            Line line = new Line();
            line.X1 = pt1.X;
            line.Y1 = pt1.Y;
            line.X2 = pt2.X;
            line.Y2 = pt2.Y;
            line.Stroke = Brushes.Black;
            cs.ChartCanvas.Children.Add(line);          
        }

        public SolidColorBrush GetBrush(double z, double zmin, double zmax)
        {
            SolidColorBrush brush = new SolidColorBrush();
            Colormap.Ydivisions = (int)((zmax - zmin) / (Colormap.ColormapLength - 1));
            Colormap.Ymin = zmin;
            Colormap.Ymax = zmax;
            Colormap.Ydivisions = 64;
            int colorIndex = (int)(((Colormap.ColormapLength - 1) * (z - zmin) + zmax - z) / (zmax - zmin));
            if (colorIndex < 0)
                colorIndex = 0;
            if (colorIndex >= Colormap.ColormapLength)
                colorIndex = Colormap.ColormapLength - 1;
            brush = Colormap.ColormapBrushes()[colorIndex];
            return brush;
        }

        public void AddMesh(ChartStyle cs, DataSeriesSurface ds)
        {
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);
            Polygon plg = new Polygon();
            Point3D[,] pts = ds.PointArray;
            double[,] zValues = new double[pts.GetLength(0), pts.GetLength(1)];
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();

            for (int i = 0; i < pts.GetLength(0); i++)
            {
                for (int j = 0; j < pts.GetLength(1); j++)
                {
                    zValues[i, j] = pts[i, j].Z;
                    pts[i, j] = cs.Normalize3D(m, pts[i, j]);
                }
            }

            // Draw mesh chart:
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                int ii = i;
                if (cs.Elevation >= 0)
                {
                    ii = i;
                    if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                    {
                        ii = pts.GetLength(0) - 2 - i;
                    }
                }
                else
                {
                    ii = pts.GetLength(0) - 2 - i;
                    if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                    {
                        ii = i;
                    }
                }
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    int jj = j;
                    if (cs.Elevation < 0)
                    {
                        jj = pts.GetLength(1) - 2 - j;
                    }
                    plg = new Polygon();
                    plg.Points.Add(new Point(pts[ii, jj].X, pts[ii, jj].Y));
                    plg.Points.Add(new Point(pts[ii, jj + 1].X, pts[ii, jj + 1].Y));
                    plg.Points.Add(new Point(pts[ii + 1, jj + 1].X, pts[ii + 1, jj + 1].Y));
                    plg.Points.Add(new Point(pts[ii + 1, jj].X, pts[ii + 1, jj].Y));

                    plg.Stroke = Brushes.Black;
                    plg.StrokeThickness = ds.LineThickness;
                    plg.Fill = Brushes.White;
                    if (!IsHiddenLine)
                    {
                        plg.Fill = Brushes.Transparent;
                    }
                    if (IsColormap)
                    {
                        plg.Stroke = GetBrush(zValues[ii, jj], zmin, zmax);
                    }
                    cs.ChartCanvas.Children.Add(plg);
                }
            }
            if (cs.IsColorBar && IsColormap)
            {
                AddColorBar(cs, ds, zmin, zmax);
            }
        }

        private void AddMeshZ(ChartStyle cs, DataSeriesSurface ds)
        {
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);
            Polygon plg = new Polygon();
            Point3D[,] pts = ds.PointArray;
            Point3D[,] pts1 = new Point3D[pts.GetLength(0), pts.GetLength(1)];
            double[,] zValues = new double[pts.GetLength(0), pts.GetLength(1)];
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();

            for (int i = 0; i < pts.GetLength(0); i++)
            {
                for (int j = 0; j < pts.GetLength(1); j++)
                {
                    zValues[i, j] = pts[i, j].Z;
                    pts1[i, j] = new Point3D(pts[i, j].X, pts[i, j].Y, pts[i, j].Z);
                    pts[i, j] = cs.Normalize3D(m, pts[i, j]);
                }
            }

            // Draw mesh using the z-order method:
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                int ii = i;
                if (cs.Elevation >= 0)
                {
                    ii = i;
                    if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                    {
                        ii = pts.GetLength(0) - 2 - i;
                    }
                }
                else
                {
                    ii = pts.GetLength(0) - 2 - i;
                    if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                    {
                        ii = i;
                    }
                }
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    int jj = j;
                    if (cs.Elevation < 0)
                    {
                        jj = pts.GetLength(1) - 2 - j;
                    }
                    plg = new Polygon();
                    plg.Points.Add(new Point(pts[ii, jj].X, pts[ii, jj].Y));
                    plg.Points.Add(new Point(pts[ii, jj + 1].X, pts[ii, jj + 1].Y));
                    plg.Points.Add(new Point(pts[ii + 1, jj + 1].X, pts[ii + 1, jj + 1].Y));
                    plg.Points.Add(new Point(pts[ii + 1, jj].X, pts[ii + 1, jj].Y));

                    plg.Stroke = Brushes.Black;
                    plg.StrokeThickness = ds.LineThickness;
                    plg.Fill = Brushes.White;
                    if (!IsHiddenLine)
                    {
                        plg.Fill = Brushes.Transparent;
                    }
                    if (IsColormap)
                    {
                        plg.Stroke = GetBrush(zValues[ii, jj], zmin, zmax);
                    }
                    cs.ChartCanvas.Children.Add(plg);
                }
            }

            //Draw curtain lines:
            Point3D[] pta = new Point3D[4];
            for (int i = 0; i < pts1.GetLength(0); i++)
            {
                int jj = pts1.GetLength(0) - 1;
                if (cs.Elevation >= 0)
                {
                    if (cs.Azimuth >= -90 && cs.Azimuth <= 90)
                        jj = 0;
                }
                else if (cs.Elevation < 0)
                {
                    jj = 0;
                    if (cs.Azimuth >= -90 && cs.Azimuth <= 90)
                        jj = pts1.GetLength(0) - 1;
                }

                if (i < pts1.GetLength(0) - 1)
                {
                    pta[0] = new Point3D(pts1[i, jj].X, pts1[i, jj].Y, pts1[i, jj].Z);
                    pta[1] = new Point3D(pts1[i + 1, jj].X, pts1[i + 1, jj].Y, pts1[i + 1, jj].Z);
                    pta[2] = new Point3D(pts1[i + 1, jj].X, pts1[i + 1, jj].Y, cs.Zmin);
                    pta[3] = new Point3D(pts1[i, jj].X, pts1[i, jj].Y, cs.Zmin);
                    for (int k = 0; k < 4; k++)
                    {
                        pta[k] = cs.Normalize3D(m, pta[k]);
                    }
                    plg = new Polygon();
                    plg.Stroke = Brushes.Black;
                    plg.StrokeThickness = ds.LineThickness;
                    plg.Fill = Brushes.White;
                    plg.Points.Add(new Point(pta[0].X, pta[0].Y));
                    plg.Points.Add(new Point(pta[1].X, pta[1].Y));
                    plg.Points.Add(new Point(pta[2].X, pta[2].Y));
                    plg.Points.Add(new Point(pta[3].X, pta[3].Y));
                    if (!IsHiddenLine)
                    {
                        plg.Fill = Brushes.Transparent;
                    }
                    if (IsColormap)
                    {
                        plg.Stroke = GetBrush(pts1[i,jj].Z, zmin, zmax);
                    }
                    cs.ChartCanvas.Children.Add(plg);
                }
            }

            for (int j = 0; j < pts1.GetLength(1); j++)
            {
                int ii = 0;
                if (cs.Elevation >= 0)
                {
                    if (cs.Azimuth >= 0 && cs.Azimuth <= 180)
                    {
                        ii = pts1.GetLength(1) - 1;
                    }
                }
                else if (cs.Elevation < 0)
                {
                    if (cs.Azimuth >= -180 && cs.Azimuth <= 0)
                        ii = pts1.GetLength(1) - 1;
                }
                if (j < pts1.GetLength(1) - 1)
                {
                    pta[0] = new Point3D(pts1[ii, j].X, pts1[ii, j].Y, pts1[ii, j].Z);
                    pta[1] = new Point3D(pts1[ii, j + 1].X, pts1[ii, j + 1].Y, pts1[ii, j + 1].Z);
                    pta[2] = new Point3D(pts1[ii, j + 1].X, pts1[ii, j + 1].Y, cs.Zmin);
                    pta[3] = new Point3D(pts1[ii, j].X, pts1[ii, j].Y, cs.Zmin);
                    for (int k = 0; k < 4; k++)
                        pta[k] = cs.Normalize3D(m, pta[k]);
                    plg = new Polygon();
                    plg.Stroke = Brushes.Black;
                    plg.StrokeThickness = ds.LineThickness;
                    plg.Fill = Brushes.White;
                    plg.Points.Add(new Point(pta[0].X, pta[0].Y));
                    plg.Points.Add(new Point(pta[1].X, pta[1].Y));
                    plg.Points.Add(new Point(pta[2].X, pta[2].Y));
                    plg.Points.Add(new Point(pta[3].X, pta[3].Y));
                    if (!IsHiddenLine)
                    {
                        plg.Fill = Brushes.Transparent;
                    }
                    if (IsColormap)
                    {
                        plg.Stroke = GetBrush(pts1[ii, j].Z, zmin, zmax);
                    }
                    cs.ChartCanvas.Children.Add(plg);
                }
            }

            if (cs.IsColorBar && IsColormap)
            {
                AddColorBar(cs, ds, zmin, zmax);
            }
        }

        private void AddWaterfall(ChartStyle cs, DataSeriesSurface ds)
        {
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);
            Polygon plg = new Polygon();
            Point3D[,] pts = ds.PointArray;
            Point3D[] pt3 = new Point3D[pts.GetLength(0) + 2];
            double[] zValues = new double[pts.Length];
            Point[] pta = new Point[pts.GetLength(0) + 2];
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();

            for (int j = 0; j < pts.GetLength(1); j++)
            {
                int jj = j;
                if (cs.Elevation >= 0)
                {
                    if (cs.Azimuth >= -90 && cs.Azimuth < 90)
                    {
                        jj = pts.GetLength(1) - 1 - j;
                    }
                }
                else if (cs.Elevation < 0)
                {
                    jj = pts.GetLength(1) - 1 - j;
                    if (cs.Azimuth >= -90 && cs.Azimuth < 90)
                        jj = j;
                }
                for (int i = 0; i < pts.GetLength(0); i++)
                {
                    pt3[i + 1] = pts[i, jj];

                    if (i == 0)
                    {
                        pt3[0] = new Point3D(pt3[i + 1].X, pt3[i + 1].Y, cs.Zmin);
                    }
                    if (i == pts.GetLength(0) - 1)
                    {
                        pt3[pts.GetLength(0) + 1] = new Point3D(pt3[i + 1].X, pt3[i + 1].Y, cs.Zmin);
                    }
                }
                plg = new Polygon();
                for (int i = 0; i < pt3.Length; i++)
                {
                    zValues[i] = pt3[i].Z;
                    pt3[i] = cs.Normalize3D(m, pt3[i]);
                    pta[i] = new Point(pt3[i].X, pt3[i].Y);
                    plg.Points.Add(new Point(pt3[i].X, pt3[i].Y));
                }
                plg.Stroke = Brushes.Transparent;
                plg.StrokeThickness = ds.LineThickness;
                plg.Fill = Brushes.White;
                cs.ChartCanvas.Children.Add(plg);

                for (int i = 1; i < pt3.Length; i++)
                {
                    Line line = new Line();
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = ds.LineThickness;
                    if (IsColormap)
                    {
                        if (i < pt3.Length - 1)
                            line.Stroke = GetBrush(zValues[i], zmin, zmax);
                        else
                            line.Stroke = GetBrush(zValues[i - 1], zmin, zmax);
                    }
                    line.X1 = pta[i - 1].X;
                    line.Y1 = pta[i - 1].Y;
                    line.X2 = pta[i].X;
                    line.Y2 = pta[i].Y;
                    cs.ChartCanvas.Children.Add(line);
                }
            }
           
            if (cs.IsColorBar && IsColormap)
            {
                AddColorBar(cs, ds, zmin, zmax);
            }
        }

        public void AddSurface(ChartStyle cs, DataSeriesSurface ds)
        {
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);
            Polygon plg = new Polygon();
            Point3D[,] pts = ds.PointArray;
            Point3D[,] pts1 = new Point3D[pts.GetLength(0), pts.GetLength(1)];
            //double[,] zValues = new double[pts.GetLength(0), pts.GetLength(1)];
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();

            for (int i = 0; i < pts.GetLength(0); i++)
            {
                for (int j = 0; j < pts.GetLength(1); j++)
                {
                    //zValues[i, j] = pts[i, j].Z;
                    pts1[i, j] = pts[i, j];
                    pts[i, j] = cs.Normalize3D(m, pts[i, j]);
                }
            }

            // Draw surface chart:
            if (!IsInterp)
            {
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    int ii = i;
                    if (cs.Elevation >= 0)
                    {
                        ii = i;
                        if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                        {
                            ii = pts.GetLength(0) - 2 - i;
                        }
                    }
                    else
                    {
                        ii = pts.GetLength(0) - 2 - i;
                        if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                        {
                            ii = i;
                        }
                    }
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        int jj = j;
                        if (cs.Elevation < 0)
                        {
                            jj = pts.GetLength(1) - 2 - j;
                        }
                        plg = new Polygon();
                        plg.Points.Add(new Point(pts[ii, jj].X, pts[ii, jj].Y));
                        plg.Points.Add(new Point(pts[ii, jj + 1].X, pts[ii, jj + 1].Y));
                        plg.Points.Add(new Point(pts[ii + 1, jj + 1].X, pts[ii + 1, jj + 1].Y));
                        plg.Points.Add(new Point(pts[ii + 1, jj].X, pts[ii + 1, jj].Y));

                        plg.StrokeThickness = ds.LineThickness;
                        plg.Stroke = ds.LineColor;                       
                        plg.Fill = GetBrush(pts1[ii,jj].Z, zmin, zmax);
                        cs.ChartCanvas.Children.Add(plg);
                    }
                }
                if (cs.IsColorBar && IsColormap)
                {
                    AddColorBar(cs, ds, zmin, zmax);
                }
            }
            else if (IsInterp)
            {
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    int ii = i;
                    if (cs.Elevation >= 0)
                    {
                        ii = i;
                        if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                        {
                            ii = pts.GetLength(0) - 2 - i;
                        }
                    }
                    else
                    {
                        ii = pts.GetLength(0) - 2 - i;
                        if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                        {
                            ii = i;
                        }
                    }
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        int jj = j;
                        if (cs.Elevation < 0)
                        {
                            jj = pts.GetLength(1) - 2 - j;
                        }

                        Point3D[] points = new Point3D[4];
                        points[0] = pts1[ii, j];
                        points[1] = pts1[ii, j + 1];
                        points[2] = pts1[ii + 1, j + 1];
                        points[3] = pts1[ii + 1, j];
                        Interp(cs, m, points, zmin, zmax);
                        plg = new Polygon();
                        plg.Stroke = ds.LineColor;
                        plg.Points.Add(new Point(pts[ii, j].X, pts[ii, j].Y));
                        plg.Points.Add(new Point(pts[ii, j+1].X, pts[ii, j+1].Y));
                        plg.Points.Add(new Point(pts[ii+1, j+1].X, pts[ii+1, j+1].Y));
                        plg.Points.Add(new Point(pts[ii+1, j].X, pts[ii+1, j].Y));
                        cs.ChartCanvas.Children.Add(plg);
                    }
                }
            }
            if (cs.IsColorBar && IsColormap)
            {
                AddColorBar(cs, ds, zmin, zmax);
            }
        }

        public void Interp(ChartStyle cs, Matrix3D m, Point3D[] pta, double zmin, double zmax)
        {
            Polygon plg = new Polygon();
            Point[] points = new Point[4];
            int npoints = NumberInterp;
            Point3D[,] pts = new Point3D[npoints + 1, npoints + 1];
            Point3D[,] pts1 = new Point3D[npoints + 1, npoints + 1];
            double x0 = pta[0].X;
            double y0 = pta[0].Y;
            double x1 = pta[2].X;
            double y1 = pta[2].Y;
            double dx = (x1 - x0) / npoints;
            double dy = (y1 - y0) / npoints;
            double c00 = pta[0].Z;
            double c10 = pta[3].Z;
            double c11 = pta[2].Z;
            double c01 = pta[1].Z;
            double x, y, c;

            for (int i = 0; i <= npoints; i++)
            {
                x = x0 + i * dx;
                for (int j = 0; j <= npoints; j++)
                {
                    y = y0 + j * dy;
                    c = (y1 - y) * ((x1 - x) * c00 + (x - x0) * c10) / (x1 - x0) / (y1 - y0) + 
                        (y - y0) * ((x1 - x) * c01 + (x - x0) * c11) / (x1 - x0) / (y1 - y0);
                    pts[i, j] = new Point3D(x, y, c);
                    pts1[i, j] = new Point3D(x, y, c);
                    pts[i, j] = cs.Normalize3D(m, pts[i, j]);
                }
            }

            for (int i = 0; i < npoints; i++)
            {
                for (int j = 0; j < npoints; j++)
                {
                    plg = new Polygon();
                    Brush brush = GetBrush(pts1[i, j].Z, zmin, zmax);
                    plg.Fill = brush;
                    plg.StrokeThickness = 0.1;
                    plg.Stroke = brush;
                    plg.Points.Add(new Point(pts[i, j].X, pts[i, j].Y));
                    plg.Points.Add(new Point(pts[i + 1, j].X, pts[i + 1, j].Y));
                    plg.Points.Add(new Point(pts[i + 1, j + 1].X, pts[i + 1, j + 1].Y));
                    plg.Points.Add(new Point(pts[i, j + 1].X, pts[i, j + 1].Y));
                    cs.ChartCanvas.Children.Add(plg);
                }
            }
        }
    }
}
