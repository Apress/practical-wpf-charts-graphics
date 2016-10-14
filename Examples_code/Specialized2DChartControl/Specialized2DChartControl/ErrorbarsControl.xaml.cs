using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Specialized2DCharts;

namespace Specialized2DChartControl
{
    /// <summary>
    /// Interaction logic for ErrorbarsControl.xaml
    /// </summary>
    public partial class ErrorbarsControl : UserControl
    {
        private ChartStyleGridlines cs;
        private DataCollectionErrorbar dc;
        private DataSeriesErrorbar ds;

        public ErrorbarsControl()
        {
            InitializeComponent();
            cs = new ChartStyleGridlines();
            dc = new DataCollectionErrorbar();
            ds = new DataSeriesErrorbar();
            cs.TextCanvas = textCanvas;
            cs.ChartCanvas = chartCanvas;
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
            if (dc.DataList.Count != 0)
            {
                dc.AddErrorbars(cs);
                dc.AddLines(cs);
            }
        }


        public ChartStyleGridlines ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public DataCollectionErrorbar DataCollection
        {
            get { return dc; }
            set { dc = value; }
        }

        public DataSeriesErrorbar DataSeries
        {
            get { return ds; }
            set { ds = value; }
        }

    }
}
