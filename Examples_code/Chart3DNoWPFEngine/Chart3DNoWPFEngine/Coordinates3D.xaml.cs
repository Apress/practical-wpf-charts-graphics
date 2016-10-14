using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chart3DNoWPFEngine
{
    /// <summary>
    /// Interaction logic for Coordinates3D.xaml
    /// </summary>
    public partial class Coordinates3D : Window
    {
        ChartStyle cs;
        public Coordinates3D()
        {
            InitializeComponent();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartCanvas.Width = chartGrid.ActualWidth;
            chartCanvas.Height = chartGrid.ActualHeight;
            AddCoordinateAxes();
        }

        private void AddCoordinateAxes()
        {
            chartCanvas.Children.Clear();
            cs = new ChartStyle();
            cs.ChartCanvas = this.chartCanvas;
            cs.GridlinePattern = ChartStyle.GridlinePatternEnum.Solid;
            cs.Elevation = double.Parse(tbElevation.Text);
            cs.Azimuth = double.Parse(tbAzimuth.Text);
            cs.AddChartStyle();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            AddCoordinateAxes();
        }
    }
}
