using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Specialized2DCharts;

namespace Specialized2DChartControl
{
    /// <summary>
    /// Interaction logic for PolarControl.xaml
    /// </summary>
    public partial class PolarControl : UserControl
    {
        private ChartStylePolar cs;
        private DataCollectionPolar dc;
        private DataSeries ds;

        public PolarControl()
        {
            InitializeComponent();
            cs = new ChartStylePolar();
            dc = new DataCollectionPolar();
            ds = new DataSeries();
            cs.ChartCanvas = chartCanvas;
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
            AddChart();
        }

        private void AddChart()
        {
            cs.SetPolarAxes();
            if (dc.DataList.Count != 0)
            {
                dc.AddPolar(cs);
            }
        }

        public ChartStylePolar ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public DataCollectionPolar DataCollection
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
