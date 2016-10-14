using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Interpolation
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

        private void Linear_Click(object sender, RoutedEventArgs e)
        {
            LinearInterpolation li = new LinearInterpolation();
            li.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Lagrange_Click(object sender, RoutedEventArgs e)
        {
            LagrangeInterpolation li = new LagrangeInterpolation();
            li.ShowDialog();
        }

        private void Barycentric_Click(object sender, RoutedEventArgs e)
        {
            BarycentricInterpolation bi = new BarycentricInterpolation();
            bi.ShowDialog();
        }

        private void Divided_Click(object sender, RoutedEventArgs e)
        {
            DividedDifferenceInterpolation ddi = new DividedDifferenceInterpolation();
            ddi.ShowDialog();
        }

        private void Spline_Click(object sender, RoutedEventArgs e)
        {
            CubicSplineInterpolation csi = new CubicSplineInterpolation();
            csi.ShowDialog();
        }
    }
}
