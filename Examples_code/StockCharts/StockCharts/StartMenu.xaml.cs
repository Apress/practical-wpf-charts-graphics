using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockCharts
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void StaticStock_Click(object sender, RoutedEventArgs e)
        {
            StaticStockCharts ssc = new StaticStockCharts();
            ssc.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MovingAverage_Click(object sender, RoutedEventArgs e)
        {
            MovingAverage ma = new MovingAverage();
            ma.ShowDialog();
        }

        private void YahooStock_Click(object sender, RoutedEventArgs e)
        {
            YahooStockChart ysc = new YahooStockChart();
            ysc.ShowDialog();
        }
    }
}
