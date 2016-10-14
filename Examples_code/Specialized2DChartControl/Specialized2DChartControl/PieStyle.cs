using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Specialized2DCharts
{
    public class PieStyle
    {
        private List<double> dataList = new List<double>();
        private List<string> labelList = new List<string>();
        private List<int> explodeList = new List<int>();
        private ColormapBrush colormapBrushes = new ColormapBrush();
        private Brush borderColor = Brushes.Black;
        private double borderThickness = 1.0;
 
        public List<double> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        public List<string> LabelList
        {
            get { return labelList; }
            set { labelList = value; }
        }

        public List<int> ExplodeList
        {
            get { return explodeList; }
            set { explodeList = value; }
        }

        public ColormapBrush ColormapBrushes
        {
            get { return colormapBrushes; }
            set { colormapBrushes = value; }
        }

        public Brush BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public double BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }

        public void AddPie(Canvas canvas)
        {
            int nData = DataList.Count;
            colormapBrushes.Ydivisions = nData;
            if (ExplodeList.Count == 0)
            {
                for (int i = 0; i < nData; i++)
                    ExplodeList.Add(0);
            }

            double sum = 0.0;
            for (int i = 0; i < nData; i++)
            {
                sum += DataList[i];
            }
            double startAngle = 0;
            double sweepAngle = 0;

            for (int i = 0; i < nData; i++)
            {
                Brush brush = ColormapBrushes.ColormapBrushes()[i];
                int explode = ExplodeList[i];

                if (sum < 1)
                {
                    startAngle += sweepAngle;
                    sweepAngle = 2 * Math.PI * DataList[i];
                }

                else if (sum >= 1)
                {
                    startAngle += sweepAngle;
                    sweepAngle = 2 * Math.PI * DataList[i] / sum;
                }
                double dx = explode * Math.Cos(startAngle + sweepAngle / 2);
                double dy = explode * Math.Sin(startAngle + sweepAngle / 2);
                DrawArc(canvas, brush, startAngle, startAngle + sweepAngle, dx, dy);
            }         
        }

        private void DrawArc(Canvas canvas, Brush fillColor, double startAngle, double endAngle, double dx, double dy)
        {
            Path path = new Path();
            path.Stroke = BorderColor;
            path.StrokeThickness = BorderThickness;
            path.Fill = fillColor;
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            LineSegment ls1 = new LineSegment();
            LineSegment ls2 = new LineSegment();
            ArcSegment arc = new ArcSegment();
            double xc = canvas.Width / 2 + dx;
            double yc = canvas.Height / 2 + dy;
            double r = 0.8 * xc;

            pf.IsClosed = true;
            pf.StartPoint = new Point(xc, yc);
            pf.Segments.Add(ls1);
            pf.Segments.Add(arc);
            pf.Segments.Add(ls2);
            pg.Figures.Add(pf);
            path.Data = pg;

            ls1.Point = new Point( xc + r * Math.Cos(startAngle), yc + r * Math.Sin(startAngle));
            arc.SweepDirection = SweepDirection.Clockwise;
            arc.Point = new Point(xc + r * Math.Cos(endAngle), yc + r * Math.Sin(endAngle));
            arc.Size = new Size(r, r);
            ls2.Point = new Point(xc + r * Math.Cos(endAngle), yc + r * Math.Sin(endAngle));
            canvas.Children.Add(path);
        }
    }
}
