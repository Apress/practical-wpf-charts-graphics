using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Chart3DWithWPFEngine
{
    public partial class SimpleSurfaceTest : Window
    {
        private SimpleSurface ss1 = new SimpleSurface();
        private SimpleSurface ss2 = new SimpleSurface();
        private Random rand = new Random();

        public SimpleSurfaceTest()
        {
            InitializeComponent();

            ss1.IsHiddenLine = true;
            ss1.Viewport3d = viewport1;
            ss2.IsHiddenLine = false;
            ss2.Viewport3d = viewport2;
            AddSinc(ss1);
            AddSinc(ss2);
            //AddPeaks(ss1);
            //AddPeaks(ss2);
            //AddRandomSurface(ss1);
            //AddRandomSurface(ss2);
        }

        private void AddSinc(SimpleSurface ss)
        {
            ss.Xmin = -8;
            ss.Xmax = 8;
            ss.Zmin = -8;
            ss.Zmax = 8;
            ss.Ymin = -1;
            ss.Ymax = 1;
            ss.CreateSurface(Sinc);
        }
        private Point3D Sinc(double x, double z)
        {
            double r = Math.Sqrt(x * x + z * z) + 0.00001;
            double y = Math.Sin(r) / r;
            return new Point3D(x, y, z);
        }

        private void AddPeaks(SimpleSurface ss)
        {
            ss.Xmin = -3;
            ss.Xmax = 3;
            ss.Zmin = -3;
            ss.Zmax = 3;
            ss.Ymin = -8;
            ss.Ymax = 8;
            ss.CreateSurface(Peaks);
        }
        private Point3D Peaks(double x, double z)
        {
            double y = 3 * Math.Pow((1 - x), 2) * Math.Exp(-x * x - (z + 1) * (z + 1)) -
                       10 * (0.2 * x - Math.Pow(x, 3) - Math.Pow(z, 5)) * Math.Exp(-x * x - z * z) -
                       1 / 3 * Math.Exp(-(x + 1) * (x + 1) - z * z);
            return new Point3D(x, y, z);
        }


        private void AddRandomSurface(SimpleSurface ss)
        {
            ss.Xmin = -8;
            ss.Xmax = 8;
            ss.Zmin = -8;
            ss.Zmax = 8;
            ss.Ymin = -1;
            ss.Ymax = 1;
            ss.CreateSurface(RandomSurface);
        }

        private Point3D RandomSurface(double x, double z)
        {
            double r = Math.Sqrt(x * x + z * z) + 0.00001;
            double y = Math.Sin(r) / r  + 0.2 * rand.NextDouble();
            return new Point3D(x, y, z);
        }
    }
}
