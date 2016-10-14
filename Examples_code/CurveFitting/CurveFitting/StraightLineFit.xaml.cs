using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CurveFitting
{
    /// <summary>
    /// Interaction logic for StraightLineFit.xaml
    /// </summary>
    public partial class StraightLineFit : Window
    {
        public StraightLineFit()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myChart.Height = rootGrid.ActualHeight;
            myChart.Width = rootGrid.ActualWidth;
            AddData();
        }

        private void AddData()
        {
            double[] x0 = new double[] { 0, 1, 2, 3, 4, 5 };
            double[] y0 = new double[] { 1.9, 2.7, 3.3, 4.4, 5.5, 6.5 };
            double[] results = CurveFittingAlgorithms.StraightLineFit(x0, y0);

            myChart.DataCollection.DataList.Clear();
            LineCharts.DataSeries ds;
             
            // Plot original data
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Original";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.OpenTriangle;
            ds.Symbols.BorderColor = Brushes.DarkBlue;
            for (int i = 0; i < x0.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x0[i], y0[i]));
            }
            myChart.DataCollection.DataList.Add(ds);

            // Curve fitting data:
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.DarkGreen;
            ds.SeriesName = "Curve Fitting";
            for (int i = 0; i < 101; i++)
            {
                double x = i / 20.0;
                double y = results[0] + results[1] * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            myChart.DataCollection.DataList.Add(ds);

            myChart.IsLegend = true;
            myChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthWest;

            //MessageBox.Show(results[0].ToString() + ", " +
            //    results[1].ToString() + ", " +
            //    results[2].ToString() + ", ");
        }
    }
}
