using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    /// <summary>
    /// Interaction logic for StairstepCharts.xaml
    /// </summary>
    public partial class StairstepCharts : Window
    {
        private ChartStyleGridlines cs;
        private DataCollectionStairstep dc;
        private DataSeriesStairstep ds;

        public StairstepCharts()
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
            dc = new DataCollectionStairstep();
            ds = new DataSeriesStairstep();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Stair Step Chart";
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
            ds = new DataSeriesStairstep();
            ds.StairstepLineColor = Brushes.Red;
            for (int i = 0; i < 50; i++)
            {
                ds.LineSeries.Points.Add(new Point(0.4 * i, Math.Sin(0.4 * i)));
            }

            ds.LineColor = Brushes.Black;
            ds.LinePattern = DataSeries.LinePatternEnum.Dash;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.Circle;

            dc.DataList.Add(ds);
            dc.AddStairstep(cs);
            dc.AddLines(cs);
        }        
    }
}
