using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for StairstepChart.xaml
    /// </summary>
    public partial class StairstepChart : Window
    {
        public StairstepChart()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myStairstepChart.Height = rootGrid.Height;
            myStairstepChart.Width = rootGrid.Width;
            AddData();
        }

        private void AddData()
        {
            myStairstepChart.DataCollection.DataList.Clear();
            myStairstepChart.ChartStyle.Title = "Stair Step Chart";
            Specialized2DCharts.DataSeriesStairstep ds = new Specialized2DCharts.DataSeriesStairstep();


            myStairstepChart.ChartStyle.Xmin = 0;
            myStairstepChart.ChartStyle.Xmax = 8;
            myStairstepChart.ChartStyle.Ymin = -1.5;
            myStairstepChart.ChartStyle.Ymax = 1.5;
            myStairstepChart.ChartStyle.XTick = 1;
            myStairstepChart.ChartStyle.YTick = 0.5;
            myStairstepChart.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myStairstepChart.ChartStyle.GridlineColor = Brushes.Black;

            // Draw the stair step chart:
            ds.StairstepLineColor = Brushes.Red;
            for (int i = 0; i < 50; i++)
            {
                ds.LineSeries.Points.Add(new Point(0.4 * i, Math.Sin(0.4 * i)));
            }

            ds.LineColor = Brushes.Black;
            ds.LinePattern = Specialized2DCharts.DataSeries.LinePatternEnum.Dash;
            ds.Symbols.SymbolType = Specialized2DCharts.Symbols.SymbolTypeEnum.Circle;
            myStairstepChart.DataCollection.DataList.Add(ds);
        }   
    }
}
