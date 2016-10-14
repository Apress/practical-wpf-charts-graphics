using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interpolation
{
    /// <summary>
    /// Interaction logic for CubicSplineInterpolation.xaml
    /// </summary>
    public partial class CubicSplineInterpolation : Window
    {
        public CubicSplineInterpolation()
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
            double[] x0 = new double[] { 1, 2, 3};
            double[] y0 = new double[] { 1, 5, 4};
            double[] x = new double[199];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = 1.01 + i / 100.0;
            }
            double[] y = InterpolationAlgorithms.Spline(x0, y0, x);

            myChart.DataCollection.DataList.Clear();
            LineCharts.DataSeries ds;

            // Plot original data
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Original";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Dot;
            ds.Symbols.BorderColor = Brushes.DarkBlue;
            for (int i = 0; i < x0.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x0[i], y0[i]));
            }
            myChart.DataCollection.DataList.Add(ds);

            // plot interpolated data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkGreen;
            ds.SeriesName = "Interpolated";
            for (int i = 0; i < x.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x[i], y[i]));
            }
            myChart.DataCollection.DataList.Add(ds);

            myChart.IsLegend = true;
            myChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;
        }
    }
}
