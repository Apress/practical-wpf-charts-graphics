using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    /// <summary>
    /// Interaction logic for PieCharts.xaml
    /// </summary>
    public partial class PieCharts : Window
    {
        private PieStyle ps;
        private PieLegend pl;

        public PieCharts()
        {
            InitializeComponent();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = chartGrid.ActualWidth;
            double height = chartGrid.ActualHeight;
            double side = width;
            if (width > height)
                side = height;
            chartCanvas.Width = side;
            chartCanvas.Height = side;
            chartCanvas.Children.Clear();
            legendCanvas.Children.Clear();
            AddChart();
        }

        private void AddChart()
        {
            ps = new PieStyle();
            pl = new PieLegend();
            double[] data = new double[] { 30, 35, 15, 10, 8 };
            //int[] explode = new int[] { 20, 0, 0, 20, 0 };
            string[] labels = new string[] { "Soc. Sec. Tax", "Income Tax", "Borrowing", "Corp. Tax", "Misc." }; 
            for (int i = 0; i < data.Length; i++)
            {
                ps.DataList.Add(data[i]);
                //ps.ExplodeList.Add(explode[i]);
                ps.LabelList.Add(labels[i]);
            }
            ps.ColormapBrushes.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Summer;
            ps.AddPie(chartCanvas);
            pl.IsLegendVisible = true;
            pl.AddLegend(legendCanvas, ps);
        }
    }
}
