using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using _3DTools;

namespace Chart3DWithWPFEngine
{
    class BezierSurface
    {
        public delegate Point3D Function(double x, double z);

        private double xmin = -3;
        private double xmax = 3;
        private double ymin = -8;
        private double ymax = 8;
        private double zmin = -3;
        private double zmax = 3;
        private int nx = 30;
        private int nz = 30;
        private Color lineColor = Colors.Black;
        private Color surfaceColor = Colors.White;
        private Point3D center = new Point3D();
        private bool isHiddenLine = false;
        private Viewport3D viewport3d = new Viewport3D();

        public bool IsHiddenLine
        {
            get { return isHiddenLine; }
            set { isHiddenLine = value; }
        }

        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        public Color SurfaceColor
        {
            get { return surfaceColor; }
            set { surfaceColor = value; }
        }

        public double Xmin
        {
            get { return xmin; }
            set { xmin = value; }
        }

        public double Xmax
        {
            get { return xmax; }
            set { xmax = value; }
        }

        public double Ymin
        {
            get { return ymin; }
            set { ymin = value; }
        }

        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }

        public double Zmin
        {
            get { return zmin; }
            set { zmin = value; }
        }

        public double Zmax
        {
            get { return zmax; }
            set { zmax = value; }
        }

        public int Nx
        {
            get { return nx; }
            set { nx = value; }
        }

        public int Nz
        {
            get { return nz; }
            set { nz = value; }
        }

        public Point3D Center
        {
            get { return center; }
            set { center = value; }
        }

        public Viewport3D ViewPort3d
        {
            get { return viewport3d; }
            set { viewport3d = value; }
        }

        private Point3D GetNormalize(Point3D pt)
        {
            pt.X = -1 + 2 * (pt.X - Xmin) / (Xmax - Xmin);
            pt.Y = -1 + 2 * (pt.Y - Ymin) / (Ymax - Ymin);
            pt.Z = -1 + 2 * (pt.Z - Zmin) / (Zmax - Zmin);
            return pt;
        }

        public ModelVisual3D CreateSurface(Function f)
        {

            double dx = (Xmax - Xmin) / Nx;
            double dz = (Zmax - Zmin) / Nz;
            if (Nx < 2 || Nz < 2)
                return null;

            Model3DGroup surface = new Model3DGroup();

            Point3D[,] pts = new Point3D[Nx, Nz];
            for (int i = 0; i < Nx; i++)
            {
                double x = Xmin + i * dx;
                for (int j = 0; j < Nz; j++)
                {
                    double z = Zmin + j * dz;
                    pts[i, j] = GetNormalize(f(x, z));
                    pts[i, j] += (Vector3D)Center;
                }
            }

            Point3D[] p = new Point3D[4];
            for (int i = 0; i < Nx - 1; i++)
            {
                for (int j = 0; j < Nz - 1; j++)
                {
                    p[0] = pts[i, j];
                    p[1] = pts[i, j + 1];
                    p[2] = pts[i + 1, j + 1];
                    p[3] = pts[i + 1, j];

                    surface.Children.Add(CreateTriangleModel(p[0], p[1], p[2]));
                    surface.Children.Add(CreateTriangleModel(p[2], p[3], p[0]));

                    ScreenSpaceLines3D ssl = new ScreenSpaceLines3D();
                    ssl.Points.Add(p[0]);
                    ssl.Points.Add(p[1]);
                    ssl.Points.Add(p[1]);
                    ssl.Points.Add(p[2]);
                    ssl.Points.Add(p[2]);
                    ssl.Points.Add(p[3]);
                    ssl.Points.Add(p[3]);
                    ssl.Points.Add(p[0]);
                    ssl.Color = LineColor;
                    ssl.Thickness = 2;
                    ViewPort3d.Children.Add(ssl);
                }
            }

            ModelVisual3D modelVisual3D = new ModelVisual3D();
            modelVisual3D.Content = surface;
            return modelVisual3D;
        }

        private Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = SurfaceColor;
            if (IsHiddenLine == true)
                brush.Color = Colors.Transparent;

            Material material = new DiffuseMaterial(brush);
            GeometryModel3D model = new GeometryModel3D(mesh, material);

            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
    }
}
