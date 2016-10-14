using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interpolation
{
    /// <summary>
    /// Interaction logic for BarycentricInterpolation.xaml
    /// </summary>
    public partial class BarycentricInterpolation : Window
    {
        public BarycentricInterpolation()
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
            double[] x0 = new double[15];
            double[] y0 = new double[15];
            double[] xe = new double[101];
            double[] ye = new double[101];

            double[] x = new double[31];


            for (int i = 0; i < x0.Length; i++)
            {
                x0[i] = Math.Round(-1.2 + i / 5.0, 3);
                y0[i] = Math.Round(Math.Sin(8*x0[i]) + 0.5 * x0[i] - x0[i] * x0[i], 2);
            }

            for (int i = 0; i < xe.Length; i++)
            {
                xe[i] = Math.Round(-1.0 + i / 50.0, 3);
                ye[i] = Math.Round(Math.Sin(8*xe[i]) + 0.5 * xe[i] - xe[i] * xe[i], 2);
            }

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = Math.Round(-1.0 + i / 15.0, 3);
            }

            double[] y = InterpolationAlgorithms.Lagrangian(x0, y0, x);

            myChart.DataCollection.DataList.Clear();
            LineCharts.DataSeries ds;

            // Plot exact data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkGreen;
            ds.SeriesName = "Exact";
            for (int i = 1; i < xe.Length - 1; i++)
            {
                ds.LineSeries.Points.Add(new Point(xe[i], ye[i]));
            }
            myChart.DataCollection.DataList.Add(ds);

            // plot interpolated data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Interpolated";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Circle;
            ds.Symbols.BorderColor = Brushes.Red;
            for (int i = 1; i < x.Length - 1; i++)
            {
                ds.LineSeries.Points.Add(new Point(x[i], y[i]));
            }
            myChart.DataCollection.DataList.Add(ds);

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
            myChart.IsLegend = true;
            myChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;
        }
    }
}
