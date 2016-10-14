using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for PolarChart.xaml
    /// </summary>
    public partial class PolarChart : Window
    {
        public PolarChart()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myPolarChart.Height = rootGrid.ActualHeight;
            myPolarChart.Width = rootGrid.ActualWidth;
            AddData1();
        }

        private void AddData()
        {
            myPolarChart.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeries ds = new Specialized2DCharts.DataSeries();

            myPolarChart.ChartStyle.Rmax = 0.5;
            myPolarChart.ChartStyle.Rmin = 0;
            myPolarChart.ChartStyle.NTicks = 4;
            myPolarChart.ChartStyle.AngleStep = 30;
            myPolarChart.ChartStyle.AngleDirection= Specialized2DCharts.ChartStylePolar.AngleDirectionEnum.CounterClockWise;
            myPolarChart.ChartStyle.LinePattern= Specialized2DCharts.ChartStylePolar.LinePatternEnum.Dot;
            myPolarChart.ChartStyle.LineColor = Brushes.Black;

            ds.LineColor = Brushes.Red;
            for (int i = 0; i < 360; i++)
            {
                double theta = 1.0 * i;
                double r = Math.Abs(Math.Cos(2.0 * theta * Math.PI / 180) * Math.Sin(2.0 * theta * Math.PI / 180));
                ds.LineSeries.Points.Add(new Point(theta, r));
            }
            myPolarChart.DataCollection.DataList.Add(ds);
        }

        private void AddData1()
        {
            myPolarChart.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeries ds = new Specialized2DCharts.DataSeries();

            myPolarChart.ChartStyle.Rmax = 1;
            myPolarChart.ChartStyle.Rmin = -7;
            myPolarChart.ChartStyle.NTicks = 4;
            myPolarChart.ChartStyle.AngleStep = 30;
            myPolarChart.ChartStyle.AngleDirection = Specialized2DCharts.ChartStylePolar.AngleDirectionEnum.CounterClockWise;
            myPolarChart.ChartStyle.LinePattern = Specialized2DCharts.ChartStylePolar.LinePatternEnum.Dot;
            myPolarChart.ChartStyle.LineColor = Brushes.Black;

            ds= new Specialized2DCharts.DataSeries();
            ds.LineColor = Brushes.Red;
            for (int i = 0; i < 361; i++)
            {
                double theta = 1.0 * i;
                double r = Math.Log(1.001 + Math.Sin(2 * theta * Math.PI / 180));
                ds.LineSeries.Points.Add(new Point(theta, r));
            }
            myPolarChart.DataCollection.DataList.Add(ds);

            ds = new Specialized2DCharts.DataSeries();
            ds.LineColor = Brushes.Blue;
            for (int i = 0; i < 361; i++)
            {
                double theta = 1.0 * i;
                double r = Math.Log(1.001 + Math.Cos(2 * theta * Math.PI / 180));
                ds.LineSeries.Points.Add(new Point(theta, r));
            }
            myPolarChart.DataCollection.DataList.Add(ds);
        }
    }
}
