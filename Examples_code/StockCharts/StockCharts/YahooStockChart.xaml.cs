using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StockCharts
{
    /// <summary>
    /// Interaction logic for YahooStockChart.xaml
    /// </summary>
    public partial class YahooStockChart : Window
    {
       YahooStock ys;

        public YahooStockChart()
        {
            InitializeComponent();
            ys = new YahooStock();
            ys.StockPeriod = "1d";
            ys.ChartType = "c";
            ys.Symbol = txStockSymbol.Text;
            chartImage.Source = new BitmapImage(new Uri(ys.ChartURI));
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ys.Symbol = txStockSymbol.Text;
            chartImage.Source = new BitmapImage(new Uri(ys.ChartURI));
        }
    }
}
