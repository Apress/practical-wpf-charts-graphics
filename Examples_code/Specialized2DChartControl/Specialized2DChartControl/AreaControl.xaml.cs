using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Specialized2DCharts;

namespace Specialized2DChartControl
{
    /// <summary>
    /// Interaction logic for AreaControl.xaml
    /// </summary>
    public partial class AreaControl : UserControl
    {
        private ChartStyleGridlines cs;
        private DataCollectionArea dc;
        private DataSeriesArea area;

        public AreaControl()
        {
            InitializeComponent();
            cs = new ChartStyleGridlines();
            dc = new DataCollectionArea();
            area = new DataSeriesArea();
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
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
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);
            if (dc.AreaList.Count != 0)
            {
                dc.AddAreas(cs);
            }
        }

        public ChartStyleGridlines ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public DataCollectionArea DataCollection
        {
            get { return dc; }
            set { dc = value; }
        }

        public DataSeriesArea DataSeries
        {
            get { return area; }
            set { area = value; }
        }
    }
}
