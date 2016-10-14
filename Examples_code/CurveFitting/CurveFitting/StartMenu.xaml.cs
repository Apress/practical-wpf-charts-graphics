using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CurveFitting
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

        private void Straightline_Click(object sender, RoutedEventArgs e)
        {
            StraightLineFit sf = new StraightLineFit();
            sf.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Linear_Click(object sender, RoutedEventArgs e)
        {
            LinearRegression lr = new LinearRegression();
            lr.ShowDialog();
        }

        private void Polynomial_Click(object sender, RoutedEventArgs e)
        {
            PolynomialFit pf = new PolynomialFit();
            pf.ShowDialog();
        }

        private void Weighted_Click(object sender, RoutedEventArgs e)
        {
            WeightedLinearRegression wlr = new WeightedLinearRegression();
            wlr.ShowDialog();
        }
    }
}

