using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Transformation3D
{
    public partial class TranslateTransformation : Window
    {

        public TranslateTransformation()
        {
            InitializeComponent();
            SetMatrixCamera();
            SetTransform();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            SetMatrixCamera();
            SetTransform();
        }

        private void SetTransform()
        {
            myTransform.OffsetX = Double.Parse(tbOffsetX.Text);
            myTransform.OffsetY = Double.Parse(tbOffsetY.Text);
            myTransform.OffsetZ = Double.Parse(tbOffsetZ.Text);
        }

        private void SetMatrixCamera()
        {
            Point3D cameraPosition = new Point3D(3, 3, 3);
            Vector3D lookDirection = new Vector3D(-3, -3, -3);
            Vector3D upDirection = new Vector3D(0, 1, 0);
            double w = 6;
            double zn = 1;
            double zf = 100;
            double fov = 60;
            double aspectRatio = 1.0;
            myCameraMatrix.ViewMatrix = Utility.SetViewMatrix(cameraPosition,
                lookDirection, upDirection);

            if (rbOrthographic.IsChecked == true)
            {
                myCameraMatrix.ProjectionMatrix =
                    Utility.SetOrthographic(w, w, zn, zf);
            }
            else if (rbPerspective.IsChecked == true)
            {
                myCameraMatrix.ProjectionMatrix =
                    Utility.SetPerspectiveFov(fov, aspectRatio, zn, zf);
            }
        }
    }
}