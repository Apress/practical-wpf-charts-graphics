using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Specialized3DChart
{
    /// <summary>
    /// Interaction logic for XYColor.xaml
    /// </summary>
    public partial class XYColor : Window
    {
        private ChartStyle2D cs;
        private DataSeriesSurface ds;
        private Draw3DChart d3c;

        public XYColor()
        {
            InitializeComponent();
        }


        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartCanvas.Width = chartGrid.ActualWidth;
            chartCanvas.Height = chartGrid.ActualHeight;
            AddChart();
        }

        private void AddChart()
        {
            chartCanvas.Children.Clear();
            cs = new ChartStyle2D();
            cs.ChartCanvas = this.chartCanvas;
            cs.GridlinePattern = ChartStyle.GridlinePatternEnum.Solid;
            cs.Elevation = 30;
            cs.Azimuth = -37;
            cs.Title = "No Title";
            cs.IsColorBar = true;
            ds = new DataSeriesSurface();
            ds.LineColor = Brushes.Transparent;
            Utility.Peak3D(cs, ds);
            d3c = new Draw3DChart();
            d3c.ChartType = Draw3DChart.ChartTypeEnum.XYColor;            
            cs.AddChartStyle2D(d3c);
            d3c.IsInterp = true;
            d3c.NumberInterp = 5;
            d3c.AddChart(cs, ds);           
        }
    }
}
