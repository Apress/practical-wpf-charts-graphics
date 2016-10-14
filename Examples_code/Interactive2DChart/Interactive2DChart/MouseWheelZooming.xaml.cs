using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Interactive2DChart
{
    /// <summary>
    /// Interaction logic for ChartZooming.xaml
    /// </summary>
    public partial class MouseWheelZooming : Window
    {
        private ChartStyle cs;
        private DataCollection dc;
        private DataSeries ds;
        private double xmin0 = 0;
        private double xmax0 = 7;
        private double ymin0 = -1.5;
        private double ymax0 = 1.5;
        private double xIncrement = 5;
        private double yIncrement = 0;

        public MouseWheelZooming()
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

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(xmin0, xmax0, ymin0, ymax0);
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double dx = (e.Delta > 0) ? xIncrement : -xIncrement;
            double dy = (e.Delta > 0) ? yIncrement : -yIncrement;            
            double x0 = cs.Xmin + (cs.Xmax - cs.Xmin) * dx / chartCanvas.Width;
            double x1 = cs.Xmax - (cs.Xmax - cs.Xmin) * dx / chartCanvas.Width;
            double y0 = cs.Ymin + (cs.Ymax - cs.Ymin) * dy / chartCanvas.Height;
            double y1 = cs.Ymax - (cs.Ymax - cs.Ymin) * dy / chartCanvas.Height;

            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(x0, x1, y0, y1);
        }
    }
}

