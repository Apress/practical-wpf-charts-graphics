using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StockCharts
{
    public class DataCollectionMA : DataCollection
    {
        public void AddSimpleMovingAverage(ChartStyle cs)
        {
            foreach (DataSeriesMA ds in DataList)
            {
                ds.SMALineSeries.Stroke = ds.SMALineColor;
                ds.SMALineSeries.StrokeThickness = ds.SMALineThickness;
                double[] data = ds.SimpleMovingAverage();
                ds.SMALineSeries.Points.Clear();
                for (int i = 0; i < data.Length; i++)
                {
                    ds.SMALineSeries.Points.Add(cs.NormalizePoint(new Point(i + ds.NDays - 1, data[i])));
                }
                cs.ChartCanvas.Children.Add(ds.SMALineSeries);
            }
        }

        public void AddWeightedMovingAverage(ChartStyle cs)
        {
            foreach (DataSeriesMA ds in DataList)
            {
                ds.WMALineSeries.Stroke = ds.WMALineColor;
                ds.WMALineSeries.StrokeThickness = ds.WMALineThickness;
                double[] data = ds.WeightedMovingAverage();
                ds.WMALineSeries.Points.Clear();
                for (int i = 0; i < data.Length; i++)
                {
                    ds.WMALineSeries.Points.Add(cs.NormalizePoint(new Point(i + ds.NDays - 1, data[i])));
                }
                cs.ChartCanvas.Children.Add(ds.WMALineSeries);
            }
        }

        public void AddExponentialMovingAverage(ChartStyle cs)
        {
            foreach (DataSeriesMA ds in DataList)
            {
                ds.EMALineSeries.Stroke = ds.EMALineColor;
                ds.EMALineSeries.StrokeThickness = ds.EMALineThickness;
                double[] data = ds.ExponentialMovingAverage();
                ds.EMALineSeries.Points.Clear();
                for (int i = 0; i < data.Length; i++)
                {
                    ds.EMALineSeries.Points.Add(cs.NormalizePoint(new Point(i + ds.NDays - 1, data[i])));
                }
                cs.ChartCanvas.Children.Add(ds.EMALineSeries);
            }
        }
    }
}
