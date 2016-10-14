using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LineCharts
{
    /// <summary>
    /// Interaction logic for LineChartExample.xaml
    /// </summary>
    public partial class LineChartExample : Window
    {
        private ChartStyle cs;
        private DataCollection dc;
        private DataSeries ds;

        public LineChartExample()
        {
            InitializeComponent();
        }

        private void AddChart()
        {
            cs = new ChartStyle();
            cs.ChartCanvas = chartCanvas;
            dc = new DataCollection();
            cs.Xmin = 0;
            cs.Xmax = 7;
            cs.Ymin = -1.1;
            cs.Ymax = 1.1;

            // Draw Sine curve:
            ds = new DataSeries();
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 2;
            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Draw cosine curve:
            ds = new DataSeries();
            ds.LineColor = Brushes.Red;
            ds.LinePattern = DataSeries.LinePatternEnum.DashDot;
            ds.LineThickness = 2;

            for (int i = 0; i < 50; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);
            dc.AddLines(cs);
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartCanvas.Width = chartGrid.ActualWidth - 20;
            chartCanvas.Height = chartGrid.ActualHeight - 20;
            chartCanvas.Children.Clear();
            AddChart();           
        }
    }
}
