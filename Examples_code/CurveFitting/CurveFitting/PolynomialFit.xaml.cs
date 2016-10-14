using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CurveFitting
{
    /// <summary>
    /// Interaction logic for PolynomialFit.xaml
    /// </summary>
    public partial class PolynomialFit : Window
    {
        public PolynomialFit()
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
            double[] x0 = new double[] { 1, 2, 3, 4, 5 };
            double[] y0 = new double[] { 5.5, 43.1, 128, 290.7, 498.4 };

            VectorR[] results = new VectorR[3];

            for (int i = 0; i < results.Length; i++)
            {
                double sigma = 0;
                results[i] = CurveFittingAlgorithms.PolynomialFit(x0, y0, i + 1, out sigma);
            }

            // Plot results:
            myChart.DataCollection.DataList.Clear();
            LineCharts.DataSeries ds;

            // Plot original data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Original";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Triangle;
            ds.Symbols.BorderColor = Brushes.Black;
            for (int i = 0; i < x0.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x0[i], y0[i]));
            }
            myChart.DataCollection.DataList.Add(ds);

            // 1st order fitting data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkGreen;
            ds.LineThickness = 2;
            ds.SeriesName = "1st Order Fitting";
            for (int i = 0; i < 141; i++)
            {
                double x = -1.0 + i / 20.0;
                double y = results[0][0] + results[0][1] * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myChart.DataCollection.DataList.Add(ds);

            // 2nd order fitting data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Red;
            ds.LineThickness = 2;
            ds.LinePattern = LineCharts.DataSeries.LinePatternEnum.Dash;
            ds.SeriesName = "2nd Order Fitting";
            for (int i = 0; i < 141; i++)
            {
                double x = -1.0 + i / 20.0;
                double y = results[1][0] + results[1][1] * x + results[1][2] * x * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myChart.DataCollection.DataList.Add(ds);

            // 3rd order fitting data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkBlue;
            ds.LineThickness = 2;
            ds.LinePattern = LineCharts.DataSeries.LinePatternEnum.DashDot;
            ds.SeriesName = "3rd Order Fitting";
            for (int i = 0; i < 141; i++)
            {
                double x = -1.0 + i / 20.0;
                double y = results[2][0] + results[2][1] * x + results[2][2] * x * x + results[2][3] * x * x * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myChart.DataCollection.DataList.Add(ds);

            myChart.IsLegend = true;
            myChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;

        }
    }
}
