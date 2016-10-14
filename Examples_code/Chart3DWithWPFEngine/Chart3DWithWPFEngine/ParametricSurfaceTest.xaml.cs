using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Chart3DWithWPFEngine
{
    public partial class ParametricSurfaceTest : Window
    {
        private ParametricSurface ps = new ParametricSurface();

        public ParametricSurfaceTest()
        {
            InitializeComponent();
            ps.Viewport3d = viewport;
            AddHelicoid();
            //AddSphere();
            //AddTorus();
            //AddHyperboloid();
            //AddParaboloid();
            //AddEllipticCone();
            //AddEllipticCylinder();
        }

        // Helicoid surface:
        private void AddHelicoid()
        {
            ps.Umin = 0;
            ps.Umax = 1;
            ps.Vmin = -3 * Math.PI;
            ps.Vmax = 3 * Math.PI;
            ps.Nv = 100;
            ps.Nu = 10;
            ps.Ymin = ps.Vmin;
            ps.Ymax = ps.Vmax;
            ps.CreateSurface(Helicoid);
        }
        private Point3D Helicoid(double u, double v)
        {
            double x = u * Math.Cos(v);
            double z = u * Math.Sin(v);
            double y = v;
            return new Point3D(x, y, z);
        }

        // Sphere surface:
        private void AddSphere()
        {
            ps.Umin = 0;
            ps.Umax = 2 * Math.PI;
            ps.Vmin = -0.5 * Math.PI;
            ps.Vmax = 0.5 * Math.PI;
            ps.Nu = 20;
            ps.Nv = 20;
            ps.CreateSurface(Sphere);
        }

        private Point3D Sphere(double u, double v)
        {
            double x = Math.Cos(v) * Math.Cos(u);
            double z = Math.Cos(v) * Math.Sin(u);
            double y = Math.Sin(v);
            return new Point3D(x, y, z);
        }

        // Torus surface:
        private void AddTorus()
        {
            ps.Umin = 0;
            ps.Umax = 2 * Math.PI;
            ps.Vmin = 0;
            ps.Vmax = 2 * Math.PI;
            ps.Nu = 50;
            ps.Nv = 20;
            ps.CreateSurface(Torus);
        }
        private Point3D Torus(double u, double v)
        {
            double x = (1 + 0.3 * Math.Cos(v)) * Math.Cos(u);
            double z = (1 + 0.3 * Math.Cos(v)) * Math.Sin(u);
            double y = 0.3 * Math.Sin(v);
            return new Point3D(x, y, z);
        }

        // Hyperboloid surface:
        private void AddHyperboloid()
        {
            ps.Umin = 0;
            ps.Umax = 2 * Math.PI;
            ps.Vmin = -1;
            ps.Vmax = 1;
            ps.Nu = 20;
            ps.Nv = 10;
            ps.CreateSurface(Hyperboloid);
        }
        private Point3D Hyperboloid(double u, double v)
        {
            double x = 0.3 * Math.Cos(u) * Math.Cosh(v);
            double z = 0.5 * Math.Sin(u) * Math.Cosh(v);
            double y = 0.5 * Math.Sinh(v);
            return new Point3D(x, y, z);
        }

        // Paraboloid surface:
        private void AddParaboloid()
        {
            ps.Umin = -2;
            ps.Umax = 2;
            ps.Vmin = -2;
            ps.Vmax = 2;
            ps.Nu = 20;
            ps.Nv = 40;
            ps.Xmin = -5;
            ps.Xmax = 5;
            ps.Ymin = 0;
            ps.Ymax = 5;
            ps.Zmin = -5;
            ps.Zmax = 5;
            ps.CreateSurface(Paraboloid);
        }
        private Point3D Paraboloid(double u, double v)
        {
            double x = 0.8 * v * Math.Cosh(u);
            double z = 0.5 * v * Math.Sinh(u);
            double y = v * v;
            return new Point3D(x, y, z);
        }

        //Elliptic cone:
        private void AddEllipticCone()
        {
            ps.Umin = 0;
            ps.Umax = 2 * Math.PI;
            ps.Vmin = -1;
            ps.Vmax = 1;
            ps.Nu = 20;
            ps.Nv = 20;
            ps.CreateSurface(EllipticCone);
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
            ps.Umin = 0;
            ps.Umax = 2 * Math.PI;
            ps.Vmin = -0.5;
            ps.Vmax = 0.5;
            ps.Nu = 40;
            ps.Nv = 10;
            ps.CreateSurface(EllipticCylinder);
        }
        private Point3D EllipticCylinder(double u, double v)
        {
            double x = 1.2 * Math.Cos(u);
            double z = 0.8 * Math.Sin(u);
            double y = v;
            return new Point3D(x, y, z);
        }
    }
}
