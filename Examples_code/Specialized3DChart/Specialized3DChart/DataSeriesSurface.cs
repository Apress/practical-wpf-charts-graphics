using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Specialized3DChart
{
    public class DataSeriesSurface : DataSeriesLine3D
    {
        private double xLimitMin = -5;
        private double yLimitMin = 5;
        private double zLimitMin = -5;
        private double xSpacing = 1;
        private double ySpacing = 1;
        private double zSpacing = 1;
        private int xNumber = 10;
        private int yNumber = 10;
        private int zNumber = 10;

        public Point3D[,] PointArray { get; set; }

        public double XLimitMin
        {
            get { return xLimitMin; }
            set { xLimitMin = value; }
        }

        public double YLimitMin
        {
            get { return yLimitMin; }
            set { yLimitMin = value; }
        }

        public double ZLimitMin
        {
            get { return zLimitMin; }
            set { zLimitMin = value; }
        }

        public double XSpacing
        {
            get { return xSpacing; }
            set { xSpacing = value; }
        }

        public double YSpacing
        {
            get { return ySpacing; }
            set { ySpacing = value; }
        }

        public double ZSpacing
        {
            get { return zSpacing; }
            set { zSpacing = value; }
        }

        public int XNumber
        {
            get { return xNumber; }
            set { xNumber = value; }
        }

        public int YNumber
        {
            get { return yNumber; }
            set { yNumber = value; }
        }

        public int ZNumber
        {
            get { return zNumber; }
            set { zNumber = value; }
        }

        public double ZDataMin()
        {
            double zmin = 0;
            for (int i = 0; i < PointArray.GetLength(0); i++)
            {
                for (int j = 0; j < PointArray.GetLength(1); j++)
                {
                    zmin = Math.Min(zmin, PointArray[i, j].Z);
                }
            }
            return zmin;
        }

        public double ZDataMax()
        {
            double zmax = 0;
            for (int i = 0; i < PointArray.GetLength(0); i++)
            {
                for (int j = 0; j < PointArray.GetLength(1); j++)
                {
                    zmax = Math.Max(zmax, PointArray[i, j].Z);
                }
            }
            return zmax;
        }
    }
}
