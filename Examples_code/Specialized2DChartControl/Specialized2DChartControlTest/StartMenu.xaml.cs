using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Specialized2DChartControlTest
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

        private void Bar_Click(object sender, RoutedEventArgs e)
        {
            BarCharts bar = new BarCharts();
            bar.ShowDialog();
        }

        private void MultipleBars_Click(object sender, RoutedEventArgs e)
        {
            MultipleBarCharts bars = new MultipleBarCharts();
            bars.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Stairstep_Click(object sender, RoutedEventArgs e)
        {
            StairstepChart ss = new StairstepChart();
            ss.ShowDialog();
        }

        private void Stem_Click(object sender, RoutedEventArgs e)
        {
            StemChart sc = new StemChart();
            sc.ShowDialog();
        }

        private void Error_Click(object sender, RoutedEventArgs e)
        {
            ErrorBars eb = new ErrorBars();
            eb.ShowDialog();
        }

        private void Area_Click(object sender, RoutedEventArgs e)
        {
            AreaChart ac = new AreaChart();
            ac.ShowDialog();
        }

        private void Polar_Click(object sender, RoutedEventArgs e)
        {
            PolarChart pc = new PolarChart();
            pc.ShowDialog();
        }

        private void Pie_Click(object sender, RoutedEventArgs e)
        {
            PieChart pc = new PieChart();
            pc.ShowDialog();
        }

        private void MultipleCharts_Click(object sender, RoutedEventArgs e)
        {
            MultipleCharts mc = new MultipleCharts();
            mc.ShowDialog();
        }
    }
}
