using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Interactive2DChart
{
    /// <summary>
    /// Interaction logic for ChartZooming.xaml
    /// </summary>
    public partial class ChartPanning : Window
    {
        private Point startPoint = new Point();
        private Point endPoint = new Point();
        private ChartStyle cs;
        private DataCollection dc;
        private DataSeries ds;
        private double xmin0 = 0;
        private double xmax0 = 7;
        private double ymin0 = -1.5;
        private double ymax0 = 1.5;

        public ChartPanning()
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
            cs.Title = "My 2D Chart";
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
                TranslateTransform tt = new TranslateTransform();
                tt.X = endPoint.X - startPoint.X;
                tt.Y = endPoint.Y - startPoint.Y;
                for (int i = 0; i < dc.DataList.Count; i++)
                {
                    dc.DataList[i].LineSeries.RenderTransform = tt;
                }
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double dx = 0;
            double dy = 0;
            double x0 = 0;
            double x1 = 1;
            double y0 = 0;
            double y1 = 1;
            endPoint = e.GetPosition(chartCanvas);
            dx = (cs.Xmax - cs.Xmin) * (endPoint.X - startPoint.X) / chartCanvas.Width;
            dy = (cs.Ymax - cs.Ymin) * (endPoint.Y - startPoint.Y) / chartCanvas.Height;
            x0 = cs.Xmin + dx;
            x1 = cs.Xmax + dx;
            y0 = cs.Ymin + dy;
            y1 = cs.Ymax + dy;

            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(x0, x1, y0, y1);

            chartCanvas.ReleaseMouseCapture();
            chartCanvas.Cursor = Cursors.Arrow;

        }

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(xmin0, xmax0, ymin0, ymax0);
        }
    }
}


