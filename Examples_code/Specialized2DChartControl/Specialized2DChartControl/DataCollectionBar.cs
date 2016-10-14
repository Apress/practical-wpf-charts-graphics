using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Specialized2DCharts
{
    public class DataCollectionBar : DataCollection
    {
        private BarTypeEnum barType = BarTypeEnum.Vertical;

        public BarTypeEnum BarType
        {
            get { return barType; }
            set { barType = value; }
        }

        public enum BarTypeEnum
        {
            Vertical = 0,
            Horizontal = 1,
            VerticalStack = 2,
            HorizontalStack = 3,
            VerticalOverlay = 4,
            HorizontalOverlay = 5
        }

        public void AddBars(ChartStyleGridlines csg)
        {
            int nSeries = DataList.Count;
            double width;

            switch (BarType)
            {
                case BarTypeEnum.Vertical:
                    if (nSeries == 1)
                    {
                        foreach (DataSeriesBar ds in DataList)
                        {
                            width = csg.XTick * ds.BarWidth;
                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                DrawVerticalBar(ds.LineSeries.Points[i], csg, ds, width, 0);
                            }
                        }
                    }
                    else
                    {
                        int j = 0;
                        foreach (DataSeriesBar ds in DataList)
                        {
                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                DrawVerticalBar1(ds.LineSeries.Points[i], csg, ds, nSeries, j);
                            }
                            j++;
                        }
                    }
                    break;

                case BarTypeEnum.VerticalOverlay:
                    if (nSeries > 1)
                    {
                        int j = 0;
                        foreach (DataSeriesBar ds in DataList)
                        {
                            width = csg.XTick * ds.BarWidth;
                            width = width / Math.Pow(2, j);
                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                DrawVerticalBar(ds.LineSeries.Points[i], csg, ds, width, 0);
                            }
                            j++;
                        }
                    }
                    break;

                case BarTypeEnum.VerticalStack:
                    if (nSeries > 1)
                    {
                        List<Point> temp = new List<Point>();
                        double[] tempy = new double[DataList[0].LineSeries.Points.Count];
                        
                        foreach (DataSeriesBar ds in DataList)
                        {
                            width = csg.XTick * ds.BarWidth;

                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                if (temp.Count > 0)
                                {
                                    tempy[i] += temp[i].Y;
                                }
                                DrawVerticalBar(ds.LineSeries.Points[i], csg, ds, width, tempy[i]);
                            }
                            temp.Clear();
                            temp.AddRange(ds.LineSeries.Points);
                        }
                    }
                    break;

                case BarTypeEnum.Horizontal:
                    if (nSeries == 1)
                    {
                        foreach (DataSeriesBar ds in DataList)
                        {
                            width = csg.YTick * ds.BarWidth;
                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                DrawHorizontalBar(ds.LineSeries.Points[i], csg, ds, width, 0);
                            }
                        }
                    }
                    else
                    {
                        int j = 0;
                        foreach (DataSeriesBar ds in DataList)
                        {
                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                DrawHorizontalBar1(ds.LineSeries.Points[i], csg, ds, nSeries, j);
                            }
                            j++;
                        }
                    }
                    break;

                case BarTypeEnum.HorizontalOverlay:
                    if (nSeries > 1)
                    {
                        int j = 0;
                        foreach (DataSeriesBar ds in DataList)
                        {
                            width = csg.YTick * ds.BarWidth;
                            width = width / Math.Pow(2, j);
                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                DrawHorizontalBar(ds.LineSeries.Points[i], csg, ds, width, 0);
                            }
                            j++;
                        }
                    }
                    break;

                case BarTypeEnum.HorizontalStack:
                    if (nSeries > 1)
                    {
                        List<Point> temp = new List<Point>();
                        double[] tempy = new double[DataList[0].LineSeries.Points.Count];

                        foreach (DataSeriesBar ds in DataList)
                        {
                            width = csg.YTick * ds.BarWidth;

                            for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                            {
                                if (temp.Count > 0)
                                {
                                    tempy[i] += temp[i].X;
                                }
                                DrawHorizontalBar(ds.LineSeries.Points[i], csg, ds, width, tempy[i]);
                            }
                            temp.Clear();
                            temp.AddRange(ds.LineSeries.Points);
                        }
                    }
                    break;
            }
        }

        private void DrawVerticalBar(Point pt, ChartStyleGridlines csg, DataSeriesBar ds, double width, double y)
        {
            Polygon plg = new Polygon();
            plg.Fill = ds.FillColor;
            plg.Stroke = ds.BorderColor;
            plg.StrokeThickness = ds.BorderThickness;

            double x = pt.X - 0.5 * csg.XTick;
            plg.Points.Add(csg.NormalizePoint(new Point(x - width / 2, y)));
            plg.Points.Add(csg.NormalizePoint(new Point(x + width / 2, y)));
            plg.Points.Add(csg.NormalizePoint(new Point(x + width / 2, y + pt.Y)));
            plg.Points.Add(csg.NormalizePoint(new Point(x - width / 2, y + pt.Y)));
            csg.ChartCanvas.Children.Add(plg);
        }

        
        private void DrawVerticalBar1(Point pt, ChartStyleGridlines csg, DataSeriesBar ds, int nSeries, int n)
        {
            Polygon plg = new Polygon();
            plg.Fill = ds.FillColor;
            plg.Stroke = ds.BorderColor;
            plg.StrokeThickness = ds.BorderThickness;

            double width = 0.7 * csg.XTick;
            double w1 = width / nSeries;
            double w = ds.BarWidth * w1;
            double space = (w1 - w) / 2;
            double x = pt.X - 0.5 * csg.XTick;
            plg.Points.Add(csg.NormalizePoint(new Point(x - width / 2 + space + n * w1, 0)));
            plg.Points.Add(csg.NormalizePoint(new Point(x - width / 2 + space + n * w1 + w, 0)));
            plg.Points.Add(csg.NormalizePoint(new Point(x - width / 2 + space + n * w1 + w, pt.Y)));
            plg.Points.Add(csg.NormalizePoint(new Point(x - width / 2 + space + n * w1, pt.Y)));
            csg.ChartCanvas.Children.Add(plg);
        }

        private void DrawHorizontalBar(Point pt, ChartStyleGridlines csg, DataSeriesBar ds, double width, double x)
        {
            Polygon plg = new Polygon();
            plg.Fill = ds.FillColor;
            plg.Stroke = ds.BorderColor;
            plg.StrokeThickness = ds.BorderThickness;

            double y = pt.Y - 0.5 * csg.YTick;
            plg.Points.Add(csg.NormalizePoint(new Point(x, y - width / 2)));
            plg.Points.Add(csg.NormalizePoint(new Point(x, y + width / 2)));
            plg.Points.Add(csg.NormalizePoint(new Point(x + pt.X, y + width / 2)));
            plg.Points.Add(csg.NormalizePoint(new Point(x + pt.X, y - width / 2)));
            csg.ChartCanvas.Children.Add(plg);
        }

        private void DrawHorizontalBar1(Point pt, ChartStyleGridlines csg, DataSeriesBar ds, int nSeries, int n)
        {
            Polygon plg = new Polygon();
            plg.Fill = ds.FillColor;
            plg.Stroke = ds.BorderColor;
            plg.StrokeThickness = ds.BorderThickness;

            double width = 0.7 * csg.YTick;
            double w1 = width / nSeries;
            double w = ds.BarWidth * w1;
            double space = (w1 - w) / 2;
            double y = pt.Y - 0.5 * csg.YTick;
            plg.Points.Add(csg.NormalizePoint(new Point(0, y - width / 2 + space + n * w1)));
            plg.Points.Add(csg.NormalizePoint(new Point(0, y - width / 2 + space + n * w1 + w)));
            plg.Points.Add(csg.NormalizePoint(new Point(pt.X, y - width / 2 + space + n * w1 + w)));
            plg.Points.Add(csg.NormalizePoint(new Point(pt.X, y - width / 2 + space + n * w1)));
            csg.ChartCanvas.Children.Add(plg);
        }
       
    }
}
