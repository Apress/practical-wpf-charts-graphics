using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interpolation
{
    /// <summary>
    /// Interaction logic for DividedDifferenceInterpolation.xaml
    /// </summary>
    public partial class DividedDifferenceInterpolation : Window
    {
        public DividedDifferenceInterpolation()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myChart.Width = rootGrid.ActualWidth;
            myChart.Height = rootGrid.ActualHeight;
            AddData();
        }

        private void AddData()
        {
            double[] x0 = new double[] { 1950, 1960, 1970, 1980, 1990 };
            double[] y0 = new double[] { 150.697, 179.323, 203.212, 226.505, 249.633 };
            double[] x = new double[] { 1952, 1955, 1958, 1962, 1965, 1968, 1972, 1975, 1978, 1982, 1985, 1988 };
            double[] y = InterpolationAlgorithms.NewtonDividedDifference(x0, y0, x);

            myChart.DataCollection.DataList.Clear();
            LineCharts.DataSeries ds;

            // plot interpolated data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Interpolated";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Circle;
            ds.Symbols.BorderColor = Brushes.Red;
            for (int i = 0; i < x.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x[i], y[i]));
            }
            myChart.DataCollection.DataList.Add(ds);

            // Plot original data
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkGreen;
            ds.SeriesName = "Original";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Dot;
            ds.Symbols.BorderColor = Brushes.DarkBlue;
            for (int i = 0; i < x0.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x0[i], y0[i]));
            }
            myChart.DataCollection.DataList.Add(ds);
            myChart.IsLegend = true;
            myChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;
        }
    }
}
