using System;
using System.Windows;
using System.Windows.Controls;
using Specialized2DCharts;

namespace Specialized2DChartControl
{
    /// <summary>
    /// Interaction logic for PieControl.xaml
    /// </summary>
    public partial class PieControl : UserControl
    {
        private PieStyle ps;
        private PieLegend pl;

        public PieControl()
        {
            InitializeComponent();
            ps = new PieStyle();
            pl = new PieLegend();
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
            if (ps.DataList.Count != 0)
            {
                ps.AddPie(chartCanvas);
                if (pl.IsLegendVisible)
                {
                    pl.AddLegend(legendCanvas, ps);
                }
            }
        }

        public PieStyle PieStyle
        {
            get { return ps; }
            set { ps = value; }
        }

        public PieLegend PieLegend
        {
            get { return pl; }
            set { pl = value; }
        }
    }
}
