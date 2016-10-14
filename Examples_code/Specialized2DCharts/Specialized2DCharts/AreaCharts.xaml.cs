using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    /// <summary>
    /// Interaction logic for AreaCharts.xaml
    /// </summary>
    public partial class AreaCharts : Window
    {
        private ChartStyleGridlines cs;
        private DataCollectionArea dc;
        private DataSeriesArea area;

        public AreaCharts()
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
            dc = new DataCollectionArea();
            area=new DataSeriesArea();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Area Chart";
            cs.Xmin = 0;
            cs.Xmax = 10;
            cs.Ymin = 0;
            cs.Ymax = 10;
            cs.XTick = 2;
            cs.YTick = 2;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);
            dc.AreaList.Clear();

            // Add sine data:
            area = new DataSeriesArea();
            area.BorderColor = Brushes.Black;
            area.FillColor = Brushes.LightPink;
            for (int i = 0; i < 41; i++)
            {
                area.AreaSeries.Points.Add(new Point(0.25 * i, 2.0 + Math.Sin(0.25 * i)));
            }
            dc.AreaList.Add(area);

            // Add cosine data:
            area = new DataSeriesArea();
            area.BorderColor = Brushes.Black;
            area.FillColor = Brushes.LightBlue;
            for (int i = 0; i < 41; i++)
            {
                area.AreaSeries.Points.Add(new Point(0.25 * i, 2.0 + Math.Cos(0.25 * i)));
            }
            dc.AreaList.Add(area);

            // Add another sine data:
            area = new DataSeriesArea();
            area.BorderColor = Brushes.Black;
            area.FillColor = Brushes.LightGreen;
            for (int i = 0; i < 41; i++)
            {
                area.AreaSeries.Points.Add(new Point(0.25 * i, 3.0 + Math.Sin(0.25 * i)));
            }
            dc.AreaList.Add(area);
            dc.AddAreas(cs);
        }        
    }
}
