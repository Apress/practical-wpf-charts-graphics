using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Specialized3DChart
{
    public class ChartStyle
    {
        #region Private fields:
        private Canvas chartCanvas;
        private double xmin = -5;
        private double xmax = 5;
        private double ymin = -3;
        private double ymax = 3;
        private double zmin = -6;
        private double zmax = 6;
        private double xtick = 1;
        private double ytick = 1;
        private double ztick = 3;
        private FontFamily tickFont = new FontFamily("Arial Narrow");
        private double tickFontSize = (double)new FontSizeConverter().ConvertFrom("8pt");
        private Brush tickColor = Brushes.Black;
        private string title = "My 3D Chart";
        private FontFamily titleFont = new FontFamily("Arial Narrow");
        private double titleFontSize = (double)new FontSizeConverter().ConvertFrom("14pt");
        private Brush titleColor = Brushes.Black;
        private string xLabel = "X Axis";
        private string yLabel = "Y Axis";
        private string zLabel = "Z Axis";
        private FontFamily labelFont = new FontFamily("Arial Narrow");
        private double labelFontSize = (double)new FontSizeConverter().ConvertFrom("10pt");
        private Brush labelColor = Brushes.Black;
        private double elevation = 30;
        private double azimuth = -37.5;
        private bool isXGrid = true;
        private bool isYGrid = true;
        private bool isZGrid = true;
        private bool isColorBar = false;
        private Line gridline = new Line();
        private Brush gridlineColor = Brushes.LightGray;
        private double gridlineThickness = 1;
        private GridlinePatternEnum gridlinePattern = GridlinePatternEnum.Dash;
        private Line axisLine = new Line();
        private Brush axisColor = Brushes.Black;
        private AxisPatternEnum axisPattern = AxisPatternEnum.Solid;
        private double axisThickness = 1;
        #endregion

        #region Public properties:
        public Canvas ChartCanvas
        {
            get { return chartCanvas; }
            set { chartCanvas = value; }
        }

        public bool IsColorBar
        {
            get { return isColorBar; }
            set { isColorBar = value; }
        }

        public Brush AxisColor
        {
            get { return axisColor; }
            set { axisColor = value; }
        }

        public AxisPatternEnum AxisPattern
        {
            get { return axisPattern; }
            set { axisPattern = value; }
        }

        public double AxisThickness
        {
            get { return axisThickness; }
            set { axisThickness = value; }
        }

        public Brush GridlineColor
        {
            get { return gridlineColor; }
            set { gridlineColor = value; }
        }

        public GridlinePatternEnum GridlinePattern
        {
            get { return gridlinePattern; }
            set { gridlinePattern = value; }
        }

        public double GridlineThickness
        {
            get { return gridlineThickness; }
            set { gridlineThickness = value; }
        }

        public FontFamily LabelFont
        {
            get { return labelFont; }
            set { labelFont = value; }
        }

        public Brush LabelColor
        {
            get { return labelColor; }
            set { labelColor = value; }
        }

        public double LabelFontSize
        {
            get { return labelFontSize; }
            set { labelFontSize = value; }
        }

        public FontFamily TitleFont
        {
            get { return titleFont; }
            set { titleFont = value; }
        }

        public Brush TitleColor
        {
            get { return titleColor; }
            set { titleColor = value; }
        }

        public double TitleFontSize
        {
            get { return titleFontSize; }
            set { titleFontSize = value; }
        }

        public FontFamily TickFont
        {
            get { return tickFont; }
            set { tickFont = value; }
        }

        public Brush TickColor
        {
            get { return tickColor; }
            set { tickColor = value; }
        }

        public double TickFontSize
        {
            get { return tickFontSize; }
            set { tickFontSize = value; }
        }

        public bool IsXGrid
        {
            get { return isXGrid; }
            set { isXGrid = value; }
        }

        public bool IsYGrid
        {
            get { return isYGrid; }
            set { isYGrid = value; }
        }

        public bool IsZGrid
        {
            get { return isZGrid; }
            set { isZGrid = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string XLabel
        {
            get { return xLabel; }
            set { xLabel = value; }
        }

        public string YLabel
        {
            get { return yLabel; }
            set { yLabel = value; }
        }

        public string ZLabel
        {
            get { return zLabel; }
            set { zLabel = value; }
        }

        public double Elevation
        {
            get { return elevation; }
            set
            {
                elevation = value; }
        }

        public double Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; }
        }

        public double Xmax
        {
            get { return xmax; }
            set { xmax = value; }
        }

        public double Xmin
        {
            get { return xmin; }
            set { xmin = value; }
        }

        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }

        public double Ymin
        {
            get { return ymin; }
            set { ymin = value; }
        }

        public double Zmax
        {
            get { return zmax; }
            set { zmax = value; }
        }

        public double Zmin
        {
            get { return zmin; }
            set { zmin = value; }
        }

        public double XTick
        {
            get { return xtick; }
            set { xtick = value; }
        }

        public double YTick
        {
            get { return ytick; }
            set { ytick = value; }
        }

        public double ZTick
        {
            get { return ztick; }
            set { ztick = value; }
        }
        #endregion

        #region Methods:
        public void AddGridlinePattern()
        {
            gridline.Stroke = GridlineColor;
            gridline.StrokeThickness = GridlineThickness;

            switch (GridlinePattern)
            {
                case GridlinePatternEnum.Dash:
                    gridline.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case GridlinePatternEnum.Dot:
                    gridline.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case GridlinePatternEnum.DashDot:
                    gridline.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
            }
        }

        public void AddAxisPattern()
        {
            axisLine.Stroke = axisColor;
            axisLine.StrokeThickness = axisThickness;

            switch (AxisPattern)
            {
                case AxisPatternEnum.Dash:
                    axisLine.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case AxisPatternEnum.Dot:
                    axisLine.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case AxisPatternEnum.DashDot:
                    axisLine.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
            }
        }

        public enum GridlinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4
        }

        public enum AxisPatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4
        }

        public void AddChartStyle()
        {
            AddTicks();
            AddGridlines();
            AddAxes();
            AddLabels();
        }

        public Point3D Normalize3D(Matrix3D m, Point3D pt)
        {
            Point3D result = new Point3D();

            // Normalize the point:
            double x1 = (pt.X - Xmin) / (Xmax - Xmin) - 0.5;
            double y1 = (pt.Y - Ymin) / (Ymax - Ymin) - 0.5;
            double z1 = (pt.Z - Zmin) / (Zmax - zmin) - 0.5;

            // Perform transformation on the point using matrix m:
            result.X = m.Transform(new Point3D(x1, y1, z1)).X;
            result.Y = m.Transform(new Point3D(x1, y1, z1)).Y;

            // Coordinate transformation from World to Device system:
            double xShift = 1.05;
            double xScale = 1;
            double yShift = 1.05;
            double yScale = 0.9;
            if (Title == "No Title")
            {
                yShift = 0.95;
                yScale = 1;
            }
            if (IsColorBar)
            {
                xShift = 0.95;
                xScale = 0.9;
            }
            result.X = (xShift + xScale * result.X) * ChartCanvas.Width / 2;
            result.Y = (yShift - yScale * result.Y) * ChartCanvas.Height / 2;
            return result;
        }

        private Point3D[] CoordinatesOfChartBox()
        {
            Point3D[] pta = new Point3D[8];
            pta[0] = new Point3D(Xmax, Ymin, Zmin);
            pta[1] = new Point3D(Xmin, Ymin, Zmin);
            pta[2] = new Point3D(Xmin, Ymax, Zmin);
            pta[3] = new Point3D(Xmin, Ymax, Zmax);
            pta[4] = new Point3D(Xmin, Ymin, Zmax);
            pta[5] = new Point3D(Xmax, Ymin, Zmax);
            pta[6] = new Point3D(Xmax, Ymax, Zmax);
            pta[7] = new Point3D(Xmax, Ymax, Zmin);

            Point3D[] pts = new Point3D[4];
            int[] npts = new int[4] { 0, 1, 2, 3 };

            if (elevation >= 0)
            {
                if (azimuth >= -180 && azimuth < -90)
                    npts = new int[4] { 1, 2, 7, 6 };
                else if (azimuth >= -90 && azimuth < 0)
                    npts = new int[4] { 0, 1, 2, 3 };
                else if (azimuth >= 0 && azimuth < 90)
                    npts = new int[4] { 7, 0, 1, 4 };
                else if (azimuth >= 90 && azimuth <= 180)
                    npts = new int[4] { 2, 7, 0, 5 };
            }
            else if (elevation < 0)
            {
                if (azimuth >= -180 && azimuth < -90)
                    npts = new int[4] { 1, 0, 7, 6 };
                else if (azimuth >= -90 && azimuth < 0)
                    npts = new int[4] { 0, 7, 2, 3 };
                else if (azimuth >= 0 && azimuth < 90)
                    npts = new int[4] { 7, 2, 1, 4 };
                else if (azimuth >= 90 && azimuth <= 180)
                    npts = new int[4] { 2, 1, 0, 5 };
            }

            for (int i = 0; i < 4; i++)
                pts[i] = pta[npts[i]];
            return pts;
        }
        #endregion

        #region Add Axes:
        private void AddAxes()
        {
            Matrix3D m = Utility.AzimuthElevation(Elevation, Azimuth);
            Point3D[] pts = CoordinatesOfChartBox();
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i] = Normalize3D(m, pts[i]);
            }
            DrawAxisLine(pts[0], pts[1]);
            DrawAxisLine(pts[1], pts[2]);
            DrawAxisLine(pts[2], pts[3]);
        }

        private void DrawAxisLine(Point3D pt1, Point3D pt2)
        {
            axisLine = new Line();
            axisLine.Stroke = AxisColor;
            axisLine.StrokeThickness = AxisThickness;
            AddAxisPattern();
            axisLine.X1 = pt1.X;
            axisLine.Y1 = pt1.Y;
            axisLine.X2 = pt2.X;
            axisLine.Y2 = pt2.Y;
            ChartCanvas.Children.Add(axisLine);
        }
        #endregion

        #region Add Ticks:
        private void AddTicks()
        {
            Matrix3D m = Utility.AzimuthElevation(Elevation, Azimuth);
            Point3D[] pta = new Point3D[2];
            Point3D[] pts = CoordinatesOfChartBox();

            // Add x ticks:
            double offset = (Ymax - Ymin) / 30.0;
            double ticklength = offset;

            for (double x = Xmin; x <= Xmax; x = x + XTick)
            {
                if (Elevation >= 0)
                {
                    if (Azimuth >= -90 && Azimuth < 90)
                        ticklength = -offset;
                }
                else if (Elevation < 0)
                {
                    if ((Azimuth >= -180 && Azimuth < -90) ||
                        Azimuth >= 90 && Azimuth <= 180)
                        ticklength = -(Ymax - Ymin) / 30;
                }
                pta[0] = new Point3D(x, pts[1].Y + ticklength, pts[1].Z);
                pta[1] = new Point3D(x, pts[1].Y, pts[1].Z);
                for (int i = 0; i < pta.Length; i++)
                {
                    pta[i] = Normalize3D(m, pta[i]);
                }
                AddTickLine(pta[0], pta[1]);
            }

            // Add y ticks:
            offset = (Xmax - Xmin) / 30.0;
            ticklength = offset;
            for (double y = Ymin; y <= Ymax; y = y + YTick)
            {
                pts = CoordinatesOfChartBox();
                if (Elevation >= 0)
                {
                    if (Azimuth >= -180 && Azimuth < 0)
                        ticklength = -offset;
                }
                else if (Elevation < 0)
                {
                    if (Azimuth >= 0 && Azimuth < 180)
                        ticklength = -offset;
                }
                pta[0] = new Point3D(pts[1].X + ticklength, y, pts[1].Z);
                pta[1] = new Point3D(pts[1].X, y, pts[1].Z);
                for (int i = 0; i < pta.Length; i++)
                {
                    pta[i] = Normalize3D(m, pta[i]);
                }
                AddTickLine(pta[0], pta[1]);
            }

            // Add z ticks:
            double xoffset = (Xmax - Xmin) / 45.0f;
            double yoffset = (Ymax - Ymin) / 20.0f;
            double xticklength = xoffset;
            double yticklength = yoffset;
            for (double z = Zmin; z <= Zmax; z = z + ZTick)
            {
                if (Elevation >= 0)
                {
                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        xticklength = 0;
                        yticklength = yoffset;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        xticklength = xoffset;
                        yticklength = 0;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        xticklength = 0;
                        yticklength = -yoffset;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        xticklength = -xoffset;
                        yticklength = 0;
                    }
                }
                else if (Elevation < 0)
                {
                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        yticklength = 0;
                        xticklength = xoffset;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        yticklength = -yoffset;
                        xticklength = 0;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        yticklength = 0;
                        xticklength = -xoffset;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        yticklength = yoffset;
                        xticklength = 0;
                    }
                }

                pta[0] = new Point3D(pts[2].X, pts[2].Y, z);
                pta[1] = new Point3D(pts[2].X + yticklength, pts[2].Y + xticklength, z);
                for (int i = 0; i < pta.Length; i++)
                {
                    pta[i] = Normalize3D(m, pta[i]);
                }
                AddTickLine(pta[0], pta[1]);
            }
        }

        private void AddTickLine(Point3D pt1, Point3D pt2)
        {
            Line line = new Line();
            line.Stroke = AxisColor;
            line.X1 = pt1.X;
            line.Y1 = pt1.Y;
            line.X2 = pt2.X;
            line.Y2 = pt2.Y;
            ChartCanvas.Children.Add(line);
        }
        #endregion

        #region Add Gridlines:
        private void AddGridlines()
        {
            Matrix3D m = Utility.AzimuthElevation(Elevation, Azimuth);
            Point3D[] pta = new Point3D[3];
            Point3D[] pts = CoordinatesOfChartBox();

            // Draw x gridlines:
            if (IsXGrid)
            {
                for (double x = Xmin; x <= Xmax; x = x + XTick)
                {
                    pts = CoordinatesOfChartBox();
                    pta[0] = new Point3D(x, pts[1].Y, pts[1].Z);
                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            pta[1] = new Point3D(x, pts[0].Y, pts[1].Z);
                            pta[2] = new Point3D(x, pts[0].Y, pts[3].Z);
                        }
                        else
                        {
                            pta[1] = new Point3D(x, pts[2].Y, pts[1].Z);
                            pta[2] = new Point3D(x, pts[2].Y, pts[3].Z);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            pta[1] = new Point3D(x, pts[2].Y, pts[1].Z);
                            pta[2] = new Point3D(x, pts[2].Y, pts[3].Z);
                        }
                        else
                        {
                            pta[1] = new Point3D(x, pts[0].Y, pts[1].Z);
                            pta[2] = new Point3D(x, pts[0].Y, pts[3].Z);
                        }
                    }
                    for (int i = 0; i < pta.Length; i++)
                    {
                        pta[i] = Normalize3D(m, pta[i]);
                    }
                    DrawGridline(pta[0], pta[1]);
                    DrawGridline(pta[1], pta[2]);
                }

                // Draw y gridlines:
                if (IsYGrid)
                {
                    for (double y = Ymin; y <= Ymax; y = y + YTick)
                    {
                        pts = CoordinatesOfChartBox();
                        pta[0] = new Point3D(pts[1].X, y, pts[1].Z);
                        if (Elevation >= 0)
                        {
                            if ((Azimuth >= -180 && Azimuth < -90) ||
                                (Azimuth >= 0 && Azimuth < 90))
                            {
                                pta[1] = new Point3D(pts[2].X, y, pts[1].Z);
                                pta[2] = new Point3D(pts[2].X, y, pts[3].Z);
                            }
                            else
                            {
                                pta[1] = new Point3D(pts[0].X, y, pts[1].Z);
                                pta[2] = new Point3D(pts[0].X, y, pts[3].Z);
                            }
                        }
                        if (elevation < 0)
                        {
                            if ((Azimuth >= -180 && Azimuth < -90) ||
                                (Azimuth >= 0 && Azimuth < 90))
                            {
                                pta[1] = new Point3D(pts[0].X, y, pts[1].Z);
                                pta[2] = new Point3D(pts[0].X, y, pts[3].Z);

                            }
                            else
                            {
                                pta[1] = new Point3D(pts[2].X, y, pts[1].Z);
                                pta[2] = new Point3D(pts[2].X, y, pts[3].Z);
                            }
                        }
                        for (int i = 0; i < pta.Length; i++)
                        {
                            pta[i] = Normalize3D(m, pta[i]);
                        }
                        DrawGridline(pta[0], pta[1]);
                        DrawGridline(pta[1], pta[2]);
                    }
                }

                // Draw Z gridlines:
                if (IsZGrid)
                {
                    for (double z = Zmin; z <= Zmax; z = z + ZTick)
                    {
                        pts = CoordinatesOfChartBox();
                        pta[0] = new Point3D(pts[2].X, pts[2].Y, z);
                        if (Elevation >= 0)
                        {
                            if ((Azimuth >= -180 && Azimuth < -90) ||
                                (Azimuth >= 0 && Azimuth < 90))
                            {
                                pta[1] = new Point3D(pts[2].X, pts[0].Y, z);
                                pta[2] = new Point3D(pts[0].X, pts[0].Y, z);
                            }
                            else
                            {
                                pta[1] = new Point3D(pts[0].X, pts[2].Y, z);
                                pta[2] = new Point3D(pts[0].X, pts[1].Y, z);
                            }
                        }
                        if (Elevation < 0)
                        {
                            if ((Azimuth >= -180 && Azimuth < -90) ||
                                (Azimuth >= 0 && Azimuth < 90))
                            {
                                pta[1] = new Point3D(pts[0].X, pts[2].Y, z);
                                pta[2] = new Point3D(pts[0].X, pts[0].Y, z);
                            }
                            else
                            {
                                pta[1] = new Point3D(pts[2].X, pts[0].Y, z);
                                pta[2] = new Point3D(pts[0].X, pts[0].Y, z);
                            }
                        }
                        for (int i = 0; i < pta.Length; i++)
                        {
                            pta[i] = Normalize3D(m, pta[i]);
                        }
                        DrawGridline(pta[0], pta[1]);
                        DrawGridline(pta[1], pta[2]);
                    }
                }
            }
        }

        private void DrawGridline(Point3D pt1, Point3D pt2)
        {
            gridline = new Line();
            gridline.Stroke = GridlineColor;
            gridline.StrokeThickness = GridlineThickness;
            AddGridlinePattern();
            gridline.X1 = pt1.X;
            gridline.Y1 = pt1.Y;
            gridline.X2 = pt2.X;
            gridline.Y2 = pt2.Y;
            ChartCanvas.Children.Add(gridline);
        }
        #endregion

        #region Add Labels:
        private void AddLabels()
        {
            Matrix3D m = Utility.AzimuthElevation(Elevation, Azimuth);
            Point3D pt = new Point3D();
            Point3D[] pts = CoordinatesOfChartBox();
            TextBlock tb = new TextBlock();
           
            // Add x tick labels:
            double offset = (Ymax - Ymin) / 20;
            double labelSpace = offset;
            for (double x = Xmin; x <= Xmax; x = x + XTick)
            {
                if (Elevation >= 0)
                {
                    if (Azimuth >= -90 && Azimuth < 90)
                        labelSpace = -offset;
                }
                else if (Elevation < 0)
                {
                    if ((Azimuth >= -180 && Azimuth < -90) ||
                        Azimuth >= 90 && Azimuth <= 180)
                        labelSpace = -offset;
                }
                pt = new Point3D(x, pts[1].Y + labelSpace, pts[1].Z);
                pt = Normalize3D(m, pt);
                tb = new TextBlock();
                tb.Text = x.ToString();
                tb.Foreground = TickColor;
                tb.FontFamily = TickFont;
                tb.FontSize = TickFontSize;
                tb.TextAlignment = TextAlignment.Center;
                ChartCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, pt.X);
                Canvas.SetTop(tb, pt.Y);
            }

            // Add y tick labels:
            offset = (Xmax - Xmin) / 20;
            labelSpace = offset;
            for (double y = Ymin; y <= Ymax; y = y + YTick)
            {
                pts = CoordinatesOfChartBox();
                if (elevation >= 0)
                {
                    if (azimuth >= -180 && azimuth < 0)
                        labelSpace = -offset;
                }
                else if (elevation < 0)
                {
                    if (azimuth >= 0 && azimuth < 180)
                        labelSpace = -offset;
                }
                pt = new Point3D(pts[1].X + labelSpace, y, pts[1].Z);
                pt = Normalize3D(m, pt);
                tb = new TextBlock();
                tb.Text = y.ToString();
                tb.Foreground = TickColor;
                tb.FontFamily = TickFont;
                tb.FontSize = TickFontSize;
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Size ytickSize = tb.DesiredSize;
                ChartCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, pt.X - ytickSize.Width / 2);
                Canvas.SetTop(tb, pt.Y);
            }

            // Add z tick labels:
            double xoffset = (Xmax - Xmin) / 30.0;
            double yoffset = (Ymax - Ymin) / 15.0;
            double xlabelSpace = xoffset;
            double ylabelSpace = yoffset;
            tb = new TextBlock();
            tb.Text = "A";
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size size = tb.DesiredSize;

            for (double z = Zmin; z <= Zmax; z = z + ZTick)
            {
                pts = CoordinatesOfChartBox();
                if (Elevation >= 0)
                {
                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        xlabelSpace = 0;
                        ylabelSpace = yoffset;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        xlabelSpace = xoffset;
                        ylabelSpace = 0;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        xlabelSpace = 0;
                        ylabelSpace = -yoffset;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        xlabelSpace = -xoffset;
                        ylabelSpace = 0;
                    }
                }
                else if (Elevation < 0)
                {
                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        ylabelSpace = 0;
                        xlabelSpace = xoffset;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        ylabelSpace = -yoffset;
                        xlabelSpace = 0;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        ylabelSpace = 0;
                        xlabelSpace = -xoffset;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        ylabelSpace = yoffset;
                        xlabelSpace = 0;
                    }
                }

                pt = new Point3D(pts[2].X + ylabelSpace, pts[2].Y + xlabelSpace, z);
                pt = Normalize3D(m, pt);
                tb = new TextBlock();
                tb.Text = z.ToString();
                tb.Foreground = TickColor;
                tb.FontFamily = TickFont;
                tb.FontSize = TickFontSize;
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Size ztickSize = tb.DesiredSize;
                ChartCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, pt.X - ztickSize.Width - 1);
                Canvas.SetTop(tb, pt.Y - ztickSize.Height / 2);
            }

            // Add Title:
            tb = new TextBlock();
            tb.Text = Title;
            tb.Foreground = TitleColor;
            tb.FontSize = TitleFontSize;
            tb.FontFamily = TitleFont;
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size titleSize = tb.DesiredSize;
            if (tb.Text != "No Title")
            {
                ChartCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, ChartCanvas.Width / 2 - titleSize.Width / 2);
                Canvas.SetTop(tb, ChartCanvas.Height / 30);
            }

            // Add x axis label:
            offset = (Ymax - Ymin) / 3;
            labelSpace = offset;
            double offset1 = (Xmax - Xmin) / 10;
            double xc = offset1;
            if (Elevation >= 0)
            {
                if (Azimuth >= -90 && Azimuth < 90)
                    labelSpace = -offset;
                if (Azimuth >= 0 && Azimuth <= 180)
                    xc = -offset1;
            }
            else if (Elevation < 0)
            {
                if ((Azimuth >= -180 && Azimuth < -90) ||
                    Azimuth >= 90 && Azimuth <= 180)
                    labelSpace = -offset;
                if (Azimuth >= -180 && Azimuth <= 0)
                    xc = -offset1;
            }
            Point3D[] pta = new Point3D[2];
            pta[0] = new Point3D(Xmin, pts[1].Y + labelSpace, pts[1].Z);
            pta[1] = new Point3D((Xmin + Xmax) / 2 - xc, pts[1].Y + labelSpace, pts[1].Z);
            pta[0] = Normalize3D(m, pta[0]);
            pta[1] = Normalize3D(m, pta[1]);
            double theta = Math.Atan((pta[1].Y - pta[0].Y) / (pta[1].X - pta[0].X));
            theta = theta * 180 / Math.PI;
            tb = new TextBlock();
            tb.Text = XLabel;
            tb.Foreground = LabelColor;
            tb.FontFamily = LabelFont;
            tb.FontSize = LabelFontSize;
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size xLabelSize = tb.DesiredSize;

            TransformGroup tg = new TransformGroup();
            RotateTransform rt = new RotateTransform(theta, 0.5, 0.5);
            TranslateTransform tt = new TranslateTransform(pta[1].X + xLabelSize.Width / 2, pta[1].Y - xLabelSize.Height / 2);
            tg.Children.Add(rt);
            tg.Children.Add(tt);
            tb.RenderTransform = tg;
            ChartCanvas.Children.Add(tb);

            // Add y axis label:
            offset = (Xmax - Xmin) / 3;
            offset1 = (Ymax - Ymin) / 5;
            labelSpace = offset;
            double yc = YTick;
            if (Elevation >= 0)
            {
                if (Azimuth >= -180 && Azimuth < 0)
                    labelSpace = -offset;
                if (Azimuth >= -90 && Azimuth <= 90)
                    yc = -offset1;
            }
            else if (Elevation < 0)
            {
                yc = -offset1;
                if (Azimuth >= 0 && Azimuth < 180)
                    labelSpace = -offset;
                if (Azimuth >= -90 && Azimuth <= 90)
                    yc = offset1;
            }
            pta[0] = new Point3D(pts[1].X + labelSpace, Ymin, pts[1].Z);
            pta[1] = new Point3D(pts[1].X + labelSpace, (Ymin + Ymax) / 2 + yc, pts[1].Z);
            pta[0] = Normalize3D(m, pta[0]);
            pta[1] = Normalize3D(m, pta[1]);

            theta = (double)Math.Atan((pta[1].Y - pta[0].Y) / (pta[1].X - pta[0].X));
            theta = theta * 180 / (double)Math.PI;
            tb = new TextBlock();
            tb.Text = YLabel;
            tb.Foreground = LabelColor;
            tb.FontFamily = LabelFont;
            tb.FontSize = LabelFontSize;
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size yLabelSize = tb.DesiredSize;

            tg = new TransformGroup();
            tt = new TranslateTransform(pta[1].X - yLabelSize.Width / 2, pta[1].Y - yLabelSize.Height / 2);
            rt = new RotateTransform(theta, 0.5, 0.5);
            tg.Children.Add(rt);
            tg.Children.Add(tt);
            tb.RenderTransform = tg;
            ChartCanvas.Children.Add(tb);

            // Add z axis labels:
            double zticklength = 10;
            labelSpace = -1.3f * offset;
            offset1 = (Zmax - Zmin) / 8;
            double zc = -offset1;
            for (double z = Zmin; z < Zmax; z = z + ZTick)
            {
                tb = new TextBlock();
                tb.Text = z.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Size size1 = tb.DesiredSize;
                if (zticklength < size1.Width)
                    zticklength = size1.Width;
            }

            double zlength = -zticklength;
            if (Elevation >= 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                {
                    zlength = -zticklength;
                    labelSpace = -1.3f * offset;
                    zc = -offset1;
                }
                else if (Azimuth >= -90 && Azimuth < 0)
                {
                    zlength = zticklength;
                    labelSpace = 2 * offset / 3;
                    zc = offset1;
                }
                else if (Azimuth >= 0 && Azimuth < 90)
                {
                    zlength = zticklength;
                    labelSpace = 2 * offset / 3;
                    zc = -offset1;
                }
                else if (Azimuth >= 90 && Azimuth <= 180)
                {
                    zlength = -zticklength;
                    labelSpace = -1.3f * offset;
                    zc = offset1;
                }
            }
            else if (Elevation < 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                {
                    zlength = -zticklength;
                    labelSpace = -1.3f * offset;
                    zc = offset1;
                }
                else if (Azimuth >= -90 && Azimuth < 0)
                {
                    zlength = zticklength;
                    labelSpace = 2 * offset / 3;
                    zc = -offset1;
                }
                else if (Azimuth >= 0 && Azimuth < 90)
                {
                    zlength = zticklength;
                    labelSpace = 2 * offset / 3;
                    zc = offset1;
                }
                else if (Azimuth >= 90 && Azimuth <= 180)
                {
                    zlength = -zticklength;
                    labelSpace = -1.3f * offset;
                    zc = -offset1;
                }
            }
            pta[0] = new Point3D(pts[2].X - labelSpace, pts[2].Y, (Zmin + Zmax) / 2 + zc);

            pta[0] = Normalize3D(m, pta[0]);
            tb = new TextBlock();
            tb.Text = ZLabel;
            tb.Foreground = LabelColor;
            tb.FontFamily = LabelFont;
            tb.FontSize = LabelFontSize;
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size zLabelSize = tb.DesiredSize;

            tg = new TransformGroup();
            tt = new TranslateTransform(pta[0].X - zlength, pta[0].Y + zLabelSize.Width / 2);
            rt = new RotateTransform(270, 0.5, 0.5);
            tg.Children.Add(rt);
            tg.Children.Add(tt);
            tb.RenderTransform = tg;
            ChartCanvas.Children.Add(tb);
        }
        #endregion
    }
}
