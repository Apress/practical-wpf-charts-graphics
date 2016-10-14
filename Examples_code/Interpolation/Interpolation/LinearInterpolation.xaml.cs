using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interpolation
{
    /// <summary>
    /// Interaction logic for LinearInterpolation.xaml
    /// </summary>
    public partial class LinearInterpolation : Window
    {
        public LinearInterpolation()
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
            double[] x0 = new double[8];
            double[] y0 = new double[8];
            double[] x = new double[70];

            for (int i = 0; i < x0.Length; i++)
            {
                x0[i] = 1.0 * i;
                y0[i] = Math.Sin(x0[i]);
            }

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i / 10.0;
            }

            double[] y = InterpolationAlgorithms.Linear(x0, y0, x);

            myChart.DataCollection.DataList.Clear();

            // plot interpolated data:
            LineCharts.DataSeries ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Interpolated";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Dot;
            ds.Symbols.SymbolSize = 3;
            ds.Symbols.BorderColor = Brushes.Red;
            for (int i = 0; i < x.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x[i], y[i]));
            }
            myChart.DataCollection.DataList.Add(ds);
            
            // Plot original data
            ds = new LineCharts.DataSeries();
            ds.LineColor = Brushes.Transparent;
            ds.SeriesName = "Original";
            ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Diamond;
            ds.Symbols.BorderColor = Brushes.DarkBlue;
            for (int i = 0; i < x0.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x0[i], y0[i]));
            }
            myChart.DataCollection.DataList.Add(ds);
            myChart.IsLegend = true;
            myChart.LegendPosition = LineCharts.Legend.LegendPositionEnum.NorthEast;
        }
    }
}
