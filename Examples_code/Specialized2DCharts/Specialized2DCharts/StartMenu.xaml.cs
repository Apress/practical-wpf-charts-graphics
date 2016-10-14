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

namespace Specialized2DCharts
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Stairstep_Click(object sender, RoutedEventArgs e)
        {
            StairstepCharts ss = new StairstepCharts();
            ss.ShowDialog();
        }

        private void Stem_Click(object sender, RoutedEventArgs e)
        {
            StemCharts sc = new StemCharts();
            sc.ShowDialog();
        }

        private void Error_Click(object sender, RoutedEventArgs e)
        {
            ErrorBars eb = new ErrorBars();
            eb.ShowDialog();
        }

        private void Area_Click(object sender, RoutedEventArgs e)
        {
            AreaCharts ac = new AreaCharts();
            ac.ShowDialog();
        }

        private void Polar_Click(object sender, RoutedEventArgs e)
        {
            PolarCharts pc = new PolarCharts();
            pc.ShowDialog();
        }

        private void Pie_Click(object sender, RoutedEventArgs e)
        {
            PieCharts pc = new PieCharts();
            pc.ShowDialog();
        }
    }
}
