using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Specialized2DCharts;

namespace Specialized2DChartControl
{
    /// <summary>
    /// Interaction logic for StairstepControl.xaml
    /// </summary>
    public partial class StairstepControl : UserControl
    {
        private ChartStyleGridlines cs;
        private DataCollectionStairstep dc;
        private DataSeriesStairstep ds;

        public StairstepControl()
        {
            InitializeComponent();
            cs = new ChartStyleGridlines();
            dc = new DataCollectionStairstep();
            ds = new DataSeriesStairstep();
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
                dc.AddStairstep(cs);
                dc.AddLines(cs);
            }
        }


        public ChartStyleGridlines ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public DataCollectionStairstep DataCollection
        {
            get { return dc; }
            set { dc = value; }
        }

        public DataSeriesStairstep DataSeries
        {
            get { return ds; }
            set { ds = value; }
        }
    }
}
