using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Transformation3D
{
    public partial class PerspectiveProjection : System.Windows.Window
    {

        public PerspectiveProjection()
        {
            InitializeComponent();
            SetMatrixCamera();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            SetMatrixCamera();
        }

        private void SetMatrixCamera()
        {
            Point3D cameraPosition = Point3D.Parse(tbCameraPosition.Text);
            Vector3D lookDirection = Vector3D.Parse(tbLookDirection.Text);
            Vector3D upDirection = Vector3D.Parse(tbUpDirection.Text);
            double fov = Double.Parse(tbFieldOfView.Text);
            double zn = Double.Parse(tbNearPlane.Text);
            double zf = Double.Parse(tbFarPlane.Text);
            double aspectRatio = 1.0;

            myCameraMatrix.ViewMatrix = Utility.SetViewMatrix(cameraPosition,
                lookDirection, upDirection);
            myCameraMatrix.ProjectionMatrix = 
                Utility.SetPerspectiveFov(fov, aspectRatio, zn, zf);
        }
    }
}