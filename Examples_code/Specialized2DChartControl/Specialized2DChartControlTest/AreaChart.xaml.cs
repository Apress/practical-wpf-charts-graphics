using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for AreaChart.xaml
    /// </summary>
    public partial class AreaChart : Window
    {
        public AreaChart()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myAreaChart.Height = rootGrid.Height;
            myAreaChart.Width = rootGrid.ActualWidth;
            AddData();
        }

        private void AddData()
        {
            myAreaChart.DataCollection.AreaList.Clear();
            Specialized2DCharts.DataSeriesArea area = new Specialized2DCharts.DataSeriesArea();
            myAreaChart.ChartStyle.Title = "Area Chart";
            myAreaChart.ChartStyle.Xmin = 0;
            myAreaChart.ChartStyle.Xmax = 10;
            myAreaChart.ChartStyle.Ymin = 0;
            myAreaChart.ChartStyle.Ymax = 10;
            myAreaChart.ChartStyle.XTick = 2;
            myAreaChart.ChartStyle.YTick = 2;
            myAreaChart.ChartStyle.GridlinePattern= Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myAreaChart.ChartStyle.GridlineColor = Brushes.Black;

            // Add sine data:
            area = new Specialized2DCharts.DataSeriesArea();
            area.BorderColor = Brushes.Black;
            area.FillColor = Brushes.LightPink;
            for (int i = 0; i < 21; i++)
            {
                area.AreaSeries.Points.Add(new Point(0.5 * i, 2.0 + Math.Sin(0.5 * i)));
            }
            myAreaChart.DataCollection.AreaList.Add(area);

            // Add cosine data:
            area = new Specialized2DCharts.DataSeriesArea();
            area.BorderColor = Brushes.Black;
            area.FillColor = Brushes.LightBlue;
            for (int i = 0; i < 21; i++)
            {
                area.AreaSeries.Points.Add(new Point(0.5 * i, 2.0 + Math.Cos(0.5 * i)));
            }
            myAreaChart.DataCollection.AreaList.Add(area);

            // Add another sine data:
            area = new Specialized2DCharts.DataSeriesArea();
            area.BorderColor = Brushes.Black;
            area.FillColor = Brushes.LightGreen;
            for (int i = 0; i < 21; i++)
            {
                area.AreaSeries.Points.Add(new Point(0.5 * i, 3.0 + Math.Sin(0.5 * i)));
            }
            myAreaChart.DataCollection.AreaList.Add(area);
        }
    }
}
