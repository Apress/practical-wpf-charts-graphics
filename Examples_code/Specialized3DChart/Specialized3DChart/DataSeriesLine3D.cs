using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Specialized3DChart
{
    public class DataSeriesLine3D 
    {
        private Polyline lineSeries = new Polyline();
        private Brush lineColor;
        private double lineThickness = 1;
        private LinePatternEnum linePattern;
        private List<Point3D> point3DList = new List<Point3D>();

        public List<Point3D> Point3DList
        {
            get { return point3DList; }
            set { point3DList = value; }
        }

        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        public Polyline LineSeries
        {
            get { return lineSeries; }
            set { lineSeries = value; }
        }

        public double LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }

        public LinePatternEnum LinePattern
        {
            get { return linePattern; }
            set { linePattern = value; }
        }

        public void AddLinePattern()
        {
            LineSeries.Stroke = LineColor;
            LineSeries.StrokeThickness = LineThickness;

            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    LineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    LineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    LineSeries.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    LineSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        public enum LinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }

        public void AddLine3D(ChartStyle cs)
        {
            Matrix3D m = Utility.AzimuthElevation(cs.Elevation, cs.Azimuth);

            Point3D[] pts = new Point3D[Point3DList.Count];
            for (int i = 0; i < Point3DList.Count; i++)
            {
                pts[i] = cs.Normalize3D(m, Point3DList[i]);
                LineSeries.Points.Add(new Point(pts[i].X, pts[i].Y));
            }
            AddLinePattern();
            cs.ChartCanvas.Children.Add(LineSeries);
        }
    }
}
