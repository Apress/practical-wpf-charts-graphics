using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CurveFitting
{
    /// <summary>
    /// Interaction logic for WeightedLinearRegression.xaml
    /// </summary>
    public partial class WeightedLinearRegression : Window
    {
        public WeightedLinearRegression()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            logChart.Width = rootGrid.ActualWidth / 2;
            logChart.Height = rootGrid.ActualHeight;
            linearChart.Width = rootGrid.ActualWidth / 2;
            linearChart.Height = rootGrid.ActualHeight;
            AddData();
        }

        private void AddData()
        {
            double[] x0 = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double[] y0 = new double[] { 1.9398, 2.9836, 5.9890, 10.2, 20.7414, 23.232, 69.5855, 82.5836, 98.1779, 339.3256 };
            double[] ylog = new double[] { 0.6626, 1.0931, 1.7899, 2.3224, 3.0321, 3.1455, 4.2426, 4.4138, 4.5868, 5.8270 };

            double[] results = CurveFittingAlgorithms.WeightedLinearRegression(x0, ylog, y0);

            // Plot linear scale results:
            LinearScale(x0, y0, results);

            // Plot log scale results:
            LogScale(x0, ylog, results);
        }

        private void LinearScale(double[] x0, double[] y0, double[] results)
        {
            linearChart.DataCollection.DataList.Clear();
            LineCharts.DataSeries ds;

            linearChart.ChartStyle.Ymin = -50;
            linearChart.ChartStyle.Ymax = 350;
            linearChart.ChartStyle.YTick = 50;
            linearChart.ChartStyle.YLabel = "Y";

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
            linearChart.DataCollection.DataList.Add(ds);

            // Plot curve fittin data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkGreen;
            ds.LineThickness = 2;
            ds.SeriesName = "Curve Fitting";
            for (int i = 0; i < 111; i++)
            {
                double x = 0.1 + i / 10.0;
                double y = Math.Exp(results[0] + results[1] * x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            linearChart.DataCollection.DataList.Add(ds);

            linearChart.Legend.IsLegend = true;
            linearChart.Legend.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;
        }

        private void LogScale(double[] x0, double[] ylog, double[] results)
        {
            logChart.DataCollection.DataList.Clear();
            LineCharts.DataSeries ds;

            logChart.ChartStyle.Ymin = 0;
            logChart.ChartStyle.Ymax = 7;
            logChart.ChartStyle.YTick = 1;
            logChart.ChartStyle.YLabel = "Log(y)";

            // Plot original data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Original";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Triangle;
            ds.Symbols.BorderColor = Brushes.Black;
            for (int i = 0; i < x0.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x0[i], ylog[i]));
            }
            logChart.DataCollection.DataList.Add(ds);

            // Plot curve fittin data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkGreen;
            ds.LineThickness = 2;
            ds.SeriesName = "Curve Fitting";
            for (int i = 0; i < 111; i++)
            {
                double x = 0.1 + i / 10.0;
                double y = results[0] + results[1] * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            logChart.DataCollection.DataList.Add(ds);
            logChart.Legend.IsLegend = true;
            logChart.Legend.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;
        }
    }
}
