using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineCharts
{
    public class DataCollection2Y : DataCollection
    {
        public void AddLines2Y(ChartStyle2Y cs)
        {
            int j = 0;
            foreach (DataSeries2Y ds in DataList)
            {
                if (ds.SeriesName == "Default Name")
                {
                    ds.SeriesName = "DataSeries" + j.ToString();
                }
                ds.AddLinePattern();
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {
                    if (ds.IsY2Data)
                        ds.LineSeries.Points[i] = cs.NormalizePoint2Y(ds.LineSeries.Points[i]);
                    else
                        ds.LineSeries.Points[i] = cs.NormalizePoint(ds.LineSeries.Points[i]);

                    ds.Symbols.AddSymbol(cs.ChartCanvas, ds.LineSeries.Points[i]);
                }
                cs.ChartCanvas.Children.Add(ds.LineSeries);
                j++;
            }
        }
    }
}
