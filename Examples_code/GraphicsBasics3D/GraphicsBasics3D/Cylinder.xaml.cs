using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphicsBasics3D
{
    /// <summary>
    /// Interaction logic for Cylinder.xaml
    /// </summary>
    public partial class Cylinder : Window
    {
        public Cylinder()
        {
            InitializeComponent();
            //AddCylinder();

            // Create a cylinder:
            CreateCylinder(new Point3D(0, 0, 0), 0, 1.2, 2, 20, Colors.LightBlue, true);

            // Create another cylinder:
            CreateCylinder(new Point3D(0, 0, -4), 0.8, 1.2, 0.5, 20, Colors.LightCoral, true);

            // Create another cylinder:
            CreateCylinder(new Point3D(-3, 0, 0), 1, 1.2, 0.5, 40, Colors.Red, false);
        }

        private Point3D GetPosition(double radius, double theta, double y)
        {
            Point3D pt = new Point3D();
            double sn = Math.Sin(theta * Math.PI / 180);
            double cn = Math.Cos(theta * Math.PI / 180);

            pt.X = radius * cn;
            pt.Y = y;
            pt.Z = -radius * sn;
            return pt;
        }

        private void CreateCylinder(Point3D center, double rin, double rout, double height, int n, 
            Color color, bool isWireframe)
        {
            if (n < 2 || rin == rout)
                return;

            double radius = rin;
            if (rin > rout)
            {
                rin = rout;
                rout = radius;
            }

            double h = height / 2;
            Model3DGroup cylinder = new Model3DGroup();
            Point3D[,] pts = new Point3D[n, 4];


            for (int i = 0; i < n; i++)
            {
                pts[i, 0] = GetPosition(rout, i * 360 / (n - 1), h);
                pts[i, 1] = GetPosition(rout, i * 360 / (n - 1), -h);
                pts[i, 2] = GetPosition(rin, i * 360 / (n - 1), -h);
                pts[i, 3] = GetPosition(rin, i * 360 / (n - 1), h);
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 4; j++)
                    pts[i, j] += (Vector3D)center;
            }

            Point3D[] p = new Point3D[8];
            for (int i = 0; i < n - 1; i++)
            {
                p[0] = pts[i, 0];
                p[1] = pts[i, 1];
                p[2] = pts[i, 2];
                p[3] = pts[i, 3];
                p[4] = pts[i + 1, 0];
                p[5] = pts[i + 1, 1];
                p[6] = pts[i + 1, 2];
                p[7] = pts[i + 1, 3];

                // Top surface:
                Utility.CreateTriangleFace(p[0], p[4], p[3], color, isWireframe, myViewport);
                Utility.CreateTriangleFace(p[4], p[7], p[3], color, isWireframe, myViewport);

                // Bottom surface:
                Utility.CreateTriangleFace(p[1], p[5], p[2], color, isWireframe, myViewport);
                Utility.CreateTriangleFace(p[5], p[6], p[2], color, isWireframe, myViewport);

                // Outer surface:
                Utility.CreateTriangleFace(p[0], p[1], p[4], color, isWireframe, myViewport);
                Utility.CreateTriangleFace(p[1], p[5], p[4], color, isWireframe, myViewport);

                // Outer surface:
                Utility.CreateTriangleFace(p[2], p[7], p[6], color, isWireframe, myViewport);
                Utility.CreateTriangleFace(p[2], p[3], p[7], color, isWireframe, myViewport);
            }
        }
    }
}
