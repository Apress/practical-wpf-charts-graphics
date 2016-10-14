using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Specialized2DCharts;

namespace Specialized2DChartControl
{
    /// <summary>
    /// Interaction logic for BarControl.xaml
    /// </summary>
    public partial class BarControl : UserControl
    {
        private ChartStyleGridlines cs;
        private DataCollectionBar dc;
        private DataSeriesBar ds;

        public BarControl()
        {
            InitializeComponent();
            this.cs = new ChartStyleGridlines();
            this.dc = new DataCollectionBar();
            this.ds = new DataSeriesBar();
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
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
                dc.AddBars(cs);
            }
        }


        public ChartStyleGridlines ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public DataCollectionBar DataCollection
        {
            get { return dc; }
            set { dc = value; }
        }

        public DataSeriesBar DataSeries
        {
            get { return ds; }
            set { ds = value; }
        }
    }
}
