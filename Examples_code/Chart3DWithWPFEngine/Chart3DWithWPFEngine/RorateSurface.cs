using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using _3DTools;

namespace Chart3DWithWPFEngine
{
    public class RorateSurface
    {
        private List<Point3D> curvePoints = new List<Point3D>();
        private int thetaDiv = 20;
        private double xmin = -1;
        private double xmax = 1;
        private double ymin = -1;
        private double ymax = 1;
        private double zmin = -1;
        private double zmax = 1;
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
        
        public int ThetaDiv
        {
            get { return thetaDiv; }
            set { thetaDiv = value; }
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

        public List<Point3D> CurvePoints
        {
            get { return curvePoints; }
            set { curvePoints = value; }
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

        private Point3D GetPosition(double r, double y, double theta)
        {
            double x = r * Math.Cos(theta);
            double z = -r * Math.Sin(theta);
            return new Point3D(x, y, z);
        }

        public void CreateSurface()
        {
            // create all points used to create rotated surface (around the Y axis):
            Point3D[,] pts = new Point3D[ThetaDiv, CurvePoints.Count];
            for (int i = 0; i < ThetaDiv; i++)
            {
                double theta = i * 2 * Math.PI / (ThetaDiv - 1);
                for (int j = 0; j < CurvePoints.Count; j++)
                {
                    double x = CurvePoints[j].X;
                    double z = CurvePoints[j].Z;
                    double r = Math.Sqrt(x * x + z * z);

                    pts[i, j] = GetPosition(r, CurvePoints[j].Y, theta);
                    pts[i, j] += (Vector3D)Center;
                    pts[i, j] = Utility.GetNormalize(pts[i, j], Xmin, Xmax, Ymin, Ymax, Zmin, Zmax);                   
                }
            }

            Point3D[] p = new Point3D[4];
            for (int i = 0; i < ThetaDiv - 1; i++)
            {
                for (int j = 0; j < CurvePoints.Count - 1; j++)
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
