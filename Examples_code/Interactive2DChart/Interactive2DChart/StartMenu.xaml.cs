using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Interactive2DChart
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>

    public partial class StartMenu : System.Windows.Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            AutomaticTicks at = new AutomaticTicks();
            at.ShowDialog();
        }

        private void Zooming_Click(object sender, RoutedEventArgs e)
        {
            ChartZooming cz = new ChartZooming();
            cz.ShowDialog();
        }

        private void Panning_Click(object sender, RoutedEventArgs e)
        {
            ChartPanning cp = new ChartPanning();
            cp.ShowDialog();
        }

        private void MouseWheel_Click(object sender, RoutedEventArgs e)
        {
            MouseWheelZooming mwz = new MouseWheelZooming();
            mwz.ShowDialog();
        }

        private void Retrieve_Click(object sender, RoutedEventArgs e)
        {
            RetrieveChartData rcd = new RetrieveChartData();
            rcd.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}