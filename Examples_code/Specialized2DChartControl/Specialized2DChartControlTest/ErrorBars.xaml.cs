using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for ErrorBars.xaml
    /// </summary>
    public partial class ErrorBars : Window
    {
        public ErrorBars()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myErrorbar.Height = rootGrid.ActualHeight;
            myErrorbar.Width = rootGrid.ActualWidth;
            AddData();
        }

        private void AddData()
        {
            myErrorbar.DataCollection.DataList.Clear();
            myErrorbar.DataCollection.ErrorList.Clear();
            Specialized2DCharts.DataSeriesErrorbar ds = new Specialized2DCharts.DataSeriesErrorbar();

            myErrorbar.ChartStyle.Title = "Error Bar Chart";
            myErrorbar.ChartStyle.Xmin = 0;
            myErrorbar.ChartStyle.Xmax = 12;
            myErrorbar.ChartStyle.Ymin = -1;
            myErrorbar.ChartStyle.Ymax = 6;
            myErrorbar.ChartStyle.XTick = 2;
            myErrorbar.ChartStyle.YTick = 1;
            myErrorbar.ChartStyle.GridlinePattern= Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            myErrorbar.ChartStyle.GridlineColor = Brushes.Black;

            ds.LineColor = Brushes.Blue;
            ds.Symbols.SymbolType= Specialized2DCharts.Symbols.SymbolTypeEnum.Circle;
            ds.Symbols.BorderColor = Brushes.DarkGreen;
            ds.ErrorLineColor = Brushes.Red;

            for (int i = 2; i < 22; i++)
            {
                ds.LineSeries.Points.Add(new Point(0.5 * i, 10.0 * Math.Exp(-0.5 * i)));
                ds.ErrorLineSeries.Points.Add(new Point(0.5 * i, 3.0 / (0.5 * i)));
            }
            myErrorbar.DataCollection.DataList.Add(ds);
        }        
    }
}
