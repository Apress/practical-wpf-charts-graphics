using System;
using System.Windows;
using System.Windows.Media;

namespace Transformation2D
{
    public partial class MatrixTransforms : Window
    {

        public MatrixTransforms()
        {
            InitializeComponent();

            // Original matrix:
            Matrix m = new Matrix(1, 2, 3, 4, 0, 1);
            tbOriginal.Text = "(" + m.ToString() + ")";

            //Scale:
            m.Scale(1, 0.5);
            tbScale.Text = "(" + m.ToString() + ")";

            // Scale - Prepend:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.ScalePrepend(1, 0.5);
            tbScalePrepend.Text = "(" + m.ToString() + ")";

            //Translation:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.Translate(1, 0.5);
            tbTranslate.Text = "(" + m.ToString() + ")";

            // Translation - Prepend:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.TranslatePrepend(1, 0.5);
            tbTranslatePrepend.Text =
                   "(" + m.ToString() + ")";

            //Rotation:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.Rotate(45);
            tbRotate.Text = "(" + MatrixRound(m).ToString()
                            + ")";

            // Rotation - Prepend:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.RotatePrepend(45);
            tbRotatePrepend.Text = "(" +
                  MatrixRound(m).ToString() + ")";

            //Rotation at (x = 1, y = 2):
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.RotateAt(45, 1, 2);
            tbRotateAt.Text = "(" +
                   MatrixRound(m).ToString() + ")";

            // Rotation at (x = 1, y = 2) - Prepend:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.RotateAtPrepend(45, 1, 2);
            tbRotateAtPrepend.Text = "(" +
                 MatrixRound(m).ToString() + ")";

            // Skew:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.Skew(45, 30);
            tbSkew.Text = "(" + MatrixRound(m).ToString() + ")";

            // Skew - Prepend:
            m = new Matrix(1, 2, 3, 4, 0, 1);
            m.SkewPrepend(45, 30);
            tbSkewPrepend.Text = "(" + MatrixRound(m).ToString() + ")";
        }

        private Matrix MatrixRound(Matrix m)
        {
            m.M11 = Math.Round(m.M11, 3);
            m.M12 = Math.Round(m.M12, 3);
            m.M21 = Math.Round(m.M21, 3);
            m.M22 = Math.Round(m.M22, 3);
            m.OffsetX = Math.Round(m.OffsetX, 3);
            m.OffsetY = Math.Round(m.OffsetY, 3);
            return m;
        }
    }
}

