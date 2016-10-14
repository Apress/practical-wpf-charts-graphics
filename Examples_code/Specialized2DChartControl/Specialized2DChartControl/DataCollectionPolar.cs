using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class DataCollectionPolar : DataCollection
    {
        public void AddPolar(ChartStylePolar csp)
        {
            double xc = csp.ChartCanvas.Width/ 2;
            double yc = csp.ChartCanvas.Height / 2;

            int j = 0;
            foreach (DataSeries ds in DataList)
            {
                if (ds.SeriesName == "Default Name")
                {
                    ds.SeriesName = "DataSeries" + j.ToString();
                }
                ds.AddLinePattern();
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {

                    double r = ds.LineSeries.Points[i].Y;
                    double theta = ds.LineSeries.Points[i].X * Math.PI / 180;
                    if (csp.AngleDirection == ChartStylePolar.AngleDirectionEnum.CounterClockWise)
                        theta = -theta;

                    double x = xc + csp.RNormalize(r) * Math.Cos(theta);
                    double y = yc + csp.RNormalize(r) * Math.Sin(theta);
                    ds.LineSeries.Points[i] = new Point(x, y);
                }
                csp.ChartCanvas.Children.Add(ds.LineSeries);
                j++;
            }
        }
    }
}
