using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Specialized3DChart
{
    public class Draw3DChart : DrawSurfaceChart
    {
        private int numberContours = 10;
        private bool isBarSingleColor = true;
        private bool isLineColorMatch = false;
        private ChartTypeEnum chartType = ChartTypeEnum.XYColor;
        private Polygon plg = new Polygon();

        public ChartTypeEnum ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        public int NumberContours
        {
            get { return numberContours; }
            set { numberContours = value; }
        }

        public bool IsBarSingleColor
        {
            get { return isBarSingleColor; }
            set { isBarSingleColor = value; }
        }

        public bool IsLineColorMatch
        {
            get { return isLineColorMatch; }
            set { isLineColorMatch = value; }
        }

        public enum ChartTypeEnum
        {
            XYColor = 1,
            Contour = 2,
            FillContour = 3,
            XYColor3D = 4,
            MeshContour3D = 5,
            SurfaceContour3D = 6,
            SurfaceFillContour3D = 7,
            BarChart3D = 8
        }

        public void AddChart(ChartStyle2D cs, DataSeriesSurface ds)
        {
            switch (ChartType)
            {
                case ChartTypeEnum.XYColor:
                    cs.AddChartStyle2D(this);
                    if (cs.IsColorBar && IsColormap)
                    {
                        cs.AddColorBar2D(cs, ds, this, ds.ZDataMin(), ds.ZDataMax());
                    }
                    AddXYColor(cs, ds);
                    break;

                case ChartTypeEnum.Contour:
                    cs.AddChartStyle2D(this);
                    if (cs.IsColorBar && IsColormap)
                    {
                        cs.AddColorBar2D(cs, ds, this, ds.ZDataMin(), ds.ZDataMax());
                    }
                    AddContour(cs, ds);
                    break;

                case ChartTypeEnum.FillContour:
                    cs.AddChartStyle2D(this);
                    if (cs.IsColorBar && IsColormap)
                    {
                        cs.AddColorBar2D(cs, ds, this, ds.ZDataMin(), ds.ZDataMax());
                    }
                    AddXYColor(cs, ds);
                    AddContour(cs, ds);
                    break;

                case ChartTypeEnum.MeshContour3D:
                    cs.AddChartStyle();
                    AddContour3D(cs, ds);
                    AddMesh(cs, ds);
                    break;

                case ChartTypeEnum.SurfaceContour3D:
                    cs.AddChartStyle();
                    AddContour3D(cs, ds);
                    AddSurface(cs, ds);
                    break;

                case ChartTypeEnum.SurfaceFillContour3D:
                    cs.AddChartStyle();
                    AddXYColor3D(cs, ds);
                    AddContour3D(cs, ds);
                    AddSurface(cs, ds);
                    break;
            }
        }

        #region AddXYColor method:
        private void AddXYColor(ChartStyle2D cs2d, DataSeriesSurface ds)
        {
            Point3D[,] pts = ds.PointArray;
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();

            // Draw surface on the XY plane:
            if (!IsInterp)
            {
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        plg = new Polygon();
                        plg.Stroke = ds.LineColor;
                        plg.StrokeThickness = ds.LineThickness;
                        plg.Fill = GetBrush(pts[i, j].Z, zmin, zmax);
                        if(IsLineColorMatch)
                            plg.Stroke = GetBrush(pts[i, j].Z, zmin, zmax);
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i, j].X, pts[i, j].Y)));
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i, j + 1].X, pts[i, j + 1].Y)));
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i + 1, j + 1].X, pts[i + 1, j + 1].Y)));
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i + 1, j].X, pts[i + 1, j].Y)));
                        cs2d.Chart2dCanvas.Children.Add(plg);
                    }
                }
            }
            else if (IsInterp)
            {
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        Point3D[] points = new Point3D[4];
                        points[0] = pts[i, j];
                        points[1] = pts[i, j + 1];
                        points[2] = pts[i + 1, j + 1];
                        points[3] = pts[i + 1, j];

                        Interp2D(cs2d, points, zmin, zmax);
                        plg = new Polygon();
                        plg.Stroke = ds.LineColor;
                        if (IsLineColorMatch)
                            plg.Stroke = GetBrush(pts[i, j].Z, zmin, zmax);
                        plg.StrokeThickness = ds.LineThickness;
                        plg.Fill = Brushes.Transparent;
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i, j].X, pts[i, j].Y)));
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i, j + 1].X, pts[i, j + 1].Y)));
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i + 1, j + 1].X, pts[i + 1, j + 1].Y)));
                        plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i + 1, j].X, pts[i + 1, j].Y)));
                        cs2d.Chart2dCanvas.Children.Add(plg);
                       
                    }
                }
            }
        }

        private void Interp2D(ChartStyle2D cs2d, Point3D[] pta, double zmin, double zmax)
        {
            Polygon plg = new Polygon();
            Point[] points = new Point[4];
            int npoints = NumberInterp;
            Point3D[,] pts = new Point3D[npoints + 1, npoints + 1];
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
                }
            }

            for (int i = 0; i < npoints; i++)
            {
                for (int j = 0; j < npoints; j++)
                {
                    plg = new Polygon();
                    Brush brush = GetBrush(pts[i, j].Z, zmin, zmax);
                    plg.Fill = brush;
                    plg.Stroke = brush;
                    plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i, j].X, pts[i, j].Y)));
                    plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i, j + 1].X, pts[i, j + 1].Y)));
                    plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i + 1, j + 1].X, pts[i + 1, j + 1].Y)));
                    plg.Points.Add(cs2d.NormalizePoint(new Point(pts[i + 1, j].X, pts[i + 1, j].Y)));
                    cs2d.Chart2dCanvas.Children.Add(plg);
                }
            }
        }
        #endregion

        #region AddContour method:
        private void AddContour(ChartStyle2D cs2d, DataSeriesSurface ds)
        {
            Point[] pta = new Point[2];
            SolidColorBrush brush = Brushes.Black;
            Line line = new Line();
            Point3D[,] pts = ds.PointArray;
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();
            double[] zlevels = new double[NumberContours];

            for (int i = 0; i < NumberContours; i++)
            {
                zlevels[i] = zmin + i * (zmax - zmin) / (NumberContours - 1);
            }

            int i0, i1, i2, j0, j1, j2;
            double zratio = 1;

            // Draw contour on the XY plane:
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    if (IsColormap && ChartType != ChartTypeEnum.FillContour)
                    {
                        brush = GetBrush(pts[i, j].Z, zmin, zmax);
                    }
                    for (int k = 0; k < NumberContours; k++)
                    {
                        // Left triangle:
                        i0 = i;
                        j0 = j;
                        i1 = i;
                        j1 = j + 1;
                        i2 = i + 1;
                        j2 = j + 1;
                        if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z || 
                            zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                            (zlevels[k] >= pts[i1, j1].Z && zlevels[k] <
                            pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = cs2d.NormalizePoint(new Point(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y));
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = cs2d.NormalizePoint(new Point((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y));
                            DrawLine(cs2d, ds, brush, pta[0], pta[1]);
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i2, j2].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k] <
                            pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[0] = cs2d.NormalizePoint(new Point((1 - zratio) * pts[i0, j0].X + zratio * pts[i2, j2].X,
                                (1 - zratio) * pts[i0, j0].Y + zratio * pts[i2, j2].Y));
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = cs2d.NormalizePoint(new Point((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y));
                            DrawLine(cs2d, ds, brush, pta[0], pta[1]);
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                         < pts[i1, j1].Z || zlevels[k] < pts[i0, j0].Z
                         && zlevels[k] >= pts[i1, j1].Z) &&
                            (zlevels[k] >= pts[i0, j0].Z && zlevels[k] <
                         pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z &&
                         zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = cs2d.NormalizePoint(new Point(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y));
                            zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[1] = cs2d.NormalizePoint(new Point(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio));
                            DrawLine(cs2d, ds, brush, pta[0], pta[1]);
                        }

                        // right triangle:
                        i0 = i;
                        j0 = j;
                        i1 = i + 1;
                        j1 = j;
                        i2 = i + 1;
                        j2 = j + 1;
                        if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] <
                            pts[i1, j1].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = cs2d.NormalizePoint(new Point(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y));
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = cs2d.NormalizePoint(new Point(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio));
                            DrawLine(cs2d, ds, brush, pta[0], pta[1]);
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i2, j2].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k] <
                            pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[0] = cs2d.NormalizePoint(new Point(pts[i0, j0].X * (1 - zratio) + 
                                pts[i2, j2].X * zratio, pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio));
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = cs2d.NormalizePoint(new Point(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio));
                            DrawLine(cs2d, ds, brush, pta[0], pta[1]);
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i1, j1].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i0, j0].Z && zlevels[k] <
                            pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = cs2d.NormalizePoint(new Point(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y));
                            zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[1] = cs2d.NormalizePoint(new Point(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio, 
                                pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio));
                            DrawLine(cs2d, ds, brush, pta[0], pta[1]);
                        }
                    }
                }
            }
        }

        private void DrawLine(ChartStyle2D cs2d, DataSeriesSurface ds, SolidColorBrush brush, Point pt0, Point pt1)
        {
            Line line = new Line();
            if (IsLineColorMatch)
                line.Stroke = brush;
            else
                line.Stroke = ds.LineColor;
            line.StrokeThickness = ds.LineThickness;
            line.X1 = pt0.X;
            line.Y1 = pt0.Y;
            line.X2 = pt1.X;
            line.Y2 = pt1.Y;
            cs2d.Chart2dCanvas.Children.Add(line);
        }
        #endregion

        #region AddXYColor3D:
        private void AddXYColor3D(ChartStyle cs, DataSeriesSurface ds)
        {
            Point3D[,] pts = ds.PointArray;
            Point3D[,] pts1 = new Point3D[pts.GetLength(0), pts.GetLength(1)];
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);
            Polygon plg = new Polygon();

            // Find the minumum and maximum z values:
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();

            // Perform transformation on points:
            for (int i = 0; i < pts.GetLength(0); i++)
            {
                for (int j = 0; j < pts.GetLength(1); j++)
                {
                    // Make a deep copy the points array:
                    pts1[i, j] = new Point3D(pts[i, j].X, pts[i, j].Y, cs.Zmin);
                    pts1[i, j] = cs.Normalize3D(m, pts1[i, j]);
                }
            }

            // Draw surface on the XY plane:
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    plg = new Polygon();
                    plg.Points.Add(new Point(pts1[i, j].X, pts1[i, j].Y));
                    plg.Points.Add(new Point(pts1[i, j + 1].X, pts1[i, j + 1].Y));
                    plg.Points.Add(new Point(pts1[i + 1, j + 1].X, pts1[i + 1, j + 1].Y));
                    plg.Points.Add(new Point(pts1[i + 1, j].X, pts1[i + 1, j].Y));
                    plg.StrokeThickness = ds.LineThickness;
                    plg.Fill = GetBrush(pts[i, j].Z, zmin, zmax);
                    plg.Stroke = GetBrush(pts[i, j].Z, zmin, zmax);
                    cs.ChartCanvas.Children.Add(plg);
                }
            }
        }
        #endregion

        #region AddContour3D method:
        private void AddContour3D(ChartStyle cs, DataSeriesSurface ds)
        {
            Point3D[] pta = new Point3D[2];
            Point3D[,] pts = ds.PointArray;
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);
            SolidColorBrush brush = Brushes.Black;

            // Find the minumum and maximum z values:
            double zmin = ds.ZDataMin();
            double zmax = ds.ZDataMax();
            double[] zlevels = new Double[NumberContours];
            for (int i = 0; i < NumberContours; i++)
            {
                zlevels[i] = zmin + i * (zmax - zmin) / (NumberContours - 1);
            }

            int i0, i1, i2, j0, j1, j2;
            double zratio = 1;

            // Draw contour on the XY plane:
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    if (IsColormap && ChartType != ChartTypeEnum.FillContour)
                    {
                        brush = GetBrush(pts[i, j].Z, zmin, zmax);
                    }
                    for (int k = 0; k < numberContours; k++)
                    {
                        // Left triangle:
                        i0 = i;
                        j0 = j;
                        i1 = i;
                        j1 = j + 1;
                        i2 = i + 1;
                        j2 = j + 1;
                        if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] <
                            pts[i1, j1].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = new Point3D(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y, cs.Zmin);
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = new Point3D((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y, cs.Zmin);
                            pta[0] = cs.Normalize3D(m, pta[0]);
                            pta[1] = cs.Normalize3D(m, pta[1]);
                            DrawLine3D(cs, ds, brush, new Point(pta[0].X, pta[0].Y), new Point(pta[1].X, pta[1].Y));
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i2, j2].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[0] = new Point3D((1 - zratio) * pts[i0, j0].X + zratio * pts[i2, j2].X, (1 - zratio) * pts[i0, j0].Y +
                                zratio * pts[i2, j2].Y, cs.Zmin);
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = new Point3D((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y, cs.Zmin);
                            pta[0] = cs.Normalize3D(m, pta[0]);
                            pta[1] = cs.Normalize3D(m, pta[1]);
                            DrawLine3D(cs, ds, brush, new Point(pta[0].X, pta[0].Y), new Point(pta[1].X, pta[1].Y));
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i1, j1].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = new Point3D(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y, cs.Zmin);
                            zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[1] = new Point3D(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio, cs.Zmin);
                            pta[0] = cs.Normalize3D(m, pta[0]);
                            pta[1] = cs.Normalize3D(m, pta[1]);
                            DrawLine3D(cs, ds, brush, new Point(pta[0].X, pta[0].Y), new Point(pta[1].X, pta[1].Y));
                        }

                        // right triangle:
                        i0 = i;
                        j0 = j;
                        i1 = i + 1;
                        j1 = j;
                        i2 = i + 1;
                        j2 = j + 1;
                        if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] <
                            pts[i1, j1].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = new Point3D(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y, cs.Zmin);
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = new Point3D(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio, cs.Zmin);
                            pta[0] = cs.Normalize3D(m, pta[0]);
                            pta[1] = cs.Normalize3D(m, pta[1]);
                            DrawLine3D(cs, ds, brush, new Point(pta[0].X, pta[0].Y), new Point(pta[1].X, pta[1].Y));
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i2, j2].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k] <
                            pts[i2, j2].Z || zlevels[k] < pts[i1, j1].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) /
                                (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[0] = new Point3D(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                           pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio, cs.Zmin);
                            zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                            pta[1] = new Point3D(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio, cs.Zmin);
                            pta[0] = cs.Normalize3D(m, pta[0]);
                            pta[1] = cs.Normalize3D(m, pta[1]);
                            DrawLine3D(cs, ds, brush, new Point(pta[0].X, pta[0].Y), new Point(pta[1].X, pta[1].Y));
                        }
                        else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k]
                            < pts[i1, j1].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i0, j0].Z && zlevels[k] <
                            pts[i2, j2].Z || zlevels[k] < pts[i0, j0].Z
                            && zlevels[k] >= pts[i2, j2].Z))
                        {
                            zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                            pta[0] = new Point3D(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y, cs.Zmin);
                            zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                            pta[1] = new Point3D(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio, cs.Zmin);
                            pta[0] = cs.Normalize3D(m, pta[0]);
                            pta[1] = cs.Normalize3D(m, pta[1]);
                            DrawLine3D(cs, ds, brush, new Point(pta[0].X, pta[0].Y), new Point(pta[1].X, pta[1].Y));
                        }
                    }
                }
            }

        }

        private void DrawLine3D(ChartStyle cs, DataSeriesSurface ds, SolidColorBrush brush, Point pt0, Point pt1)
        {
            Line line = new Line();
            line.Stroke = ds.LineColor;
            if (IsLineColorMatch)
                line.Stroke = brush;
            line.StrokeThickness = ds.LineThickness;
            line.X1 = pt0.X;
            line.Y1 = pt0.Y;
            line.X2 = pt1.X;
            line.Y2 = pt1.Y;
            cs.ChartCanvas.Children.Add(line);
        }
        #endregion

        #region AddBar3D method:
        public void AddBar3D(ChartStyle2D cs, Bar3DStyle bs)
        {
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);
            Point[] pta = new Point[4];
            Point3D[,] pts = bs.PointArray;

            // Find the minumum and maximum z values:
            double zmin = bs.ZDataMin();
            double zmax = bs.ZDataMax();

            // Check parameters:
            double xlength = bs.XLength;
            if (xlength <= 0)
                xlength = 0.1 * bs.XSpacing;
            else if (xlength > 0.5)
                xlength = 0.5 * bs.XSpacing;
            else
                xlength = bs.XLength * bs.XSpacing;
            double ylength = bs.YLength;
            if (ylength <= 0)
                ylength = 0.1 * bs.YSpacing;
            else if (ylength > 0.5)
                ylength = 0.5 * bs.YSpacing;
            else
                ylength = bs.YLength * bs.YSpacing;
            double zorigin = bs.ZOrigin;

            // Draw 3D bars:
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    int ii = i;
                    int jj = j;
                    if (cs.Azimuth >= -180 && cs.Azimuth < -90)
                    {
                        ii = pts.GetLength(0) - 2 - i;
                        jj = j;
                    }
                    else if (cs.Azimuth >= -90 && cs.Azimuth < 0)
                    {
                        ii = pts.GetLength(0) - 2 - i;
                        jj = pts.GetLength(1) - 2 - j;
                    }
                    else if (cs.Azimuth >= 0 && cs.Azimuth < 90)
                    {
                        ii = i;
                        jj = pts.GetLength(1) - 2 - j;
                    }
                    else if (cs.Azimuth >= 90 && cs.Azimuth <= 180)
                    {
                        ii = i;
                        jj = j;
                    }
                    DrawBar(cs, bs, m, pts[ii, jj], xlength, ylength, zorigin, zmax, zmin);
                }
            }
            if (cs.IsColorBar && IsColormap)
            {
                AddColorBar(cs, bs, zmin, zmax);
            }
        }

        private void DrawBar(ChartStyle2D cs, Bar3DStyle bs, Matrix3D m, Point3D pt, double xlength, double ylength, 
                             double zorign, double zmax, double zmin)
        {
            SolidColorBrush lineBrush = (SolidColorBrush)bs.LineColor;
            SolidColorBrush fillBrush = GetBrush(pt.Z, zmin, zmax);
            Point3D[] pts = new Point3D[8];
            Point3D[] pts1 = new Point3D[8];
            Point3D[] pt3 = new Point3D[4];
            Point[] pta = new Point[4];

            pts[0] = new Point3D(pt.X - xlength, pt.Y - ylength, zorign);
            pts[1] = new Point3D(pt.X - xlength, pt.Y + ylength, zorign);
            pts[2] = new Point3D(pt.X + xlength, pt.Y + ylength, zorign);
            pts[3] = new Point3D(pt.X + xlength, pt.Y - ylength, zorign);
            pts[4] = new Point3D(pt.X + xlength, pt.Y - ylength, pt.Z);
            pts[5] = new Point3D(pt.X + xlength, pt.Y + ylength, pt.Z);
            pts[6] = new Point3D(pt.X - xlength, pt.Y + ylength, pt.Z);
            pts[7] = new Point3D(pt.X - xlength, pt.Y - ylength, pt.Z);

            for (int i = 0; i < pts.Length; i++)
            {
                pts1[i] = new Point3D(pts[i].X, pts[i].Y, pts[i].Z);
                pts[i] = cs.Normalize3D(m, pts[i]);
            }

            int[] nconfigs = new int[8];
            if (IsBarSingleColor)
            {
                pta[0] = new Point(pts[4].X, pts[4].Y);
                pta[1] = new Point(pts[5].X, pts[5].Y);
                pta[2] = new Point(pts[6].X, pts[6].Y);
                pta[3] = new Point(pts[7].X, pts[7].Y);
                DrawPolygon(cs, bs, pta, fillBrush,lineBrush);

                if (cs.Azimuth >= -180 && cs.Azimuth < -90)
                {
                    nconfigs = new int[8] { 1, 2, 5, 6, 1, 0, 7, 6 };
                }
                else if (cs.Azimuth >= -90 && cs.Azimuth < 0)
                {
                    nconfigs = new int[8] { 1, 0, 7, 6, 0, 3, 4, 7 };
                }
                else if (cs.Azimuth >= 0 && cs.Azimuth < 90)
                {
                    nconfigs = new int[8] { 0, 3, 4, 7, 2, 3, 4, 5 };
                }
                else if (cs.Azimuth >= 90 && cs.Azimuth < 180)
                {
                    nconfigs = new int[8] { 2, 3, 4, 5, 1, 2, 5, 6 };
                }
                pta[0] = new Point(pts[nconfigs[0]].X, pts[nconfigs[0]].Y);
                pta[1] = new Point(pts[nconfigs[1]].X, pts[nconfigs[1]].Y);
                pta[2] = new Point(pts[nconfigs[2]].X, pts[nconfigs[2]].Y);
                pta[3] = new Point(pts[nconfigs[3]].X, pts[nconfigs[3]].Y);
                DrawPolygon(cs, bs, pta, fillBrush, lineBrush);

                pta[0] = new Point(pts[nconfigs[4]].X, pts[nconfigs[4]].Y);
                pta[1] = new Point(pts[nconfigs[5]].X, pts[nconfigs[5]].Y);
                pta[2] = new Point(pts[nconfigs[6]].X, pts[nconfigs[6]].Y);
                pta[3] = new Point(pts[nconfigs[7]].X, pts[nconfigs[7]].Y);
                DrawPolygon(cs, bs, pta, fillBrush, lineBrush);
            }
            else if (!IsBarSingleColor && IsColormap)
            {
                pta[0] = new Point(pts[4].X, pts[4].Y);
                pta[1] = new Point(pts[5].X, pts[5].Y);
                pta[2] = new Point(pts[6].X, pts[6].Y);
                pta[3] = new Point(pts[7].X, pts[7].Y);
                DrawPolygon(cs, bs, pta, fillBrush, lineBrush);

                pta[0] = new Point(pts[0].X, pts[0].Y);
                pta[1] = new Point(pts[1].X, pts[1].Y);
                pta[2] = new Point(pts[2].X, pts[2].Y);
                pta[3] = new Point(pts[3].X, pts[3].Y);
                fillBrush = GetBrush(pts1[0].Z, zmin, zmax);
                DrawPolygon(cs, bs, pta, fillBrush, lineBrush);

                double dz = (zmax - zmin) / 63;
                if (pt.Z < zorign)
                    dz = -dz;
                int nz = (int)((pt.Z - zorign) / dz) + 1;
                if (nz < 1)
                    nz = 1;
                double z = zorign;

                if (cs.Azimuth >= -180 && cs.Azimuth < -90)
                {
                    nconfigs = new int[4] { 1, 2, 1, 0 };
                }
                else if (cs.Azimuth >= -90 && cs.Azimuth < 0)
                {
                    nconfigs = new int[4] { 1, 0, 0, 3 };
                }
                else if (cs.Azimuth >= 0 && cs.Azimuth < 90)
                {
                    nconfigs = new int[4] { 0, 3, 2, 3 };
                }
                else if (cs.Azimuth >= 90 && cs.Azimuth <= 180)
                {
                    nconfigs = new int[4] { 2, 3, 1, 2 };
                }
                for (int i = 0; i < nz; i++)
                {
                    z = zorign + i * dz;
                    pt3[0] = new Point3D(pts1[nconfigs[0]].X, pts1[nconfigs[0]].Y, z);
                    pt3[1] = new Point3D(pts1[nconfigs[1]].X, pts1[nconfigs[1]].Y, z);
                    pt3[2] = new Point3D(pts1[nconfigs[1]].X, pts1[nconfigs[1]].Y, z + dz);
                    pt3[3] = new Point3D(pts1[nconfigs[0]].X, pts1[nconfigs[0]].Y, z + dz);
                    for (int j = 0; j < pt3.Length; j++)
                    {
                        pt3[j] = cs.Normalize3D(m, pt3[j]);
                    }
                    pta[0] = new Point(pt3[0].X, pt3[0].Y);
                    pta[1] = new Point(pt3[1].X, pt3[1].Y);
                    pta[2] = new Point(pt3[2].X, pt3[2].Y);
                    pta[3] = new Point(pt3[3].X, pt3[3].Y);
                    fillBrush = GetBrush(z, zmin, zmax);
                    DrawPolygon(cs, bs, pta, fillBrush, fillBrush);
                }
                pt3[0] = new Point3D(pts1[nconfigs[0]].X, pts1[nconfigs[0]].Y, zorign);
                pt3[1] = new Point3D(pts1[nconfigs[1]].X, pts1[nconfigs[1]].Y, zorign);
                pt3[2] = new Point3D(pts1[nconfigs[1]].X, pts1[nconfigs[1]].Y, pt.Z);
                pt3[3] = new Point3D(pts1[nconfigs[0]].X, pts1[nconfigs[0]].Y, pt.Z);
                for (int j = 0; j < pt3.Length; j++)
                {
                    pt3[j] = cs.Normalize3D(m, pt3[j]);
                }
                pta[0] = new Point(pt3[0].X, pt3[0].Y);
                pta[1] = new Point(pt3[1].X, pt3[1].Y);
                pta[2] = new Point(pt3[2].X, pt3[2].Y);
                pta[3] = new Point(pt3[3].X, pt3[3].Y);
                fillBrush = Brushes.Transparent;
                DrawPolygon(cs, bs, pta, fillBrush, lineBrush);

                for (int i = 0; i < nz; i++)
                {
                    z = zorign + i * dz;
                    pt3[0] = new Point3D(pts1[nconfigs[2]].X, pts1[nconfigs[2]].Y, z);
                    pt3[1] = new Point3D(pts1[nconfigs[3]].X, pts1[nconfigs[3]].Y, z);
                    pt3[2] = new Point3D(pts1[nconfigs[3]].X, pts1[nconfigs[3]].Y, z + dz);
                    pt3[3] = new Point3D(pts1[nconfigs[2]].X, pts1[nconfigs[2]].Y, z + dz);

                    for (int j = 0; j < pt3.Length; j++)
                    {
                        pt3[j] = cs.Normalize3D(m, pt3[j]);
                    }
                    pta[0] = new Point(pt3[0].X, pt3[0].Y);
                    pta[1] = new Point(pt3[1].X, pt3[1].Y);
                    pta[2] = new Point(pt3[2].X, pt3[2].Y);
                    pta[3] = new Point(pt3[3].X, pt3[3].Y);
                    fillBrush = GetBrush(z, zmin, zmax);
                    DrawPolygon(cs, bs, pta, fillBrush, fillBrush);
                }
                pt3[0] = new Point3D(pts1[nconfigs[2]].X, pts1[nconfigs[2]].Y, zorign);
                pt3[1] = new Point3D(pts1[nconfigs[3]].X, pts1[nconfigs[3]].Y, zorign);
                pt3[2] = new Point3D(pts1[nconfigs[3]].X, pts1[nconfigs[3]].Y, pt.Z);
                pt3[3] = new Point3D(pts1[nconfigs[2]].X, pts1[nconfigs[2]].Y, pt.Z);
                for (int j = 0; j < pt3.Length; j++)
                {
                    pt3[j] = cs.Normalize3D(m, pt3[j]);
                }
                pta[0] = new Point(pt3[0].X, pt3[0].Y);
                pta[1] = new Point(pt3[1].X, pt3[1].Y);
                pta[2] = new Point(pt3[2].X, pt3[2].Y);
                pta[3] = new Point(pt3[3].X, pt3[3].Y);
                fillBrush = Brushes.Transparent;
                DrawPolygon(cs, bs, pta, fillBrush, lineBrush);
            }
        }

        private void DrawPolygon(ChartStyle2D cs, Bar3DStyle bs, Point[] pts, SolidColorBrush fillBrush, SolidColorBrush lineBrush)
        {
            Polygon plg = new Polygon();
            plg.Stroke = lineBrush;
            plg.StrokeThickness = bs.LineThickness;
            plg.Fill = fillBrush;
            for (int i = 0; i < pts.Length; i++)
            {
                plg.Points.Add(pts[i]);
            }
            cs.ChartCanvas.Children.Add(plg);
        }
        #endregion
    }
}
