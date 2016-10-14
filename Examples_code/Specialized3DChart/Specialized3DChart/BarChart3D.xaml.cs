using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Specialized3DChart
{
    public partial class BarChart3D : Window
    {
        private ChartStyle2D cs;
        private Bar3DStyle ds;
        private Draw3DChart d3c;

        public BarChart3D()
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
            cs.IsColorBar = true;
            cs.Title = "No Title";
            ds = new Bar3DStyle();
            ds.LineColor = Brushes.Black;
            ds.ZOrigin = 0;
            ds.XLength = 0.6;
            ds.YLength = 0.6;
            Utility.Peak3D(cs, ds);

            d3c = new Draw3DChart();
            d3c.Colormap.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Jet;
            d3c.IsBarSingleColor = true;
            d3c.IsColormap = true;
            
            cs.AddChartStyle();
            d3c.AddBar3D(cs, ds);
        }
    }
}


