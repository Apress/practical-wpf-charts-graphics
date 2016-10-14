using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LineCharts
{
    /// <summary>
    /// Interaction logic for LineWithSymbols.xaml
    /// </summary>
    public partial class LineChartWithSymbols : Window
    {

        private ChartStyleGridlines cs;
        private Legend lg;
        private DataCollection dc;
        private DataSeries ds;

        public LineChartWithSymbols()
        {
            InitializeComponent();
            AddChart();
        }

        private void AddChart()
        {
            cs = new ChartStyleGridlines();
            lg = new Legend();
            dc = new DataCollection();
            ds = new DataSeries();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Sine and Cosine Chart";
            cs.Xmin = 0;
            cs.Xmax = 7;
            cs.Ymin = -1.5;
            cs.Ymax = 1.5;
            cs.YTick = 0.5;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Draw Sine curve:
            ds = new DataSeries();
            ds.Symbols.BorderColor = Brushes.Blue;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.OpenDiamond;
            ds.LineColor = Brushes.Blue;
            //ds.LineThickness = 1;
            ds.SeriesName = "Sine";
            for (int i = 0; i < 70; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Draw cosine curve:
            ds = new DataSeries();
            ds.Symbols.BorderColor = Brushes.Red;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.Dot;
            ds.Symbols.FillColor = Brushes.Red;
            ds.LineColor = Brushes.Red;
            ds.SeriesName = "Cosine";
            ds.LinePattern = DataSeries.LinePatternEnum.DashDot;
            //ds.LineThickness = 2;
            for (int i = 0; i < 70; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Draw sine^2 curve:
            ds = new DataSeries();
            ds.Symbols.BorderColor = Brushes.DarkGreen;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.OpenTriangle;
            ds.LineColor = Brushes.DarkGreen;
            ds.SeriesName = "Sine^2";
            ds.LinePattern = DataSeries.LinePatternEnum.Dot;
            //ds.LineThickness = 2;
            for (int i = 0; i < 70; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x) * Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);
            dc.AddLines(cs);

            lg.LegendCanvas = legendCanvas;
            lg.IsLegend = true;
            lg.IsBorder = true;
            lg.LegendPosition = Legend.LegendPositionEnum.NorthEast;
            lg.AddLegend(cs.ChartCanvas, dc);


        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            legendCanvas.Children.Clear();
            chartCanvas.Children.RemoveRange(1, chartCanvas.Children.Count - 1);
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart();


        }
    }
}
