using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BarCharts : Window
    {
        public BarCharts()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myBarChart.Height = rootGrid.ActualHeight;
            myBarChart.Width = rootGrid.ActualWidth;
            //AddVerticalBarData();
            //AddVerticalGroupBarData();
            //AddHorizontalBarChart();
            AddHorizontalGroupBarChart();
        }

        private void AddVerticalBarData()
        {
            myBarChart.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeriesBar ds = new Specialized2DCharts.DataSeriesBar();
            myBarChart.DataCollection.BarType = Specialized2DCharts.DataCollectionBar.BarTypeEnum.Vertical;
            myBarChart.ChartStyle.Title = myBarChart.DataCollection.BarType.ToString();
            myBarChart.ChartStyle.Xmin = 0;
            myBarChart.ChartStyle.Xmax = 5;
            myBarChart.ChartStyle.Ymin = 0;
            myBarChart.ChartStyle.Ymax = 10;
            myBarChart.ChartStyle.XTick = 1;
            myBarChart.ChartStyle.YTick = 2;
            myBarChart.ChartStyle.GridlinePattern= Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myBarChart.ChartStyle.GridlineColor = Brushes.Black;

            // Draw the bar chart:
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.6;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myBarChart.DataCollection.DataList.Add(ds);
            myBarChart.DataCollection.AddBars(myBarChart.ChartStyle);
        }

        private void AddVerticalGroupBarData()
        {
            myBarChart.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeriesBar ds = new Specialized2DCharts.DataSeriesBar();
            myBarChart.DataCollection.BarType = Specialized2DCharts.DataCollectionBar.BarTypeEnum.Vertical;
            myBarChart.ChartStyle.Title = myBarChart.DataCollection.BarType.ToString();
            myBarChart.ChartStyle.Xmin = 0;
            myBarChart.ChartStyle.Xmax = 5;
            myBarChart.ChartStyle.Ymin = 0;
            myBarChart.ChartStyle.Ymax = 10;
            myBarChart.ChartStyle.XTick = 1;
            myBarChart.ChartStyle.YTick = 2;
            myBarChart.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myBarChart.ChartStyle.GridlineColor = Brushes.Black;           

            // Add the first bar series:
            ds = new Specialized2DCharts.DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.9;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myBarChart.DataCollection.DataList.Add(ds);

            // Add the second bar series:
            ds = new Specialized2DCharts.DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Yellow;
            ds.BarWidth = 0.9;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.5 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myBarChart.DataCollection.DataList.Add(ds);

            // Add the third bar series:
            ds = new Specialized2DCharts.DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Blue;
            ds.BarWidth = 0.9;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.0 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myBarChart.DataCollection.DataList.Add(ds);
            myBarChart.DataCollection.AddBars(myBarChart.ChartStyle);
        }

        private void AddHorizontalBarChart()
        {
            myBarChart.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeriesBar ds = new Specialized2DCharts.DataSeriesBar();
            myBarChart.DataCollection.BarType = Specialized2DCharts.DataCollectionBar.BarTypeEnum.Horizontal;
            myBarChart.ChartStyle.Title = myBarChart.DataCollection.BarType.ToString();
            myBarChart.ChartStyle.Xmin = 0;
            myBarChart.ChartStyle.Xmax = 10;
            myBarChart.ChartStyle.Ymin = 0;
            myBarChart.ChartStyle.Ymax = 5;
            myBarChart.ChartStyle.XTick = 2;
            myBarChart.ChartStyle.YTick = 1;
            myBarChart.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myBarChart.ChartStyle.GridlineColor = Brushes.Black;           

            // Draw the bar chart:
            ds = new Specialized2DCharts.DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.6;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            myBarChart.DataCollection.DataList.Add(ds);
            myBarChart.DataCollection.AddBars(myBarChart.ChartStyle);
        }


        private void AddHorizontalGroupBarChart()
        {
            myBarChart.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeriesBar ds = new Specialized2DCharts.DataSeriesBar();
            myBarChart.DataCollection.BarType = Specialized2DCharts.DataCollectionBar.BarTypeEnum.HorizontalStack;
            myBarChart.ChartStyle.Title = myBarChart.DataCollection.BarType.ToString();
            myBarChart.ChartStyle.Xmin = 0;
            myBarChart.ChartStyle.Xmax = 25;
            myBarChart.ChartStyle.Ymin = 0;
            myBarChart.ChartStyle.Ymax = 5;
            myBarChart.ChartStyle.XTick = 5;
            myBarChart.ChartStyle.YTick = 1;
            myBarChart.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myBarChart.ChartStyle.GridlineColor = Brushes.Black;        

            // Add the first bar series:
            ds = new Specialized2DCharts.DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Green;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 2.0 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            myBarChart.DataCollection.DataList.Add(ds);

            // Add the second bar series:
            ds = new Specialized2DCharts.DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Yellow;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.5 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            myBarChart.DataCollection.DataList.Add(ds);

            // Add the third bar series:
            ds = new Specialized2DCharts.DataSeriesBar();
            ds.BorderColor = Brushes.Red;
            ds.FillColor = Brushes.Blue;
            ds.BarWidth = 0.8;

            for (int i = 0; i < 5; i++)
            {
                double x = i + 1.0;
                double y = 1.0 * x;
                ds.LineSeries.Points.Add(new Point(y, x));
            }
            myBarChart.DataCollection.DataList.Add(ds);
            myBarChart.DataCollection.AddBars(myBarChart.ChartStyle);
        }
    }
}
