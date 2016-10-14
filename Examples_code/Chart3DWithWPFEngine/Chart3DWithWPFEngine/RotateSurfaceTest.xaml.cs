using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Chart3DWithWPFEngine
{
    /// <summary>
    /// Interaction logic for RotateSurfaceTest.xaml
    /// </summary>
    public partial class RotateSurfaceTest : Window
    {
        private RorateSurface rs = new RorateSurface();

        public RotateSurfaceTest()
        {
            InitializeComponent();

            rs.Viewport3d = viewport;
            rs.IsHiddenLine = false;
            //AddRotateSurface();
            //AddSphereSurface();
            AddTorusSurface();
        }

        // Rotated surface:
        private void AddRotateSurface()
        {  
            for (int i = 0; i < 33; i++)
            {
                double y = i * Math.PI / 12;
                double siny = Math.Sin(y);
                rs.CurvePoints.Add(new Point3D(0.2 + siny * siny, y, 0));
            }
            rs.Xmin = -3;
            rs.Xmax = 3;
            rs.Ymin = 5;
            rs.Ymax = 15;
            rs.Zmin = -3;
            rs.Zmax = 3;
            rs.CreateSurface();
        } 

        // Sphere surface:
        private void AddSphereSurface()
        {
            for (int i = 0; i < 11; i++)
            {
                double theta = -Math.PI / 2 + i * Math.PI / 10;
                rs.CurvePoints.Add(new Point3D(Math.Cos(theta), Math.Sin(theta), 0));
            }
            rs.Xmin = -2;
            rs.Xmax = 2;
            rs.Ymin = 0;
            rs.Ymax = 4;
            rs.Zmin = -2;
            rs.Zmax = 2;
            rs.CreateSurface();
        }

        // Torus surface:
        private void AddTorusSurface()
        {
            for (int i = 0; i < 21; i++)
            {
                double theta = i * Math.PI / 10;
                Point3D pt = new Point3D(0.3 * Math.Cos(theta), 0.3 * Math.Sin(theta), 0);
                pt += new Vector3D(1, 0, 0);
                rs.CurvePoints.Add(pt);
            }
            rs.Xmin = -2;
            rs.Xmax = 2;
            rs.Ymin = 0;
            rs.Ymax = 4;
            rs.Zmin = -2;
            rs.Zmax = 2;
            rs.CreateSurface();
        } 
    }
}
