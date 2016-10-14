using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphicsBasics3D
{
    public partial class Cube : Window
    {
        public Cube()
        {
            InitializeComponent();

            // Create a cube:
            CreateCube(new Point3D(0, 0, 0), 2, true);

            // Create another cube:
            CreateCube(new Point3D(0, 0, -4), 2, false);
        }

        public void CreateCube(Point3D center, double side, bool isWireframe)
        {
            double a = side / 2.0;
            Point3D[] p = new Point3D[8];
            p[0] = new Point3D(-a,  a,  a);
            p[1] = new Point3D( a,  a,  a);
            p[2] = new Point3D( a,  a, -a);
            p[3] = new Point3D(-a,  a, -a);
            p[4] = new Point3D(-a, -a,  a);
            p[5] = new Point3D( a, -a,  a);
            p[6] = new Point3D( a, -a, -a);
            p[7] = new Point3D(-a, -a, -a);

            // Redefine the center of the cube:
            for (int i = 0; i < 8; i++)
                p[i] += (Vector3D)center;

            // Surface 1 (0,1,2,3):
            Utility.CreateTriangleFace(p[0], p[1], p[2], Colors.LightGray, isWireframe, myViewport);
            Utility.CreateTriangleFace(p[2], p[3], p[0], Colors.LightGray, isWireframe, myViewport);

            // Surface 2 (4,7,6,5):
            Utility.CreateTriangleFace(p[4], p[7], p[6], Colors.Black, isWireframe, myViewport);
            Utility.CreateTriangleFace(p[6], p[5], p[4], Colors.Black, isWireframe, myViewport);

            // Surface 3 (0,4,5,1):
            Utility.CreateTriangleFace(p[0], p[4], p[5], Colors.LightCoral, isWireframe, myViewport);
            Utility.CreateTriangleFace(p[5], p[1], p[0], Colors.LightCoral, isWireframe, myViewport);

            // Surface 4 (1,5,6,2):
            Utility.CreateTriangleFace(p[1], p[5], p[6], Colors.LightGreen, isWireframe, myViewport);
            Utility.CreateTriangleFace(p[6], p[2], p[1], Colors.LightGreen, isWireframe, myViewport);

            // Surface 5 (2,6,7,3):
            Utility.CreateTriangleFace(p[2], p[6], p[7], Colors.Blue, isWireframe, myViewport);
            Utility.CreateTriangleFace(p[7], p[3], p[2], Colors.Blue, isWireframe, myViewport);

            // Surface 6 (0,3,7,4):
            Utility.CreateTriangleFace(p[0], p[3], p[7], Colors.Black, isWireframe, myViewport);
            Utility.CreateTriangleFace(p[7], p[4], p[0], Colors.Black, isWireframe, myViewport);
        }
    }
}
