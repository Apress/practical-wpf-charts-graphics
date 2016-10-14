using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace StockCharts
{
    /// <summary>
    /// Interaction logic for StaticStockCharts.xaml
    /// </summary>
    public partial class MovingAverage : Window
    {
        private ChartStyle cs;
        private DataCollectionMA dc;
        private DataSeriesMA ds;
        private TextFileReader tfr;

        public MovingAverage()
        {
            InitializeComponent();
            dc = new DataCollectionMA();
            tfr = new TextFileReader();
            cs = new ChartStyle();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            dc.DataList.Clear();
            ds = new DataSeriesMA();
            ds.DataString = tfr.LoadFile();
            ds.NDays = 5;
            ds.LineColor = Brushes.Black;
            ds.SMALineColor = Brushes.Red;
            ds.WMALineColor = Brushes.DarkGreen;
            ds.EMALineColor = Brushes.Blue;
            dc.DataList.Add(ds);
            AddChart();
        }

        private void AddChart()
        {
            if (dc.DataList.Count > 0)
            {
                cs = new ChartStyle();
                cs.ChartCanvas = chartCanvas;
                cs.TextCanvas = textCanvas;
                cs.Xmin = -1;
                cs.Xmax = ds.DataString.GetLength(1);
                cs.XTick = 3;
                cs.Ymin = double.Parse(txYmin.Text);
                cs.Ymax = double.Parse(txYmax.Text);
                cs.YTick = double.Parse(txYTick.Text);
                cs.Title = "Stock Chart";
                cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel, ds);
                dc.StockChartType = DataCollection.StockChartTypeEnum.Candle;
                dc.AddStockChart(cs);
                dc.AddSimpleMovingAverage(cs);
                dc.AddWeightedMovingAverage(cs);
                dc.AddExponentialMovingAverage(cs);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

