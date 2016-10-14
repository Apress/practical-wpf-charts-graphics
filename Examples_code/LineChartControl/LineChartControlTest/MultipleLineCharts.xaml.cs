using System;
using System.Windows;
using System.Windows.Media;

namespace LineChartControlTest
{
    /// <summary>
    /// Interaction logic for MultipleLineCharts.xaml
    /// </summary>
    public partial class MultipleLineCharts : Window
    {
        public MultipleLineCharts()
        {
            InitializeComponent();
        }

        private void AddData(LineChartControl.LineChartControlLib myLineChart)
        {
            LineCharts.DataSeries ds = new LineCharts.DataSeries();
            myLineChart.DataCollection.DataList.Clear();

            // Draw Sine curve:
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 1;
            ds.SeriesName = "Sine";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Circle;
            ds.Symbols.BorderColor = ds.LineColor;
            ds.Symbols.SymbolSize = 6;
            for (int i = 0; i < 15; i++)
            {
                double x = i / 2.0;
                double y = Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myLineChart.DataCollection.DataList.Add(ds);

            // Draw cosine curve:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Red;
            ds.SeriesName = "Cosine";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.OpenDiamond;
            ds.Symbols.BorderColor = ds.LineColor;
            ds.Symbols.SymbolSize = 6;
            for (int i = 0; i < 15; i++)
            {
                double x = i / 2.0;
                double y = Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myLineChart.DataCollection.DataList.Add(ds);

            myLineChart.IsLegend = true;
            myLineChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthEast;

        }

        private void grid1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myLineChart1.Width = grid1.ActualWidth;
            myLineChart1.Height = grid1.ActualHeight;
            AddData(myLineChart1);
        }
        private void grid2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myLineChart2.Width = grid2.ActualWidth;
            myLineChart2.Height = grid2.ActualHeight;
            AddData(myLineChart2);
        }

        private void grid3_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myLineChart3.Width = grid3.ActualWidth;
            myLineChart3.Height = grid3.ActualHeight;
            AddData(myLineChart3);
        }
        private void grid4_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myLineChart4.Width = grid4.ActualWidth;
            myLineChart4.Height = grid4.ActualHeight;
            AddData(myLineChart4);
        }
    }
}
