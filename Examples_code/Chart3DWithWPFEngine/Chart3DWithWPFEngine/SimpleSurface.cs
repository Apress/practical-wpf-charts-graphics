using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;

namespace Chart3DWithWPFEngine
{
    public class SimpleSurface
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
        private bool isWireframe = true;
        private Viewport3D viewport3d = new Viewport3D();


        public bool IsWireframe
        {
            get { return isWireframe; }
            set { isWireframe = value; }
        }

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

        public Viewport3D Viewport3d
        {
            get { return viewport3d; }
            set { viewport3d = value; }
        }

        public void CreateSurface(Function f)
        {
            
            double dx = (Xmax - Xmin) / Nx;
            double dz = (Zmax - Zmin) / Nz;
            if (Nx < 2 || Nz < 2)
                return;

            Point3D[,] pts = new Point3D[Nx, Nz];
            for (int i = 0; i < Nx; i++)
            {
                double x = Xmin + i * dx;
                for (int j = 0; j < Nz; j++)
                {
                    double z = Zmin + j * dz;
                    pts[i, j] = f(x, z);
                    pts[i, j] += (Vector3D)Center;
                    pts[i, j] = Utility.GetNormalize(pts[i, j], Xmin, Xmax, Ymin, Ymax, Zmin, Zmax);
                }
            }

            Point3D[] p = new Point3D[4];
            for (int i = 0; i < Nx - 1; i++)
            {
                for (int j = 0; j < Nz - 1; j++)
                {
                    p[0] = pts[i, j];
                    p[1] = pts[i + 1, j];
                    p[2] = pts[i + 1, j + 1];
                    p[3] = pts[i, j + 1];

                    //Create rectangular face:
                    if (IsHiddenLine == false)
                        Utility.CreateRectangleFace(p[0], p[1], p[2], p[3], SurfaceColor, Viewport3d);

                    // Create wireframe:
                    if (IsWireframe == true)
                        Utility.CreateWireframe(p[0], p[1], p[2], p[3], LineColor, Viewport3d);
                }
            }
        }
    }
}
