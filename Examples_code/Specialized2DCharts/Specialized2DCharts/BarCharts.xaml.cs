using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    /// <summary>
    /// Interaction logic for BarCharts.xaml
    /// </summary>
    public partial class BarCharts : Window
    {
        private ChartStyleGridlines cs;
        private DataCollectionBar dc;
        private DataSeriesBar ds;

        public BarCharts()
        {
            InitializeComponent();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);

            //AddVerticalBarChart();
            //AddVerticalGroupBarChart();
            //AddHorizontalBarChart();
            AddHorizontalGroupBarChart();
        }

        private void AddVerticalBarChart()
        {
            cs = new ChartStyleGridlines();
            dc = new DataCollectionBar();
            ds = new DataSeriesBar();

            dc.BarType = DataCollectionBar.BarTypeEnum.Vertical;
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Bar Chart";
            cs.Xmin = 0;
            cs.Xmax = 5;
            cs.Ymin = 0;
            cs.Ymax = 10;
            cs.XTick = 1;
            cs.YTick = 2;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Draw the bar chart:
            dc.DataList.Clear();
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.6;

            /*for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }*/

            double[] x = new double[] { 1, 2, 3, 4, 5 };
            double[] y = new double[] { 2, 0, 3, 8, 10 };
            for (int i = 0; i < x.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x[i], y[i]));
            }

            dc.DataList.Add(ds);
            dc.AddBars(cs);
        }

        private void AddVerticalGroupBarChart()
        {
            cs = new ChartStyleGridlines();
            dc = new DataCollectionBar();
            ds = new DataSeriesBar();

            dc.BarType = DataCollectionBar.BarTypeEnum.VerticalStack;
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = dc.BarType.ToString();
            cs.Xmin = 0;
            cs.Xmax = 5;
            cs.Ymin = 0;
            cs.Ymax = 25;
            cs.XTick = 1;
            cs.YTick = 5;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Add the first bar series:
            dc.DataList.Clear();
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Add the second bar series:
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Yellow;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.5 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Add the third bar series:
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Blue;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.0 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);
            dc.AddBars(cs);
        }

        private void AddHorizontalBarChart()
        {
            cs = new ChartStyleGridlines();
            dc = new DataCollectionBar();
            ds = new DataSeriesBar();

            dc.BarType = DataCollectionBar.BarTypeEnum.Horizontal;
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Bar Chart";
            cs.Xmin = 0;
            cs.Xmax = 10;
            cs.Ymin = 0;
            cs.Ymax = 5;
            cs.XTick = 2;
            cs.YTick = 1;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Draw the bar chart:
            dc.DataList.Clear();
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.6;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            dc.DataList.Add(ds);
            dc.AddBars(cs);
        }


        private void AddHorizontalGroupBarChart()
        {
            cs = new ChartStyleGridlines();
            dc = new DataCollectionBar();
            ds = new DataSeriesBar();

            dc.BarType = DataCollectionBar.BarTypeEnum.HorizontalStack;
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = dc.BarType.ToString();
            cs.Xmin = 0;
            cs.Xmax = 25;
            cs.Ymin = 0;
            cs.Ymax = 5;
            cs.XTick = 5;
            cs.YTick = 1;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Add the first bar series:
            dc.DataList.Clear();
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            dc.DataList.Add(ds);

            // Add the second bar series:
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Yellow;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.5 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            dc.DataList.Add(ds);

            // Add the third bar series:
            ds = new DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Blue;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.0 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            dc.DataList.Add(ds);
            dc.AddBars(cs);
        }
    }
}
