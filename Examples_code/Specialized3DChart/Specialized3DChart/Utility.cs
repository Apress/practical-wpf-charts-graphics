using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Specialized3DChart
{
    public class Utility
    {
        public static Matrix3D AzimuthElevation(double elevation, double azimuth)
        {
            // Make sure elevation is in the range of [-90, 90]:
            if (elevation > 90)
                elevation = 90;
            else if (elevation < -90)
                elevation = -90;

            // Make sure azimuth is in the range of [-180, 180]:
            if (azimuth > 180)
                azimuth = 180;
            else if (azimuth < -180)
                azimuth = -180;

            elevation = elevation * Math.PI / 180;
            azimuth = azimuth * Math.PI / 180;
            double sne = Math.Sin(elevation);
            double cne = Math.Cos(elevation);
            double sna = Math.Sin(azimuth);
            double cna = Math.Cos(azimuth);

            Matrix3D result = new Matrix3D(cna, -sne * sna,  cne * sna, 0,
                                           sna,  sne * cna, -cne * cna, 0,
                                             0,        cne,        sne, 0,
                                             0,          0,          0, 1);

            return result;
        }

        public static Vector3D NormalVector(Point3D pt1, Point3D pt2, Point3D pt3)
        {
            Vector3D v1 = new Vector3D();
            Vector3D v2 = new Vector3D();
            v1.X = pt2.X - pt1.X;
            v1.Y = pt2.Y - pt1.Y;
            v1.Z = pt2.Z - pt1.Z;
            v2.X = pt3.X - pt2.X;
            v2.Y = pt3.Y - pt2.Y;
            v2.Z = pt3.Z - pt1.Z;

            return Vector3D.CrossProduct(v1, v2);
        }

        public static void Peak3D(ChartStyle cs, DataSeriesSurface ds)
        {
            cs.Xmin = -3;
            cs.Xmax = 3;
            cs.Ymin = -3;
            cs.Ymax = 3;
            cs.Zmin = -8;
            cs.Zmax = 8;
            cs.XTick = 1;
            cs.YTick = 1;
            cs.ZTick = 4;

            ds.XLimitMin = cs.Xmin;
            ds.YLimitMin = cs.Ymin;
            ds.XSpacing = 0.2;
            ds.YSpacing = 0.2;
            ds.XNumber = Convert.ToInt16((cs.Xmax - cs.Xmin) / ds.XSpacing) + 1;
            ds.YNumber = Convert.ToInt16((cs.Ymax - cs.Ymin) / ds.YSpacing) + 1;

            Point3D[,] pts = new Point3D[ds.XNumber, ds.YNumber];
            for (int i = 0; i < ds.XNumber; i++)
            {
                for (int j = 0; j < ds.YNumber; j++)
                {
                    double x = ds.XLimitMin + i * ds.XSpacing;
                    double y = ds.YLimitMin + j * ds.YSpacing;
                    double z = 3 * Math.Pow((1 - x), 2) * 	Math.Exp(-x * x - (y + 1) * (y + 1)) - 10 * 
                        (0.2 * x - Math.Pow(x, 3) - Math.Pow(y, 5)) * Math.Exp(-x * x - y * y) - 1 / 3 * 
					    Math.Exp(-(x + 1) * (x + 1) - y * y);
                    pts[i, j] = new Point3D(x, y, z);
                }
            }
            ds.PointArray = pts;
        }

        public static void Sinc3D(ChartStyle cs, DataSeriesSurface ds)
        {
            cs.Xmin = -8;
            cs.Xmax = 8;
            cs.Ymin = -8;
            cs.Ymax = 8;
            cs.Zmin = -0.5f;
            cs.Zmax = 1;
            cs.XTick = 4;
            cs.YTick = 4;
            cs.ZTick = 0.5f;

            ds.XLimitMin = cs.Xmin;
            ds.YLimitMin = cs.Ymin;
            ds.XSpacing = 0.5;
            ds.YSpacing = 0.5;
            ds.XNumber = Convert.ToInt16((cs.Xmax - cs.Xmin) / ds.XSpacing) + 1;
            ds.YNumber = Convert.ToInt16((cs.Ymax - cs.Ymin) / ds.YSpacing) + 1;

            Point3D[,] pts = new Point3D[ds.XNumber, ds.YNumber];
            for (int i = 0; i < ds.XNumber; i++)
            {
                for (int j = 0; j < ds.YNumber; j++)
                {
                    double x = ds.XLimitMin + i * ds.XSpacing;
                    double y = ds.YLimitMin + j * ds.YSpacing;
                    double r = Math.Sqrt(x * x + y * y) + 0.000001;
                    double z = Math.Sin(r) / r;
                    pts[i, j] = new Point3D(x, y, z);
                }
            }
            ds.PointArray = pts;
        }
    }
}
