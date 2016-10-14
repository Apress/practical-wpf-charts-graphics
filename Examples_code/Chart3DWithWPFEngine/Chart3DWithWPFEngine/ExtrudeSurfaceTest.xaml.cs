using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Chart3DWithWPFEngine
{
    public partial class ExtrudeSurfaceTest : Window
    {
        private ExtrudeSurface es = new ExtrudeSurface();

        public ExtrudeSurfaceTest()
        {
            InitializeComponent();

            es.Viewport3d = viewport;
            es.IsHiddenLine = false;
            AddExtrudeSurface1();
            //AddExtrudeSurface2();
        }

        // Extruded surface:
        private void AddExtrudeSurface1()
        {
            for (int i = 0; i < 17; i++)
            {
                double angle = i * Math.PI / 16 + 3 * Math.PI / 2;
                es.CurvePoints.Add(new Point3D(Math.Cos(angle), 0, Math.Sin(angle)));
            }

            for (int i = 0; i < 33; i++)
            {
                es.PathPoints.Add(new Point3D(Math.Cos(i * Math.PI / 12), i * Math.PI / 12, 0));
            }
            es.Xmin = -3;
            es.Xmax = 3;
            es.Ymin = 5;
            es.Ymax = 20;
            es.Zmin = -3;
            es.Zmax = 5;
            es.CreateSurface();
        }

        // Another Extruded surface:
        private void AddExtrudeSurface2()
        {
            for (int i = 0; i < 17; i++)
            {
                double angle = i * Math.PI / 8;
                es.CurvePoints.Add(new Point3D(1 + 0.3 * Math.Cos(angle), 0, 0.3 * Math.Sin(angle)));
            }

            for (int i = 0; i < 45; i++)
            {
                double angle = i * Math.PI / 16;
                es.PathPoints.Add(new Point3D(1.3*Math.Cos(angle), angle, 1.3*Math.Sin(angle)));
            }
            es.Xmin = -3;
            es.Xmax = 3;
            es.Ymin = 5;
            es.Ymax = 30;
            es.Zmin = -3;
            es.Zmax = 3;
            es.CreateSurface();
        } 
    }
}
