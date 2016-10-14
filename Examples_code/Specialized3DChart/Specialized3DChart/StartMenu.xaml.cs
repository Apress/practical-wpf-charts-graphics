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

namespace Specialized3DChart
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

        private void XYColor_Click(object sender, RoutedEventArgs e)
        {
            XYColor xy = new XYColor();
            xy.ShowDialog();
        }

        private void Contour_Click(object sender, RoutedEventArgs e)
        {
            Contour cc = new Contour();
            cc.ShowDialog();
        }

        private void FilledContour_Click(object sender, RoutedEventArgs e)
        {
            FilledContour fc = new FilledContour();
            fc.ShowDialog();
        }

        private void MeshContour_Click(object sender, RoutedEventArgs e)
        {
            MeshContour mc = new MeshContour();
            mc.ShowDialog();
        }

        private void SurfaceContour_Click(object sender, RoutedEventArgs e)
        {
            SurfaceContour sc = new SurfaceContour();
            sc.ShowDialog();
        }

        private void SurfaceFilledContour_Click(object sender, RoutedEventArgs e)
        {
            SurfaceFilledContour sfc = new SurfaceFilledContour();
            sfc.ShowDialog();
        }

        private void Bar3D_Click(object sender, RoutedEventArgs e)
        {
            BarChart3D bc = new BarChart3D();
            bc.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
