using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Transformation2D
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            MatrixOperations mo = new MatrixOperations();
            mo.ShowDialog();
        }

        private void Transforms_Click(object sender, RoutedEventArgs e)
        {
            MatrixTransforms mt = new MatrixTransforms();
            mt.ShowDialog();
        }

        private void NormalLine_Click(object sender, RoutedEventArgs e)
        {
            PerpendicularLine pl = new PerpendicularLine();
            pl.ShowDialog();
        }

        private void Objects_Click(object sender, RoutedEventArgs e)
        {
            ObjectMatrixTransforms omt = new ObjectMatrixTransforms();
            omt.ShowDialog();
        }

        private void scale_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransforms st = new ScaleTransforms();
            st.ShowDialog();
        }

        private void translate_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransforms tt = new TranslateTransforms();
            tt.ShowDialog();
        }

        private void rotate_Click(object sender, RoutedEventArgs e)
        {
            RotateTransforms rt = new RotateTransforms();
            rt.ShowDialog();
        }

        private void skew_Click(object sender, RoutedEventArgs e)
        {
            SkewTransforms st = new SkewTransforms();
            st.ShowDialog();
        }

        private void combine_Click(object sender, RoutedEventArgs e)
        {
            CombineTransforms ct = new CombineTransforms();
            ct.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }      
    }
}
