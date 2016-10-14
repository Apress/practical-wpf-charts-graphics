using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Specialized3DChart
{
    public partial class SurfaceFilledContour : Window
    {
        private ChartStyle2D cs;
        private DataSeriesSurface ds;
        private Draw3DChart d3c;

        public SurfaceFilledContour()
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
            ds = new DataSeriesSurface();
            ds.LineColor = Brushes.Black;
            Utility.Peak3D(cs, ds);

            d3c = new Draw3DChart();
            d3c.Colormap.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Jet;
            d3c.ChartType = Draw3DChart.ChartTypeEnum.SurfaceFillContour3D;
            d3c.IsLineColorMatch = false;
            d3c.NumberContours = 15;
            d3c.AddChart(cs, ds);
        }
    }
}
