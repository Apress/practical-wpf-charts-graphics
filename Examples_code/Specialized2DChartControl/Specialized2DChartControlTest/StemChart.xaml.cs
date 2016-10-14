using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for StemChart.xaml
    /// </summary>
    public partial class StemChart : Window
    {
        public StemChart()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myStemChart.Height = rootGrid.Height;
            myStemChart.Width = rootGrid.Width;
            AddData();
        }

        private void AddData()
        {
            myStemChart.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeries ds = new Specialized2DCharts.DataSeries();

            myStemChart.ChartStyle.Title = "Stem Chart";
            myStemChart.ChartStyle.Xmin = 0;
            myStemChart.ChartStyle.Xmax = 8;
            myStemChart.ChartStyle.Ymin = -1.5;
            myStemChart.ChartStyle.Ymax = 1.5;
            myStemChart.ChartStyle.XTick = 1;
            myStemChart.ChartStyle.YTick = 0.5;
            myStemChart.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myStemChart.ChartStyle.GridlineColor = Brushes.Black;

            // Draw the stair step chart:
            for (int i = 0; i < 50; i++)
            {
                ds.LineSeries.Points.Add(new Point(0.4 * i, Math.Sin(0.4 * i)));
            }

            ds.LineColor = Brushes.Red;
            ds.Symbols.SymbolType = Specialized2DCharts.Symbols.SymbolTypeEnum.Diamond;
            ds.Symbols.FillColor = Brushes.Yellow;
            ds.Symbols.BorderColor = Brushes.DarkGreen;
            myStemChart.DataCollection.DataList.Add(ds);

        }        
    }
}
