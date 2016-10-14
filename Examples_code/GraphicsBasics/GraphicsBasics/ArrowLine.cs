using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicsBasics
{
    public class ArrowLine : Shape
    {
        protected PathGeometry pg;
        protected PathFigure pf;
        protected PolyLineSegment pls;

        PathFigure pfStartArrow;
        PolyLineSegment plsStartArrow;
        PathFigure pfEndArrow;
        PolyLineSegment plsEndArrow;

        public ArrowLine()
        {
            pg = new PathGeometry();
            pf = new PathFigure();
            pls = new PolyLineSegment();
            pf.Segments.Add(pls);
            pfStartArrow = new PathFigure();
            plsStartArrow = new PolyLineSegment();
            pfStartArrow.Segments.Add(plsStartArrow);
            pfEndArrow = new PathFigure();
            plsEndArrow = new PolyLineSegment();
            pfEndArrow.Segments.Add(plsEndArrow);
        }

        // Specify the X1 dependency property:
        public static readonly DependencyProperty X1Property =
            DependencyProperty.Register("X1",
            typeof(double), typeof(ArrowLine),
            new FrameworkPropertyMetadata(0.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double X1
        {
            set { SetValue(X1Property, value); }
            get { return (double)GetValue(X1Property); }
        }

        // Specify the Y1 dependency property:
        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register("Y1",
            typeof(double), typeof(ArrowLine),
            new FrameworkPropertyMetadata(0.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Y1
        {
            set { SetValue(Y1Property, value); }
            get { return (double)GetValue(Y1Property); }
        }

        // Specify the X2 dependency property:
        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register("X2",
            typeof(double), typeof(ArrowLine),
            new FrameworkPropertyMetadata(0.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double X2
        {
            set { SetValue(X2Property, value); }
            get { return (double)GetValue(X2Property); }
        }

        // Specify the Y2 dependency property:
        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register("Y2",
            typeof(double), typeof(ArrowLine),
            new FrameworkPropertyMetadata(0.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Y2
        {
            set { SetValue(Y2Property, value); }
            get { return (double)GetValue(Y2Property); }
        }

        // Specify the arrowhead size in the x direction:
        public static readonly DependencyProperty ArrowheadSizeXProperty =
            DependencyProperty.Register("ArrowheadSizeX",
            typeof(double), typeof(ArrowLine),
            new FrameworkPropertyMetadata(10.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowheadSizeX
        {
            set { SetValue(ArrowheadSizeXProperty, value); }
            get { return (double)GetValue(ArrowheadSizeXProperty); }
        }

        // Specify the arrowhead size in the y direction:
        public static readonly DependencyProperty ArrowheadSizeYProperty =
            DependencyProperty.Register("ArrowheadSizeY",
            typeof(double), typeof(ArrowLine),
            new FrameworkPropertyMetadata(10.0,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowheadSizeY
        {
            set { SetValue(ArrowheadSizeYProperty, value); }
            get { return (double)GetValue(ArrowheadSizeYProperty); }
        }

        // Specify arrowhead ends:
        public static readonly DependencyProperty ArrowheadEndProperty =
            DependencyProperty.Register("ArrowheadEnd",
            typeof(ArrowheadEndEnum), typeof(ArrowLine),
            new FrameworkPropertyMetadata(ArrowheadEndEnum.End,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public ArrowheadEndEnum ArrowheadEnd
        {
            set { SetValue(ArrowheadEndProperty, value); }
            get { return (ArrowheadEndEnum)GetValue(ArrowheadEndProperty); }
        }

        // Specify IsArrowheadClosed property
        public static readonly DependencyProperty IsArrowheadClosedProperty =
            DependencyProperty.Register("IsArrowheadClosed",
            typeof(bool), typeof(ArrowLine),
            new FrameworkPropertyMetadata(false,
            FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool IsArrowheadClosed
        {
            set { SetValue(IsArrowheadClosedProperty, value); }
            get { return (bool)GetValue(IsArrowheadClosedProperty); }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                pg.Figures.Clear();
                pf.StartPoint = new Point(X1, Y1);
                pls.Points.Clear();
                pls.Points.Add(new Point(X2, Y2));
                pg.Figures.Add(pf);

                if (pls.Points.Count > 0)
                {
                    Point pt1 = new Point();
                    Point pt2 = new Point();

                    if ((ArrowheadEnd & ArrowheadEndEnum.Start) 
                        == ArrowheadEndEnum.Start)
                    {
                        pt1 = pf.StartPoint;
                        pt2 = pls.Points[0];
                        pg.Figures.Add(CreateArrowhead(pfStartArrow, pt2, pt1));
                    }

                    if ((ArrowheadEnd & ArrowheadEndEnum.End) 
                        == ArrowheadEndEnum.End)
                    {
                        pt1 = pls.Points.Count == 1 ? pf.StartPoint :
                                                 pls.Points[pls.Points.Count - 2];
                        pt2 = pls.Points[pls.Points.Count - 1];
                        pg.Figures.Add(CreateArrowhead(pfEndArrow, pt1, pt2));
                    }
                }
                return pg;
            }
        }

        PathFigure CreateArrowhead(PathFigure pathFigure, Point pt1, Point pt2)
        {
            Point pt = new Point();
            Vector v = new Vector();

            Matrix m = ArrowheadTransform(pt1, pt2);
            PolyLineSegment pls1 = pathFigure.Segments[0] as PolyLineSegment;


            pls1.Points.Clear();
            if (!IsArrowheadClosed)
            {
                v = new Point(0, 0) - new Point(ArrowheadSizeX / 2, ArrowheadSizeY);
                pt = pt2 + v * m;
                pathFigure.StartPoint = pt;
                pls1.Points.Add(pt2);
                v = new Point(0, 0) - new Point(-ArrowheadSizeX / 2, ArrowheadSizeY);
                pt = pt2 + v * m;
                pls1.Points.Add(pt);
            }
            else if (IsArrowheadClosed)
            {
                v = new Point(0, 0) - new Point(ArrowheadSizeX / 2, 0);
                pt = pt2 + v * m;
                pathFigure.StartPoint = pt;
                v = new Point(0, 0) - new Point(0, -ArrowheadSizeY);
                pt = pt2 + v * m;
                pls1.Points.Add(pt);
                v = new Point(0, 0) - new Point(-ArrowheadSizeX / 2, 0);
                pt = pt2 + v * m;
                pls1.Points.Add(pt);
            }
            pathFigure.IsClosed = IsArrowheadClosed;
            return pathFigure;
        }

        private Matrix ArrowheadTransform(Point pt1, Point pt2)
        {
            Matrix m = new Matrix();
            double theta = 180 * (Math.Atan((pt2.X - pt1.X) /
                (pt2.Y - pt1.Y))) / Math.PI;
            double dx = pt2.X - pt1.X;
            double dy = pt2.Y - pt1.Y;

            if (dx >= 0 && dy >= 0)
                theta = -theta;
            else if (dx < 0 && dy >= 0)
                theta = -theta;
            else if (dx < 0 && dy < 0)
                theta = 180 - theta;
            else if (dx >= 0 && dy < 0)
                theta = 180 - theta;
            m.Rotate(theta);
            return m;
        }
    }

    public enum ArrowheadEndEnum
    {
        None = 0,
        Start = 1,
        End = 2,
        Both = 3
    }
}
