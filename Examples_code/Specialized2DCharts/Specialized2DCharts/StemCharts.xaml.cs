using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    /// <summary>
    /// Interaction logic for StemCharts.xaml
    /// </summary>
    public partial class StemCharts : Window
    {
        private ChartStyleGridlines cs;
        private DataCollectionStem dc;
        private DataSeries ds;

        public StemCharts()
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
            dc = new DataCollectionStem();
            ds = new DataSeries();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Stem Chart";
            cs.Xmin = 0;
            cs.Xmax = 8;
            cs.Ymin = -1.5;
            cs.Ymax = 1.5;
            cs.XTick = 1;
            cs.YTick = 0.5;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Draw the stair step chart:
            dc.DataList.Clear();
            ds = new DataSeries();

            for (int i = 0; i < 50; i++)
            {
                ds.LineSeries.Points.Add(new Point(0.4 * i, Math.Sin(0.4 * i)));
            }

            ds.LineColor = Brushes.Red;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.Diamond;
            ds.Symbols.FillColor = Brushes.Yellow;
            ds.Symbols.BorderColor = Brushes.DarkGreen;


            dc.DataList.Add(ds);
            dc.AddStems(cs);
        }        
    }
}
