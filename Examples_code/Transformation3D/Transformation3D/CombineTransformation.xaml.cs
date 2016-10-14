using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Transformation3D
{
    public partial class CombineTransformation : System.Windows.Window
    {

        public CombineTransformation()
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
            // Scale transformation:
            scaleTransform.ScaleX = Double.Parse(tbScaleX.Text);
            scaleTransform.ScaleY = Double.Parse(tbScaleY.Text);
            scaleTransform.ScaleZ = Double.Parse(tbScaleZ.Text);
           
            // Rotation Transformation:
            Vector3D rotateAxis = Vector3D.Parse(tbAxis.Text);
            double rotateAngle = Double.Parse(tbAngle.Text);
            rotateTransform.Rotation = new AxisAngleRotation3D(rotateAxis, rotateAngle);
        }

        private void SetMatrixCamera()
        {
            Point3D cameraPosition = new Point3D(3, 3, 3);
            Vector3D lookDirection = new Vector3D(-1, -1, -1);
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