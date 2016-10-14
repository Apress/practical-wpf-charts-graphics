using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    /// <summary>
    /// Interaction logic for ErrorBars.xaml
    /// </summary>
    public partial class ErrorBars : Window
    {
        private ChartStyleGridlines cs;
        private DataCollectionErrorbar dc;
        private DataSeriesErrorbar ds;

        public ErrorBars()
        {
            InitializeComponent();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart();
        }

        private void AddChart()
        {
            cs = new ChartStyleGridlines();
            dc = new DataCollectionErrorbar();
            ds = new DataSeriesErrorbar();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Error Bar Chart";
            cs.Xmin = 0;
            cs.Xmax = 12;
            cs.Ymin = -1;
            cs.Ymax = 6;
            cs.XTick = 2;
            cs.YTick = 1;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            dc.DataList.Clear();
            dc.ErrorList.Clear();
            ds = new DataSeriesErrorbar();
            ds.LineColor = Brushes.Blue;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.Circle;
            ds.Symbols.BorderColor = Brushes.DarkGreen;
            ds.ErrorLineColor = Brushes.Red;

            for (int i = 2; i < 22; i++)
            {
                ds.LineSeries.Points.Add(new Point(0.5 * i, 10.0 * Math.Exp(-0.5 * i)));
                ds.ErrorLineSeries.Points.Add(new Point(0.5 * i, 3.0 / (0.5 * i)));
            }
            dc.DataList.Add(ds);
            dc.AddErrorbars(cs);
            dc.AddLines(cs);
        }        
    }
}
