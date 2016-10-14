using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Interactive2DChart
{
    /// <summary>
    /// Interaction logic for ChartZooming.xaml
    /// </summary>
    public partial class ChartZooming : Window
    {
        private Point startPoint = new Point();
        private Point endPoint = new Point();
        private Shape rubberBand = null;
        private ChartStyle cs;
        private DataCollection dc;
        private DataSeries ds;
        private double xmin0 = 0;
        private double xmax0 = 7;
        private double ymin0 = -1.5;
        private double ymax0 = 1.5;
        
        public ChartZooming()
        {
            InitializeComponent();
            cs = new ChartStyle();
            cs.Xmin = xmin0;
            cs.Xmax = xmax0;
            cs.Ymin = ymin0;
            cs.Ymax = ymax0;
        }

        private void AddChart(double xmin, double xmax, double ymin, double ymax)
        {
            dc = new DataCollection();
            ds = new DataSeries();
            cs = new ChartStyle();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Sine and Cosine Chart";
            cs.Xmin = xmin;
            cs.Xmax = xmax;
            cs.Ymin = ymin;
            cs.Ymax = ymax;
            cs.GridlinePattern = ChartStyle.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Draw Sine curve:
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 2;
            double dx = (cs.Xmax - cs.Xmin) / 100;

            for (double x = cs.Xmin; x <= cs.Xmax + dx; x += dx)
            {
                double y = Math.Exp(-0.3 * Math.Abs(x)) * Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Draw cosine curve:
            ds = new DataSeries();
            ds.LineColor = Brushes.Red;
            ds.LinePattern = DataSeries.LinePatternEnum.DashDot;
            ds.LineThickness = 2;

            for (double x = cs.Xmin; x <= cs.Xmax + dx; x += dx)
            {
                double y = Math.Exp(-0.3 * Math.Abs(x)) * Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);
            dc.AddLines(cs);
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            tbDate.Text = DateTime.Now.ToShortDateString();
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(cs.Xmin, cs.Xmax, cs.Ymin, cs.Ymax);   
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!chartCanvas.IsMouseCaptured)
            {
                startPoint = e.GetPosition(chartCanvas);
                chartCanvas.CaptureMouse();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (chartCanvas.IsMouseCaptured)
            {
                endPoint = e.GetPosition(chartCanvas);
                if (rubberBand == null)
                {
                    rubberBand = new Rectangle();
                    rubberBand.Stroke = Brushes.Red;
                    chartCanvas.Children.Add(rubberBand);
                }
                rubberBand.Width = Math.Abs(startPoint.X - endPoint.X);
                rubberBand.Height = Math.Abs(startPoint.Y - endPoint.Y);
                double left = Math.Min(startPoint.X, endPoint.X);
                double top = Math.Min(startPoint.Y, endPoint.Y);
                Canvas.SetLeft(rubberBand, left);
                Canvas.SetTop(rubberBand, top);
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double x0 = 0;
            double x1 = 1;
            double y0 = 0;
            double y1 = 1;
            endPoint = e.GetPosition(chartCanvas);

            if (endPoint.X > startPoint.X)
            {
               x0 = cs.Xmin + (cs.Xmax - cs.Xmin) * startPoint.X / chartCanvas.Width;
               x1 = cs.Xmin + (cs.Xmax - cs.Xmin) * endPoint.X / chartCanvas.Width;
            }
            else if (endPoint.X < startPoint.X)
            {
                x1 = cs.Xmin + (cs.Xmax - cs.Xmin) * startPoint.X / chartCanvas.Width;
                x0 = cs.Xmin + (cs.Xmax - cs.Xmin) * endPoint.X / chartCanvas.Width;
            }
            
            if (endPoint.Y < startPoint.Y)
            {
                y0 = cs.Ymin + (cs.Ymax - cs.Ymin) * (chartCanvas.Height - startPoint.Y) / chartCanvas.Height;
                y1 = cs.Ymin + (cs.Ymax - cs.Ymin) * (chartCanvas.Height - endPoint.Y) / chartCanvas.Height;
            }
            else if (endPoint.Y > startPoint.Y)
            {
                y1 = cs.Ymin + (cs.Ymax - cs.Ymin) * (chartCanvas.Height - startPoint.Y) / chartCanvas.Height;
                y0 = cs.Ymin + (cs.Ymax - cs.Ymin) * (chartCanvas.Height - endPoint.Y) / chartCanvas.Height;
            }

            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(x0, x1, y0, y1);

            if (rubberBand != null)
            {
                rubberBand = null;
                chartCanvas.ReleaseMouseCapture();
            }
        }

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(xmin0, xmax0, ymin0, ymax0);   
        }
    }
}
