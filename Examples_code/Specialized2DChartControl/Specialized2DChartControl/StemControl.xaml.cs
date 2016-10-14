using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Specialized2DCharts;

namespace Specialized2DChartControl
{
    /// <summary>
    /// Interaction logic for StemControl.xaml
    /// </summary>
    public partial class StemControl : UserControl
    {
        private ChartStyleGridlines cs;
        private DataCollectionStem dc;
        private DataSeries ds;

        public StemControl()
        {
            InitializeComponent();
            cs = new ChartStyleGridlines();
            dc = new DataCollectionStem();
            ds = new DataSeries();
            cs.TextCanvas = textCanvas;
            cs.ChartCanvas = chartCanvas;
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            legendCanvas.Children.Clear();
            chartCanvas.Children.RemoveRange(1, chartCanvas.Children.Count - 1);
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart();
        }

        private void AddChart()
        {
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);
            if (dc.DataList.Count != 0)
            {
                dc.AddStems(cs);
            }
        }


        public ChartStyleGridlines ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public DataCollectionStem DataCollection
        {
            get { return dc; }
            set { dc = value; }
        }

        public DataSeries DataSeries
        {
            get { return ds; }
            set { ds = value; }
        }
    }
}
