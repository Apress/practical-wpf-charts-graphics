using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Specialized3DChart
{
    public class ChartStyle2D : ChartStyle
    {
        private double leftOffset = 20;
        private double bottomOffset = 40;
        private double rightOffset = 10;
        private Canvas chart2dCanvas = new Canvas();
        Border chart2dBorder;
        double colorbarWidth;
        Line gridline = new Line();

        public Canvas Chart2dCanvas
        {
            get { return chart2dCanvas; }
        }

        public ChartStyle2D()
        {
            chart2dBorder = new Border();
            chart2dBorder.BorderBrush = Brushes.Black;
            chart2dBorder.BorderThickness = new Thickness(1);
            chart2dBorder.Child = chart2dCanvas;
        }

        public void AddChartStyle2D(DrawSurfaceChart dsc)
        {
            colorbarWidth = ChartCanvas.Width / 7;
            ChartCanvas.Children.Clear();
            ChartCanvas.Children.Add(chart2dBorder);
            Point pt = new Point();
            Line tick = new Line();
            double offset = 0;
            double dx, dy;
            TextBlock tb = new TextBlock();

            //  determine right offset:
            tb.Text = Xmax.ToString();
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size size = tb.DesiredSize;
            rightOffset = size.Width / 2 + 2;

            // Determine left offset:
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.TextAlignment = TextAlignment.Right;
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (offset < size.Width)
                    offset = size.Width;
            }
            leftOffset = offset + 5 + 30;

            Canvas.SetLeft(chart2dBorder, leftOffset);
            Canvas.SetBottom(chart2dBorder, bottomOffset);
            if (!IsColorBar)
                colorbarWidth = 0;
            chart2dCanvas.Width = ChartCanvas.Width - leftOffset - rightOffset - colorbarWidth;
            chart2dCanvas.Height = ChartCanvas.Height - bottomOffset - size.Height / 2;
         
            // Create vertical gridlines:
            if (IsYGrid == true)
            {
                for (dx = Xmin + XTick; dx < Xmax; dx += XTick)
                {
                    
                    gridline = new Line();
                    gridline.Stroke = GridlineColor;
                    gridline.StrokeThickness = GridlineThickness;
                    gridline.X1 = NormalizePoint(new Point(dx, Ymin)).X;
                    gridline.Y1 = NormalizePoint(new Point(dx, Ymin)).Y;
                    gridline.X2 = NormalizePoint(new Point(dx, Ymax)).X;
                    gridline.Y2 = NormalizePoint(new Point(dx, Ymax)).Y;
                    chart2dCanvas.Children.Add(gridline);
                }
            }

            // Create horizontal gridlines:
            if (IsXGrid == true)
            {
                for (dy = Ymin + YTick; dy < Ymax; dy += YTick)
                {
                    gridline = new Line();
                    gridline.Stroke = GridlineColor;
                    gridline.StrokeThickness = GridlineThickness;
                    gridline.X1 = NormalizePoint(new Point(Xmin, dy)).X;
                    gridline.Y1 = NormalizePoint(new Point(Xmin, dy)).Y;
                    gridline.X2 = NormalizePoint(new Point(Xmax, dy)).X;
                    gridline.Y2 = NormalizePoint(new Point(Xmax, dy)).Y;
                    chart2dCanvas.Children.Add(gridline);
                }
            }

            // Create x-axis tick marks:
            for (dx = Xmin; dx <= Xmax; dx += XTick)
            {
                pt = NormalizePoint(new Point(dx, Ymin));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X;
                tick.Y2 = pt.Y - 5;
                chart2dCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dx.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                ChartCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, leftOffset + pt.X - size.Width / 2);
                Canvas.SetTop(tb, pt.Y + 2 + size.Height / 2);
            }

            // Create y-axis tick marks:
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X + 5;
                tick.Y2 = pt.Y;
                chart2dCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                ChartCanvas.Children.Add(tb);
                Canvas.SetRight(tb, chart2dCanvas.Width + 10 + colorbarWidth);
                Canvas.SetTop(tb, pt.Y);
            }

            tb = new TextBlock();
            tb.Text = XLabel;
            tb.FontFamily = LabelFont;
            tb.FontSize = LabelFontSize;
            tb.Foreground = LabelColor;
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            size = tb.DesiredSize;
            ChartCanvas.Children.Add(tb);
            Canvas.SetBottom(tb, bottomOffset / 10);
            Canvas.SetLeft(tb, leftOffset + chart2dCanvas.Width / 2 - size.Width / 2);

            tb = new TextBlock();
            tb.Text = YLabel;
            tb.FontFamily = LabelFont;
            tb.FontSize = LabelFontSize;
            tb.Foreground = LabelColor;
            tb.RenderTransform = new RotateTransform(-90, 0.5, 0.5);

            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            size = tb.DesiredSize;
            ChartCanvas.Children.Add(tb);
            Canvas.SetBottom(tb, chart2dCanvas.Height / 2 + size.Width / 3);
            Canvas.SetLeft(tb, leftOffset / 10);
        }

        public Point NormalizePoint(Point pt)
        {
            if (chart2dCanvas.Width.ToString() == "NaN")
                chart2dCanvas.Width = 270;
            if (chart2dCanvas.Height.ToString() == "NaN")
                chart2dCanvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - Xmin) * chart2dCanvas.Width / (Xmax - Xmin);
            result.Y = chart2dCanvas.Height - (pt.Y - Ymin) * chart2dCanvas.Height / (Ymax - Ymin);
            return result;
        }


        public void AddColorBar2D(ChartStyle2D cs, DataSeriesSurface ds, Draw3DChart dsc, double zmin, double zmax)
        {
            TextBlock tb;
            tb = new TextBlock();
            tb.Text = "A";
            tb.FontFamily = cs.TickFont;
            tb.FontSize = cs.TickFontSize;
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size tickSize = tb.DesiredSize;

            double x = 8 * cs.ChartCanvas.Width / 9;
            double y = 7;
            double width = cs.ChartCanvas.Width / 25;
            double height = chart2dCanvas.Height;
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
                SolidColorBrush brush = dsc.GetBrush(pts[i].Z, zmin, zmax);
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
                dsc.AddTickLine(cs, new Point(x, yy), new Point(x + tickLength, yy));
                dsc.AddTickLine(cs, new Point(x + width, yy), new Point(x + width - tickLength, yy));
                tb = new TextBlock();
                tb.Text = (Math.Round(z, 2)).ToString();
                tb.FontFamily = cs.TickFont;
                tb.FontSize = cs.TickFontSize;
                cs.ChartCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, x + width + 5);
                Canvas.SetTop(tb, yy - tickSize.Height / 2);
            }
        }
    }
}
