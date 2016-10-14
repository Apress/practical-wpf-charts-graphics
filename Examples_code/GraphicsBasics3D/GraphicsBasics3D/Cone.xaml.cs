using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphicsBasics3D
{
    public partial class Cone : Window
    {
        public Cone()
        {
            InitializeComponent();

            // Create a cone:
            CreateCone(new Point3D(0, 0, 0), 0, 1.2, 2, 20, Colors.LightBlue, true);

            // Create another cone:
            CreateCone(new Point3D(0, 0, -3), 0.6, 1.2, 1, 20, Colors.LightCoral, false);

            // Create another cone:
            CreateCone(new Point3D(0, 0, -6), 1.2, 0.4, 2, 20, Colors.LightGreen, false);

            // Create another cone:
            CreateCone(new Point3D(0, 0, 3), 0.5, 1.2, 1.2, 4, Colors.Goldenrod, false);

            // Create another cone:
            CreateCone(new Point3D(-3, 0, 0), 0.8, 0.8, 1.5, 20, Colors.Red, true);

            // Create another cone:
            CreateCone(new Point3D(3, 0, 0), 0, 0.8, 1.5, 5, Colors.SteelBlue, false);
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

        private void CreateCone(Point3D center, double rtop, double rbottom, 
                                double height, int n, Color color, bool isWireframe)
        {
            if (n < 2)
                return;

            double h = height / 2;
            Model3DGroup cone = new Model3DGroup();
            Point3D[,] pts = new Point3D[n + 1, 4];


            for (int i = 0; i < n + 1; i++)
            {
                pts[i, 0] = GetPosition(rtop, i * 360 / (n - 1), h);
                pts[i, 1] = GetPosition(rbottom, i * 360 / (n - 1), -h);
                pts[i, 2] = GetPosition(0, i * 360 / (n - 1), -h);
                pts[i, 3] = GetPosition(0, i * 360 / (n - 1), h);
            }
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < 4; j++)
                    pts[i, j] += (Vector3D)center;
            }

            Point3D[] p = new Point3D[6];
            for (int i = 0; i < n; i++)
            {
                p[0] = pts[i, 0];
                p[1] = pts[i, 1];
                p[2] = pts[i, 2];
                p[3] = pts[i, 3];
                p[4] = pts[i + 1, 0];
                p[5] = pts[i + 1, 1];
                
                // Top surface:
                Utility.CreateTriangleFace(p[0], p[4], p[3], color, isWireframe, myViewport);

                // Bottom surface:
                Utility.CreateTriangleFace(p[1], p[5], p[2], color, isWireframe, myViewport);

                // Side surface:
                Utility.CreateTriangleFace(p[0], p[1], p[5], color, isWireframe, myViewport);
                Utility.CreateTriangleFace(p[0], p[5], p[4], color, isWireframe, myViewport);
            }
        }
    }
}
