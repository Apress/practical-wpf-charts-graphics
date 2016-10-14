using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Interactive2DChart
{
    /// <summary>
    /// Interaction logic for ChartZooming.xaml
    /// </summary>
    public partial class RetrieveChartData : Window
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
        private List<Ellipse> circles = new List<Ellipse>();
        private List<Ellipse> labelCircles = new List<Ellipse>();
        private List<TextBlock> labelResults = new List<TextBlock>();
        private TextBlock xCoordinate = new TextBlock();

        public RetrieveChartData()
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

            // Draw Sine-like curve:
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 2;
            double dx = (cs.Xmax - cs.Xmin) / 100;

            for (double x = cs.Xmin; x <= cs.Xmax + dx; x += dx)
            {
                double y = Math.Exp(-0.3 * Math.Abs(x)) * Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);
            Ellipse circle = new Ellipse();
            circle.Width = 8;
            circle.Height = 8;
            circle.Margin = new Thickness(2);
            circle.Fill = ds.LineColor;
            labelCircles.Add(circle);
            TextBlock tb = new TextBlock();
            tb.Text = "Y0 Value";
            tb.FontSize = 10;
            tb.Margin = new Thickness(2);
            labelResults.Add(tb);
            circle = new Ellipse();
            circle.Width = 8;
            circle.Height = 8;
            circle.Fill = ds.LineColor;
            circle.Visibility = Visibility.Hidden;
            circles.Add(circle);

            // Draw Cosine-like curve:
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
            circle = new Ellipse();
            circle.Width = 8;
            circle.Height = 8;
            circle.Margin = new Thickness(2, 2, 2, 2);
            circle.Fill = ds.LineColor;
            labelCircles.Add(circle);
            tb = new TextBlock();
            tb.Text = "Y1 Value";
            tb.FontSize = 10;
            tb.Margin = new Thickness(2);
            labelResults.Add(tb);

            circle = new Ellipse();
            circle.Width = 8;
            circle.Height = 8;
            circle.Fill = ds.LineColor;
            circle.Visibility = Visibility.Hidden;
            circles.Add(circle);

            xCoordinate.Text = "X Value";
            xCoordinate.FontSize = 10;
            xCoordinate.Margin = new Thickness(2);
            resultPanel.Children.Add(xCoordinate);

            for (int i = 0; i < dc.DataList.Count; i++)
            {
                chartCanvas.Children.Add(circles[i]);
                Canvas.SetTop(circles[i], 0);
                Canvas.SetLeft(circles[i], 0);
                resultPanel.Children.Add(labelCircles[i]);
                resultPanel.Children.Add(labelResults[i]);
            }
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            tbDate.Text = DateTime.Now.ToShortDateString();
            resultPanel.Children.Clear();
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
                chartCanvas.Cursor = Cursors.Cross;
                chartCanvas.CaptureMouse();
                for (int i = 0; i < dc.DataList.Count; i++)
                {
                    double x = startPoint.X;
                    double y = GetInterpolatedYValue(dc.DataList[i], x);
                    Canvas.SetLeft(circles[i], x - circles[i].Width / 2);
                    Canvas.SetTop(circles[i], y - circles[i].Height / 2);
                }
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (chartCanvas.IsMouseCaptured)
            {
                endPoint = e.GetPosition(chartCanvas);
                if (Math.Abs(endPoint.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance &&
                    Math.Abs(endPoint.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    double x, y;
                    for (int i = 0; i < dc.DataList.Count; i++)
                    {
                        TranslateTransform tt = new TranslateTransform();
                        tt.X = endPoint.X - startPoint.X;
                        tt.Y = GetInterpolatedYValue(dc.DataList[i], endPoint.X) - GetInterpolatedYValue(dc.DataList[i], startPoint.X);
                        circles[i].RenderTransform = tt;
                        circles[i].Visibility = Visibility.Visible;
                        x = endPoint.X;
                        x = cs.Xmin + x * (cs.Xmax - cs.Xmin) / chartCanvas.Width;
                        y = GetInterpolatedYValue(dc.DataList[i], endPoint.X);
                        y = cs.Ymin + (chartCanvas.Height - y) * (cs.Ymax - cs.Ymin) / chartCanvas.Height;
                        xCoordinate.Text = Math.Round(x, 4).ToString();
                        labelResults[i].Text = Math.Round(y, 4).ToString();
                    }
                }
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            chartCanvas.ReleaseMouseCapture();
            chartCanvas.Cursor = Cursors.Arrow;

            xCoordinate.Text = "X Value";
            for (int i = 0; i < dc.DataList.Count; i++ )
            {
                circles[i].Visibility = Visibility.Hidden;
                labelResults[i].Text = "Y" + i.ToString() + " Value";
            }
        }

        private double GetInterpolatedYValue(DataSeries data, double x)
        {
            double result = double.NaN;
            for (int i = 1; i < data.LineSeries.Points.Count; i++)
            {
                double x1 = data.LineSeries.Points[i - 1].X;
                double x2 = data.LineSeries.Points[i].X;
                if (x >= x1 && x < x2)
                {
                    double y1 = data.LineSeries.Points[i - 1].Y;
                    double y2 = data.LineSeries.Points[i].Y;
                    result = y1 + (y2 - y1) * (x - x1) / (x2 - x1);
                }
            }
            return result;
        }
    }
}


