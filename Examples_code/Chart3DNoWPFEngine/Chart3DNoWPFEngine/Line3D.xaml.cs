using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Chart3DNoWPFEngine
{
    /// <summary>
    /// Interaction logic for Line3D.xaml
    /// </summary>
    public partial class Line3D : Window
    {
        private ChartStyle cs;
        private DataSeriesLine3D ds;
        public Line3D()
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
            ds = new DataSeriesLine3D();
            cs.ChartCanvas = this.chartCanvas;
            cs.GridlinePattern = ChartStyle.GridlinePatternEnum.Solid;
            cs.Elevation = double.Parse(tbElevation.Text);
            cs.Azimuth = double.Parse(tbAzimuth.Text);
            cs.Xmin = -1;
            cs.Xmax = 1;
            cs.Ymin = -1;
            cs.Ymax = 1;
            cs.Zmin = 0;
            cs.Zmax = 30;
            cs.XTick = 0.5;
            cs.YTick = 0.5;
            cs.ZTick = 5;
            cs.Title = "No Title";
            cs.AddChartStyle();

            ds.LineColor = Brushes.Red;
            for (int i = 0; i < 300; i++)
            {
                double t = 0.1 * i;
                double x = Math.Exp(-t / 30) * Math.Cos(t);
                double y = Math.Exp(-t / 30) * Math.Sin(t);
                double z = t;
                ds.Point3DList.Add(new Point3D(x, y, z));
            }
            ds.AddLine3D(cs);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            AddChart();
        }
    }
}
