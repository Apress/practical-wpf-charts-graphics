using System;
using System.Windows;
using System.Windows.Media;

namespace LineChartControlTest
{
    /// <summary>
    /// Interaction logic for XYChart.xaml
    /// </summary>
    public partial class LineChart : Window
    {
        public LineChart()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myLineChart.Width = rootGrid.ActualWidth;
            myLineChart.Height = rootGrid.ActualHeight;
            AddData();
        }

        private void AddData()
        {
            myLineChart.DataCollection.DataList.Clear();

            // Draw Sine curve:
            LineCharts.DataSeries ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 1;
            ds.SeriesName = "Sine";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Circle;
            ds.Symbols.BorderColor = ds.LineColor;
            for (int i = 0; i < 70; i++)
            {
                double x = i / 5.0;
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
            ds.LinePattern = LineCharts.DataSeries.LinePatternEnum.DashDot;
            ds.LineThickness = 2;
            for (int i = 0; i < 70; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myLineChart.DataCollection.DataList.Add(ds);

            myLineChart.IsLegend = true;
            myLineChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthEast;
        }
    }
}
