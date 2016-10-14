using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Transformation3D
{
    public class Utility
    {
        public Utility()
        {
        }

        public static Matrix3D Matrix3DRound(Matrix3D m, int n)
        {
            m.M11 = Math.Round(m.M11, n);
            m.M12 = Math.Round(m.M12, n);
            m.M13 = Math.Round(m.M13, n);
            m.M14 = Math.Round(m.M14, n);
            m.M21 = Math.Round(m.M21, n);
            m.M22 = Math.Round(m.M22, n);
            m.M23 = Math.Round(m.M23, n);
            m.M24 = Math.Round(m.M24, n);
            m.M31 = Math.Round(m.M31, n);
            m.M32 = Math.Round(m.M32, n);
            m.M33 = Math.Round(m.M33, n);
            m.M34 = Math.Round(m.M34, n);
            m.OffsetX = Math.Round(m.OffsetX, n);
            m.OffsetY = Math.Round(m.OffsetY, n);
            m.OffsetZ = Math.Round(m.OffsetZ, n);
            m.M44 = Math.Round(m.M44, n);
            return m;
        }

        public ModelVisual3D CreateCube(Point3D center, double side)
        {
            Model3DGroup cube = new Model3DGroup();

            double a = side / 2.0;
            Point3D[] p = new Point3D[8];
            p[0] = new Point3D(-a, -a, -a);
            p[1] = new Point3D( a, -a, -a);
            p[2] = new Point3D( a, -a,  a);
            p[3] = new Point3D(-a, -a,  a);
            p[4] = new Point3D(-a,  a, -a);
            p[5] = new Point3D( a,  a, -a);
            p[6] = new Point3D( a,  a,  a);
            p[7] = new Point3D(-a,  a,  a);
            
            // Redefine the center of the cube:
            for (int i = 0; i < 8; i++)
                p[i] += (Vector3D)center;

            // Front side:
            cube.Children.Add(CreateTriangleModel(p[3], p[2], p[6], Colors.Red));
            cube.Children.Add(CreateTriangleModel(p[3], p[6], p[7], Colors.Red));
            
            // Right side:
            cube.Children.Add(CreateTriangleModel(p[2], p[1], p[5], Colors.Green));
            cube.Children.Add(CreateTriangleModel(p[2], p[5], p[6], Colors.Green));
            
            // Back side:
            cube.Children.Add(CreateTriangleModel(p[1], p[0], p[4], Colors.Blue));
            cube.Children.Add(CreateTriangleModel(p[1], p[4], p[5], Colors.Blue));
            
            // Left side:
            cube.Children.Add(CreateTriangleModel(p[0], p[3], p[7], Colors.LightCoral));
            cube.Children.Add(CreateTriangleModel(p[0], p[7], p[4], Colors.LightCoral));
            
            // Top side:
            cube.Children.Add(CreateTriangleModel(p[7], p[6], p[5], Colors.LightGray));
            cube.Children.Add(CreateTriangleModel(p[7], p[5], p[4], Colors.LightGray));
            
            // Bottom side:
            cube.Children.Add(CreateTriangleModel(p[2], p[3], p[0], Colors.Black));
            cube.Children.Add(CreateTriangleModel(p[2], p[0], p[1], Colors.Black));

            ModelVisual3D modelVisual3D = new ModelVisual3D();
            modelVisual3D.Content = cube;
            return modelVisual3D;
        }

        private Model3DGroup CreateTriangleModel(Point3D p0, 
            Point3D p1, Point3D p2, Color color)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = color;
            brush.Opacity = 0.5;
            Material material = new DiffuseMaterial(brush);
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }

        public static Matrix3D SetViewMatrix(Point3D cameraPosition, 
            Vector3D lookDirection, Vector3D upDirection)
        {
            // Normalize vectors:
            lookDirection.Normalize();
            upDirection.Normalize();

            // Define vectors, XScale, YScale, and ZScale:
            double denom = Math.Sqrt(1 -
                Math.Pow(Vector3D.DotProduct(lookDirection, upDirection), 2));
            Vector3D XScale = Vector3D.CrossProduct(lookDirection, upDirection) / denom;
            Vector3D YScale = (upDirection - (Vector3D.DotProduct(upDirection,
                lookDirection)) * lookDirection) / denom;
            Vector3D ZScale = lookDirection;

            // Construct M matrix:
            Matrix3D M = new Matrix3D();
            M.M11 = XScale.X;
            M.M21 = XScale.Y;
            M.M31 = XScale.Z;
            M.M12 = YScale.X;
            M.M22 = YScale.Y;
            M.M32 = YScale.Z;
            M.M13 = ZScale.X;
            M.M23 = ZScale.Y;
            M.M33 = ZScale.Z;

            // Translate the camera position to the origin:
            Matrix3D translateMatrix = new Matrix3D();
            translateMatrix.Translate(new Vector3D(-cameraPosition.X,
                -cameraPosition.Y, -cameraPosition.Z));

            // Define reflect matrix about the Z axis:
            Matrix3D reflectMatrix = new Matrix3D();
            reflectMatrix.M33 = -1;

            // Construct the View matrix:
            Matrix3D viewMatrix = translateMatrix * M * reflectMatrix;
            return viewMatrix;           
        }

        public static Matrix3D SetPerspectiveOffCenter(double left,
           double right, double bottom, double top, double near, double far)
        {
            Matrix3D perspectiveMatrix = new Matrix3D();
            perspectiveMatrix.M11 = 2 * near / (right - left);
            perspectiveMatrix.M22 = 2 * near / (top - bottom);
            perspectiveMatrix.M31 = (right + left) / (right - left);
            perspectiveMatrix.M32 = (top + bottom) / (top - bottom);
            perspectiveMatrix.M33 = far / (near - far);
            perspectiveMatrix.M34 = -1.0;
            perspectiveMatrix.OffsetZ = near * far / (near - far);
            perspectiveMatrix.M44 = 0;
            return perspectiveMatrix;
        }

        public static Matrix3D SetPerspective(double width, double height, 
            double near, double far)
        {
            Matrix3D perspectiveMatrix = new Matrix3D();
            perspectiveMatrix.M11 = 2 * near / width;
            perspectiveMatrix.M22 = 2 * near / height;
            perspectiveMatrix.M33 = far / (near - far);
            perspectiveMatrix.M34 = -1.0;
            perspectiveMatrix.OffsetZ = near * far / (near - far);
            perspectiveMatrix.M44 = 0;
            return perspectiveMatrix;
        }

        public static Matrix3D SetPerspectiveFov(double fov,
            double aspectRatio, double near, double far)
        {
            Matrix3D perspectiveMatrix = new Matrix3D();
            double yscale = 1.0 / Math.Tan(fov * Math.PI / 180 / 2);
            double xscale = yscale / aspectRatio;
            perspectiveMatrix.M11 = xscale;
            perspectiveMatrix.M22 = yscale;
            perspectiveMatrix.M33 = far / (near - far);
            perspectiveMatrix.M34 = -1.0;
            perspectiveMatrix.OffsetZ = near * far / (near - far);
            perspectiveMatrix.M44 = 0;
            return perspectiveMatrix;
        }

        public static Matrix3D SetOrthographicOffCenter(double left, double right,
           double bottom, double top, double near, double far)
        {
            Matrix3D orthographicMatrix = new Matrix3D();
            orthographicMatrix.M11 = 2 / (right - left);
            orthographicMatrix.M22 = 2 / (top - bottom);
            orthographicMatrix.M33 = 1 / (near - far);
            orthographicMatrix.OffsetX = (left + right) / (left - right);
            orthographicMatrix.OffsetY = (bottom + top) / (bottom - top);
            orthographicMatrix.OffsetZ = near / (near - far);
            return orthographicMatrix;
        }

        public static Matrix3D SetOrthographic(double width, double height,
           double near, double far)
        {
            Matrix3D orthographicMatrix = new Matrix3D();
            orthographicMatrix.M11 = 2 / width;
            orthographicMatrix.M22 = 2 / height;
            orthographicMatrix.M33 = 1 / (near - far);
            orthographicMatrix.OffsetZ = near / (near - far);
            return orthographicMatrix;
        }
    }
}
