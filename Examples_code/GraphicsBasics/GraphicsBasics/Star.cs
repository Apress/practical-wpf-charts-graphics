using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicsBasics
{
    public class Star : Shape
    {
        protected PathGeometry pg;
        PathFigure pf;
        PolyLineSegment pls;

        public Star()
        {
            pg = new PathGeometry();
            pf = new PathFigure();
            pls = new PolyLineSegment();
            pg.Figures.Add(pf);
        }

        // Specify the center of the star
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(Star),
            new FrameworkPropertyMetadata(new Point(20.0, 20.0),
            FrameworkPropertyMetadataOptions.AffectsMeasure));
        public Point Center
        {
            set { SetValue(CenterProperty, value); }
            get { return (Point)GetValue(CenterProperty); }
        }

        // Specify the size of the star:
        public static readonly DependencyProperty SizeRProperty =
            DependencyProperty.Register("SizeR", typeof(double), typeof(Star),
            new FrameworkPropertyMetadata(10.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure));
        public double SizeR
        {
            set { SetValue(SizeRProperty, value); }
            get { return (double)GetValue(SizeRProperty); }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                double r = SizeR;
                double x = Center.X;
                double y = Center.Y;
                double sn36 = Math.Sin(36.0 * Math.PI / 180.0);
                double sn72 = Math.Sin(72.0 * Math.PI / 180.0);
                double cs36 = Math.Cos(36.0 * Math.PI / 180.0);
                double cs72 = Math.Cos(72.0 * Math.PI / 180.0);
               
                pf.StartPoint = new Point(x, y - r);
                pls.Points.Add(new Point(x + r * sn36, y + r * cs36));
                pls.Points.Add(new Point(x - r * sn72, y - r * cs72));
                pls.Points.Add(new Point(x + r * sn72, y - r * cs72));
                pls.Points.Add(new Point(x - r * sn36, y + r * cs36));
                pls.Points.Add(new Point(x, y - r));
                pf.Segments.Add(pls);
                pf.IsClosed = true;
                pg.FillRule = FillRule.Nonzero;  

                return pg;
            }
        }
    }
}
