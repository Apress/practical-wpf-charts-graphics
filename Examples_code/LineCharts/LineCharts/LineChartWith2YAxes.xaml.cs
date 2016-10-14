using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineCharts
{
    /// <summary>
    /// Interaction logic for LineChartWithTwoYAxes.xaml
    /// </summary>
    public partial class LineChartWith2YAxes : Window
    {
        private ChartStyleGridlines2Y cs;
        private Legend lg;
        private DataCollection2Y dc;
        private DataSeries2Y ds;

        public LineChartWith2YAxes()
        {
            InitializeComponent();
            AddChart();
        }

        private void AddChart()
        {
            cs = new ChartStyleGridlines2Y();
            lg = new Legend();
            dc = new DataCollection2Y();
            ds = new DataSeries2Y();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Sine and Cosine Chart";
            cs.Xmin = 0;
            cs.Xmax = 30;
            cs.Ymin = -20;
            cs.Ymax = 20;
            cs.YTick = 5;
            cs.XTick = 5;
            cs.Y2min = 100;
            cs.Y2max = 700;
            cs.Y2Tick = 100;
            cs.XLabel = "X Axis";
            cs.YLabel = "Y Axis";
            cs.Y2Label = "Y2 Axis";
            cs.Title = "line Chart with Y2 Axis"; 

            cs.GridlinePattern = ChartStyleGridlines2Y.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Transparent;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel, tbY2Label);

            // Draw Y curve:
            ds = new DataSeries2Y();
            ds.Symbols.BorderColor = Brushes.Blue;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.OpenDiamond;
            ds.LineColor = Brushes.Blue;
            ds.SeriesName = "x * cos (x)";
            for (int i = 0; i < 20; i++)
            {
                double x = 1.0 * i;
                double y = x*Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Draw Y2 curve:
            ds = new DataSeries2Y();
            ds.IsY2Data = true;
            ds.Symbols.BorderColor = Brushes.Red;
            ds.Symbols.SymbolType = Symbols.SymbolTypeEnum.Dot;
            ds.Symbols.FillColor = Brushes.Red;
            ds.LineColor = Brushes.Red;
            ds.SeriesName = "100 + 20 * x";
            ds.LinePattern = DataSeries.LinePatternEnum.DashDot;
            for (int i = 5; i < 30; i++)
            {
                double x = 1.0 * i;
                double y = 100.0 + 20.0 * x;
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);
            dc.AddLines2Y(cs);
            
            lg.LegendCanvas = legendCanvas;
            lg.IsLegend = true;
            lg.IsBorder = true;
            lg.LegendPosition = Legend.LegendPositionEnum.NorthWest;
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
