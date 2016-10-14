using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    class DataCollectionArea : DataCollection
    {
        private double areaAxis = 0;
        private List<DataSeriesArea> areaList;

        public DataCollectionArea()
        {
            areaList = new List<DataSeriesArea>();
        }

        public List<DataSeriesArea> AreaList
        {
            get { return areaList; }
            set { areaList = value; }
        }

        public double AreaAxis
        {
            get { return areaAxis; }
            set { areaAxis = value; }
        }

        public void AddAreas(ChartStyleGridlines csg)
        {
            int nSeries = AreaList.Count;
            int nPoints = AreaList[0].AreaSeries.Points.Count;
            double[] ySum = new double[nPoints];
            Point[] pts = new Point[2 * nPoints];
            Point[] pt0 = new Point[nPoints];
            Point[] pt1 = new Point[nPoints];

            for (int i = 0; i < nPoints; i++)
                ySum[i] = AreaAxis;

            foreach (DataSeriesArea area in AreaList)
            {
                area.AddBorderPattern();
                for (int i = 0; i < nPoints; i++)
                {
                    pt0[i] = new Point(area.AreaSeries.Points[i].X, ySum[i]);
                    ySum[i] += area.AreaSeries.Points[i].Y;
                    pt1[i] = new Point(area.AreaSeries.Points[i].X, ySum[i]);
                    pts[i] = csg.NormalizePoint(pt0[i]);
                    pts[2 * nPoints - 1 - i] = csg.NormalizePoint(pt1[i]);
                }
               
                area.AreaSeries.Points.Clear();
                for (int i = 0; i < pts.GetLength(0); i++)
                    area.AreaSeries.Points.Add(pts[i]);
                csg.ChartCanvas.Children.Add(area.AreaSeries);                
            }         
        }
    }
}
