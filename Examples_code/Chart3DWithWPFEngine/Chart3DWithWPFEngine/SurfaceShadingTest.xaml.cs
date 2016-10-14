using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;


namespace Chart3DWithWPFEngine
{
    public partial class SurfaceShadingTest : Window
    {
        private SurfaceShading ss;

        public SurfaceShadingTest()
        {
            InitializeComponent();
            AddHyperboloid();
            AddEllipticCone();
            AddEllipticCylinder();
        }

        // Hyperboloid surface:
        private void AddHyperboloid()
        {
            ss = new SurfaceShading();
           
            Material material = new DiffuseMaterial(Brushes.Red);
            ss.MaterialGroup.Children.Add(material);
            material = new SpecularMaterial(Brushes.Yellow, 60);
            ss.MaterialGroup.Children.Add(material);
            material = new DiffuseMaterial(Brushes.SteelBlue);
            ss.BackMaterial = material;

            ss.Viewport3d = viewport;
            ss.Center = new Point3D(0, 0, 2);
            ss.Umin = 0;
            ss.Umax = 2 * Math.PI;
            ss.Vmin = -1;
            ss.Vmax = 1;
            ss.Nu = 30;
            ss.Nv = 30;
            ss.CreateSurface(Hyperboloid);
        }
        private Point3D Hyperboloid(double u, double v)
        {
            double x = 0.3 * Math.Cos(u) * Math.Cosh(v);
            double z = 0.5 * Math.Sin(u) * Math.Cosh(v);
            double y = Math.Sinh(v);
            return new Point3D(x, y, z);
        }

        //Elliptic cone:
        private void AddEllipticCone()
        {
            ss = new SurfaceShading();

            Material material = new DiffuseMaterial(Brushes.Green);
            ss.MaterialGroup.Children.Add(material);
            material = new SpecularMaterial(Brushes.LightGreen, 60);
            ss.MaterialGroup.Children.Add(material);
            material = new DiffuseMaterial(Brushes.SteelBlue);
            ss.BackMaterial = material;

            ss.Viewport3d = viewport;
            ss.Center = new Point3D(0, 0, 0);
            ss.Umin = 0;
            ss.Umax = 2 * Math.PI;
            ss.Vmin = -1;
            ss.Vmax = 1;
            ss.Nu = 30;
            ss.Nv = 30;
            ss.CreateSurface(EllipticCone);
        }
        private Point3D EllipticCone(double u, double v)
        {
            double x = 1.2 * v * Math.Cos(u);
            double z = 0.8 * v * Math.Sin(u);
            double y = 0.9 * v;
            return new Point3D(x, y, z);
        }

        //Elliptic cylinder:
        private void AddEllipticCylinder()
        {
            ss = new SurfaceShading();

            Material material = new DiffuseMaterial(Brushes.Goldenrod);
            ss.MaterialGroup.Children.Add(material);
            material = new SpecularMaterial(Brushes.LightGoldenrodYellow, 60);
            ss.MaterialGroup.Children.Add(material);
            material = new DiffuseMaterial(Brushes.SteelBlue);
            ss.BackMaterial = material;

            ss.Viewport3d = viewport;
            ss.Center = new Point3D(0, 1, -2);
            ss.Umin = 0;
            ss.Umax = 2 * Math.PI;
            ss.Vmin = -0.5;
            ss.Vmax = 0.5;
            ss.Nu = 40;
            ss.Nv = 10;
            ss.CreateSurface(EllipticCylinder);
        }
        private Point3D EllipticCylinder(double u, double v)
        {
            ss.Viewport3d = viewport;
            double x = 1.2 * Math.Cos(u);
            double z = 0.8 * Math.Sin(u);
            double y = 2 * v;
            return new Point3D(x, y, z);
        }
    }
}
