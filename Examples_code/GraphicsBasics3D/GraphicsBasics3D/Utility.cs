using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using _3DTools;


namespace GraphicsBasics3D
{
    public class Utility
    {
        public static void CreateTriangleFace(Point3D p0, Point3D p1, Point3D p2, 
            Color color, bool isWireframe, Viewport3D viewport)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = color;
            Material material = new DiffuseMaterial(brush);
            GeometryModel3D geometry = new GeometryModel3D(mesh, material);
            ModelUIElement3D model = new ModelUIElement3D();
            model.Model = geometry;
            viewport.Children.Add(model);

            if (isWireframe == true)
            {
                ScreenSpaceLines3D ssl = new ScreenSpaceLines3D();
                ssl.Points.Add(p0);
                ssl.Points.Add(p1);
                ssl.Points.Add(p1);
                ssl.Points.Add(p2);
                ssl.Points.Add(p2);
                ssl.Points.Add(p0);
                ssl.Color = Colors.Black;
                ssl.Thickness = 2;
                viewport.Children.Add(ssl);
            }
        }
    }
}
