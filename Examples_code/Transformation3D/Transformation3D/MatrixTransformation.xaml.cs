using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Transformation3D
{
    public partial class MatrixTransformation : System.Windows.Window
    {

        public MatrixTransformation()
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
            Matrix3D m3 = new Matrix3D();
            m3.M11 = Double.Parse(tbM11.Text);
            m3.M21 = Double.Parse(tbM21.Text);
            m3.M31 = Double.Parse(tbM31.Text);
            m3.OffsetX = Double.Parse(tbM41.Text);
            m3.M12 = Double.Parse(tbM12.Text);
            m3.M22 = Double.Parse(tbM22.Text);
            m3.M32 = Double.Parse(tbM32.Text);
            m3.OffsetY = Double.Parse(tbM42.Text);
            m3.M13 = Double.Parse(tbM13.Text);
            m3.M23 = Double.Parse(tbM23.Text);
            m3.M33 = Double.Parse(tbM33.Text);
            m3.OffsetZ = Double.Parse(tbM43.Text);
            m3.M14 = Double.Parse(tbM14.Text);
            m3.M24 = Double.Parse(tbM24.Text);
            m3.M34 = Double.Parse(tbM34.Text);
            m3.M44 = Double.Parse(tbM44.Text);

            myTransform.Matrix = m3;
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