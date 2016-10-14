using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using _3DTools;

namespace Chart3DWithWPFEngine
{
    public class Utility
    {
        public static void CreateRectangleFace(Point3D p0, Point3D p1, Point3D p2, Point3D p3, 
            Color surfaceColor, Viewport3D viewport)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);

            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = surfaceColor;
            Material material = new DiffuseMaterial(brush);
            GeometryModel3D geometry = new GeometryModel3D(mesh, material);
            ModelVisual3D model = new ModelVisual3D();
            model.Content = geometry;
            viewport.Children.Add(model);
        }

        public static void CreateWireframe(Point3D p0, Point3D p1, Point3D p2, Point3D p3, 
            Color lineColor, Viewport3D viewport)
        {
            ScreenSpaceLines3D ssl = new ScreenSpaceLines3D();

            ssl.Points.Add(p0);
            ssl.Points.Add(p1);
            ssl.Points.Add(p1);
            ssl.Points.Add(p2);
            ssl.Points.Add(p2);
            ssl.Points.Add(p3);
            ssl.Points.Add(p3);
            ssl.Points.Add(p0);
            ssl.Color = lineColor;
            ssl.Thickness = 2;
            viewport.Children.Add(ssl);
        }

        public static Point3D GetNormalize(Point3D pt, double xmin, double xmax, 
            double ymin, double ymax, double zmin, double zmax)
        {
            pt.X = -1 + 2.0 * (pt.X - xmin) / (xmax - xmin);
            pt.Y = -1 + 2.0 * (pt.Y - ymin) / (ymax - ymin);
            pt.Z = -1 + 2.0 * (pt.Z - zmin) / (zmax - zmin);
            return pt;
        }
    }
}
