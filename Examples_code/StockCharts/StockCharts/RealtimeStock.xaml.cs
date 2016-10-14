using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace StockCharts
{
    /// <summary>
    /// Interaction logic for RealtimeStock.xaml
    /// </summary>
    public partial class RealtimeStock : Window
    {
        YahooStock ys;
        DispatcherTimer timer;

        public RealtimeStock()
        {
            InitializeComponent();
            AddYahooStockChart();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            AddYahooStockChart();
        }

        private void AddYahooStockChart()
        {
            ys = new YahooStock();
            ys.StockPeriod = "1d";
            ys.ChartType = "c";
            ys.UpdateIntervalInSeconds = 20;
            ys.Symbol = txStockSymbol.Text;

            if (ys.CheckInternetConnection())
            {
                chartImage.Source = new BitmapImage(new Uri(ys.ChartURI));
            }
            UpdateStock();
        }

        private void UpdateStock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(ys.UpdateIntervalInSeconds * 1000);
            timer.Tick += new EventHandler(Timer_Tick);
            if (ys.CheckInternetConnection())
            {
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs eArgs)
        {
            //chartImage.Source = new BitmapImage(new Uri(ys.ChartURI));
            ys = new YahooStock();
            ys.StockPeriod = "1d";
            ys.ChartType = "c";
            ys.UpdateIntervalInSeconds = 20;
            ys.Symbol = txStockSymbol.Text;

            if (ys.CheckInternetConnection())
            {
                chartImage.Source = new BitmapImage(new Uri(ys.ChartURI));
            }
            MessageBox.Show(ys.Symbol.ToString());
        }
    }
}
