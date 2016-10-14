using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Transformation3D
{
    /// <summary>
    /// Interaction logic for Matrix3DTransformation.xaml
    /// </summary>

    public partial class Matrix3DTransforms : System.Windows.Window
    {

        public Matrix3DTransforms()
        {
            InitializeComponent();

            // Original matrix:
            Matrix3D M = new Matrix3D(1, 2, 3, 4,
                                      2, 1, 0, 0,
                                      0, 0, 1, 0,
                                      1, 2, 3, 1); 
            Matrix3D M1 = M;
            tbOriginal.Text = "(" + M.ToString() + ")";

            //Scale:
            M.Scale(new Vector3D(0.5, 1.5, 2.5));
            tbScale.Text = "(" + M.ToString() + ")";

            M = M1; // Reset M to the original matrix.
            M.ScalePrepend(new Vector3D(0.5, 1.5, 2.5));
            tbScalePrepend.Text = "(" + M.ToString() + ")";

            //Translation:
            M = M1; // Reset M to the original matrix.
            M.Translate(new Vector3D(100, 150, 200));
            tbTranslate.Text = "(" + M.ToString() + ")";

            // Translation - Prepend:
            M = M1; // Reset M to the original matrix.
            M.TranslatePrepend(new Vector3D(100, 150, 200));
            tbTranslatePrepend.Text = "(" + M.ToString() + ")";

            // Rotation:
            M = M1; // Reset M to the original matrix.
            M.Rotate(new Quaternion(new Vector3D(1, 2, 3), 45));
            tbRotate.Text = "(" + Utility.Matrix3DRound(M, 3).ToString() + ")";

            // Rotation - Prepend:
            M = M1; // Reset M to the original matrix.
            M.RotatePrepend(new Quaternion(new Vector3D(1, 2, 3), 45));
            tbRotatePrepend.Text = "(" + Utility.Matrix3DRound(M, 3).ToString() + ")";

            //Rotation at (x = 10, y = 30, z = 20):
            M = M1; // Reset M to the original matrix.
            M.RotateAt(new Quaternion(new Vector3D(1, 2, 3), 45), new Point3D(10, 30, 20));
            tbRotateAt.Text = "(" + Utility.Matrix3DRound(M, 3).ToString() + ")";

            // Rotation at (x = 10, y = 30, z = 20) - Prepend:
            M = M1; // Reset M to the original matrix.
            M.RotateAtPrepend(new Quaternion(new Vector3D(1, 2, 3), 45), new Point3D(10, 30, 20));
            tbRotateAtPrepend.Text = "(" + Utility.Matrix3DRound(M, 3).ToString() + ")";

        }
    }
}