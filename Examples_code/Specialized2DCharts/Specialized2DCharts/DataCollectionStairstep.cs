using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class DataCollectionStairstep : DataCollection
    {
        public void AddStairstep(ChartStyleGridlines csg)
        {
            foreach (DataSeriesStairstep ds in DataList)
            {
                List<Point> ptList = new List<Point>();
                Point[] pts = new Point[2];
                ds.AddStairstepLinePattern();

                // Create Stairstep data:
                for (int i = 0; i < ds.LineSeries.Points.Count - 1; i++)
                {
                    pts[0] = ds.LineSeries.Points[i];
                    pts[1] = ds.LineSeries.Points[i + 1];
                    ptList.Add(pts[0]);
                    ptList.Add(new Point(pts[1].X, pts[0].Y));
                }
                ptList.Add(new Point(pts[1].X, pts[0].Y));

                // Draw stairstep line:
                for (int i = 0; i < ptList.Count; i++)
                {
                    ds.StairstepLineSeries.Points.Add(csg.NormalizePoint(ptList[i]));
                }
                csg.ChartCanvas.Children.Add(ds.StairstepLineSeries);
            }
        }
    }
}
