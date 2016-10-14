using System;
using System.Windows;
using System.Windows.Media;

namespace Transformation2D
{
    public partial class MatrixOperations : Window
    {
        public MatrixOperations()
        {
            InitializeComponent();

            // Invert matrix:
            Matrix m = new Matrix(1, 2, 3, 4, 0, 0);
            tbOriginal.Text = "(" + m.ToString() + ")";
            m.Invert();
            tbInvert.Text = "(" + m.ToString() + ")";

            // Matrix multiplication:
            Matrix m1 = new Matrix(1, 2, 3, 4, 0, 1);
            Matrix m2 = new Matrix(0, 1, 2, 1, 0, 1);
            Matrix m12 = Matrix.Multiply(m1, m2);
            Matrix m21 = Matrix.Multiply(m2, m1);

            tbM1M2.Text = "M1 = (" + m1.ToString() + "), " +
                " M2 = (" + m2.ToString() + ")";
            tbM12.Text = "(" + m12.ToString() + ")";
            tbM21.Text = "(" + m21.ToString() + ")";
        }
    }
}
