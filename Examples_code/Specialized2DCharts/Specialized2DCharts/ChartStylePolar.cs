using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class ChartStylePolar
    {
        private double angleStep = 30;
        private AngleDirectionEnum angleDirection = AngleDirectionEnum.CounterClockWise;
        private double rmin = 0;
        private double rmax = 1;
        private int nTicks = 4;
        private Brush lineColor = Brushes.Black;
        private double lineThickness = 1;
        private LinePatternEnum linePattern = LinePatternEnum.Dash;

        public Canvas ChartCanvas { get; set; }

        public LinePatternEnum LinePattern
        {
            get { return linePattern; }
            set { linePattern = value; }
        }
        
        public double LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }
        
        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        public int NTicks
        {
            get { return nTicks; }
            set { nTicks = value; }
        }
                
        public double Rmax
        {
            get { return rmax; }
            set { rmax = value; }
        }

        public double Rmin
        {
            get { return rmin; }
            set { rmin = value; }
        }

        public AngleDirectionEnum AngleDirection
        {
            get { return angleDirection; }
            set { angleDirection = value; }
        }
        
        public double AngleStep
        {
            get { return angleStep; }
            set { angleStep = value; }
        }

        public enum AngleDirectionEnum
        {
            CounterClockWise = 0,
            ClockWise = 1
        }

        public DoubleCollection SetLinePattern()
        {
            DoubleCollection collection = new DoubleCollection();
            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    collection = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    collection = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    collection = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
            }
            return collection;
        }

        public double RNormalize(double r)
        {
            double result = new double();
            if (r < Rmin || r > Rmax)
                result = double.NaN;
            double width = Math.Min(ChartCanvas.Width, ChartCanvas.Height);
            result = (r - Rmin) * width / 2 / (Rmax - Rmin);
            return result;
        }

        public enum LinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4
        }

        public void SetPolarAxes()
        {
            double xc = ChartCanvas.Width / 2;
            double yc = ChartCanvas.Height / 2;

            // Draw circles:
            double dr = RNormalize(Rmax / NTicks) - RNormalize(Rmin / NTicks);
            for (int i = 0; i < NTicks; i++)
            {
                Ellipse circle = CircleLine();
                Canvas.SetLeft(circle, xc - (i + 1) * dr);
                Canvas.SetTop(circle, yc - (i + 1) * dr);
                circle.Width = 2.0 * (i + 1) * dr;
                circle.Height = 2.0 * (i + 1) * dr;
                ChartCanvas.Children.Add(circle);
            }

            //Draw radius lines:
            for (int i = 0; i < (int)360 / AngleStep; i++)
            {
                Line line = RadiusLine();
                line.X1 = RNormalize(Rmax) * Math.Cos(i * AngleStep * Math.PI / 180) + xc;
                line.Y1 = RNormalize(Rmax) * Math.Sin(i * AngleStep * Math.PI / 180) + yc;
                line.X2 = xc;
                line.Y2 = yc;
                ChartCanvas.Children.Add(line);
            }

            // Add radius labels:
            for (int i = 1; i <= NTicks; i++)
            {
                double rlabel = Rmin + i * (Rmax - Rmin) / NTicks;
                TextBlock tb = new TextBlock();
                tb.Text = rlabel.ToString();
                Canvas.SetLeft(tb, xc + 3);
                Canvas.SetTop(tb, yc - i * dr + 2);
                ChartCanvas.Children.Add(tb);
            }

            // Add angle Labels:
            double anglelabel = 0;
            for (int i = 0; i < (int)360 / AngleStep; i++)
            {
                if (AngleDirection == AngleDirectionEnum.ClockWise)
                    anglelabel = i * AngleStep;
                else if (AngleDirection == AngleDirectionEnum.CounterClockWise)
                {
                    anglelabel = 360 - i * AngleStep;
                    if (i == 0)
                        anglelabel = 0;
                }
                TextBlock tb = new TextBlock();
                tb.Text = anglelabel.ToString();
                tb.TextAlignment = TextAlignment.Center;
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Size size = tb.DesiredSize;

                double x = (RNormalize(Rmax) + 1.5 * size.Width / 2) * Math.Cos(i * AngleStep * Math.PI / 180) + xc;
                double y = (RNormalize(Rmax) + 1.5 * size.Width / 2) * Math.Sin(i * AngleStep * Math.PI / 180) + yc;
                Canvas.SetLeft(tb, x - size.Width / 2);
                Canvas.SetTop(tb, y - 1.2 * size.Height / 2);
                ChartCanvas.Children.Add(tb);
            }
        }

        private Ellipse CircleLine()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = LineColor;
            ellipse.StrokeThickness = LineThickness;
            ellipse.StrokeDashArray = SetLinePattern();
            ellipse.Fill = Brushes.Transparent;
            return ellipse;
        }

        private Line RadiusLine()
        {
            Line line = new Line();
            line.Stroke = LineColor;
            line.StrokeThickness = LineThickness;
            line.StrokeDashArray = SetLinePattern();
            return line;
        }
    }
}
