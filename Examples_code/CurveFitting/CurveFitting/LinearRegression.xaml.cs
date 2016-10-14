using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CurveFitting
{
    /// <summary>
    /// Interaction logic for LinearRegression.xaml
    /// </summary>
    public partial class LinearRegression : Window
    {
        public LinearRegression()
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
            double[] x0 = new double[] { 0, 1, 2, 3, 4, 5 };
            double[] y0 = new double[] { 2, 1, 4, 4, 3, 2 };

            // First order polynormial (m = 1):
            CurveFittingAlgorithms.ModelFunction[] f = new CurveFittingAlgorithms.ModelFunction[] { f0, f1 };
            double sigma = 0.0;
            VectorR results1 = CurveFittingAlgorithms.LinearRegression(x0, y0, f, out sigma);

            // Second order polynormial (m = 2):
            f = new CurveFittingAlgorithms.ModelFunction[] { f0, f1, f2 };
            VectorR results2 = CurveFittingAlgorithms.LinearRegression(x0, y0, f, out sigma);

            // Third order polynormial (m = 3):
            f = new CurveFittingAlgorithms.ModelFunction[] { f0, f1, f2, f3 };
            VectorR results3 = CurveFittingAlgorithms.LinearRegression(x0, y0, f, out sigma);

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
                double y = results1[0] + results1[1] * x;
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
                double y = results2[0] + results2[1] * x + results2[2] * x * x;
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
                double y = results3[0] + results3[1] * x + results3[2] * x * x + results3[3] * x * x * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myChart.DataCollection.DataList.Add(ds);

            myChart.IsLegend = true;
            myChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;

        }

        private static double f0(double x)
        {
            return 1.0;
        }

        private static double f1(double x)
        {
            return x;
        }

        private static double f2(double x)
        {
            return x * x;
        }

        private static double f3(double x)
        {
            return x * x * x;
        }
    }
}
