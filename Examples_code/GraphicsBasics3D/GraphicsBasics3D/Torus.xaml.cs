using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphicsBasics3D
{
    public partial class Torus : Window
    {
        public Torus()
        {
            InitializeComponent();

            // Create a torus:
            CreateTorus(new Point3D(0, 0, 0), 1, 0.3, 20, 15, Colors.LightBlue, true);

            // Create another torus:
            CreateTorus(new Point3D(0, 0, -3), 0.5, 0.5, 20, 15, Colors.LightCoral, false);

            // Create another torus:
            CreateTorus(new Point3D(0, 0, -6), 0.3, 0.5, 20, 15, Colors.LightGreen, true);

            // Create another torus:
            CreateTorus(new Point3D(-3, 0, 0), 0.0, 0.8, 20, 25, Colors.SteelBlue, false);

            // Create another torus:
            CreateTorus(new Point3D(3, 0, 0), 0.0, 0.8, 20, 25, Colors.Goldenrod, false);
        }

        private Point3D GetPosition(double R, double r, double u, double v)
        {
            Point3D pt = new Point3D();
            double snu = Math.Sin(u * Math.PI / 180);
            double cnu = Math.Cos(u * Math.PI / 180);
            double snv = Math.Sin(v * Math.PI / 180);
            double cnv = Math.Cos(v * Math.PI / 180);

            pt.X = (R + r * cnv) * cnu;
            pt.Y = r * snv;
            pt.Z = -(R + r * cnv) * snu;
            return pt;
        }

        private void CreateTorus(Point3D center, double R, double r, int N, int n, Color color, bool isWireframe)
        {
            if (n < 2 || N < 2)
                return;

            Model3DGroup torus = new Model3DGroup();
            Point3D[,] pts = new Point3D[N, n];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pts[i, j] = GetPosition(R, r, i * 360 / (N - 1), j * 360 / (n - 1));
                    pts[i, j] += (Vector3D)center;
                }
            }

            Point3D[] p = new Point3D[4];
            for (int i = 0; i < N - 1; i++)
            {
                for (int j = 0; j < n - 1; j++)
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
