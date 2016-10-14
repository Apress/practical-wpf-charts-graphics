using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphicsBasics3D
{
    public partial class Sphere : Window
    {
        public Sphere()
        {
            InitializeComponent();

            // Add a sphere:
            CreateSphere(new Point3D(0, 0, 0), 1.5, 20, 15, Colors.LightBlue, true);

            // Add another sphere:
            CreateSphere(new Point3D(0, 0, -4), 1.5, 20, 15, Colors.LightCoral, false);
        }

        private Point3D GetPosition(double radius, double theta, double phi)
        {
            Point3D pt = new Point3D();
            double snt = Math.Sin(theta * Math.PI / 180);
            double cnt = Math.Cos(theta * Math.PI / 180);
            double snp = Math.Sin(phi * Math.PI / 180);
            double cnp = Math.Cos(phi * Math.PI / 180);

            pt.X = radius * snt * cnp;
            pt.Y = radius * cnt;
            pt.Z = -radius * snt * snp;
            return pt;
        }

        private void CreateSphere(Point3D center, double radius, int u, int v, Color color, bool isWireframe)
        {
            if (u < 2 || v < 2)
                return;
           
            Point3D[,] pts = new Point3D[u, v];
            for (int i = 0; i < u; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    pts[i, j] = GetPosition(radius, i * 180 / (u - 1), j * 360 / (v - 1));
                    pts[i, j] += (Vector3D)center;
                }
            }

            Point3D[] p = new Point3D[4];
            for (int i = 0; i < u - 1; i++)
            {
                for (int j = 0; j < v - 1; j++)
                {
                    p[0] = pts[i, j];
                    p[1] = pts[i + 1, j];
                    p[2] = pts[i + 1, j + 1];
                    p[3] = pts[i, j + 1];
                    Utility.CreateTriangleFace(p[0], p[1], p[2], color, isWireframe, myViewport);
                    Utility.CreateTriangleFace(p[2], p[3], p[0], color, isWireframe, myViewport);
                }
            }
        }
    }
}
