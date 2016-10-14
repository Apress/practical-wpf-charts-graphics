using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Chart3DNoWPFEngine
{
    /// <summary>
    /// Interaction logic for SurfaceChart.xaml
    /// </summary>
    public partial class SurfaceChart : Window
    {
        private ChartStyle cs;
        private DataSeriesSurface ds;
        private DrawSurfaceChart dsc;

        public SurfaceChart()
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
            cs = new ChartStyle();
            cs.ChartCanvas = this.chartCanvas;
            cs.GridlinePattern = ChartStyle.GridlinePatternEnum.Solid;
            cs.Elevation = 30;
            cs.Azimuth = -37;
            cs.Title = "No Title";
            cs.IsColorBar = true;
            cs.AddChartStyle();

            ds = new DataSeriesSurface();
            ds.LineColor = Brushes.Black;
            Utility.Peak3D(cs, ds);
            dsc = new DrawSurfaceChart();
            dsc.SurfaceChartType = DrawSurfaceChart.SurfaceChartTypeEnum.Surface;
            dsc.IsColormap = true;
            dsc.IsHiddenLine = false;
            //dsc.IsInterp = true;
            //dsc.NumberInterp = 3;
            dsc.Colormap.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Jet;
            dsc.AddSurfaceChart(cs, ds);
           
        }
    }
}
