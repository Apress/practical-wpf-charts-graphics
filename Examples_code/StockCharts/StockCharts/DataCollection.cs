using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace StockCharts
{
    public class DataCollection
    {
        private List<DataSeries> dataList = new List<DataSeries>();
        private StockChartTypeEnum stockChartType = StockChartTypeEnum.HiLoOpenClose;

        public StockChartTypeEnum StockChartType
        {
            get { return stockChartType; }
            set { stockChartType = value; }
        }

        public List<DataSeries> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        public void AddStockChart(ChartStyle cs)
        {
            foreach (DataSeries ds in DataList)
            {
                double barWidth = cs.ChartCanvas.Width / (5 * ds.DataString.GetLength(1));
                double d0 = cs.DateToDouble(ds.DataString[0, 0]);
                double d1 = cs.DateToDouble(ds.DataString[0, 1]);

                double[,] stockData = new double[ds.DataString.GetLength(0), ds.DataString.GetLength(1)];
                for (int i = 0; i < ds.DataString.GetLength(1); i++)
                {
                    for (int j = 1; j < stockData.GetLength(0); j++)
                    {
                        if (d0 > d1)
                            stockData[j, i] = Convert.ToDouble(ds.DataString[j, ds.DataString.GetLength(1) - 1 - i]);
                        else
                            stockData[j, i] = Convert.ToDouble(ds.DataString[j, i]);
                    }
                }
                for (int i = 0; i < ds.DataString.GetLength(1); i++)
                {
                    Point ptHigh = cs.NormalizePoint(new Point(i, stockData[2, i]));
                    Point ptLow = cs.NormalizePoint(new Point(i, stockData[3, i]));
                    Point ptOpen = cs.NormalizePoint(new Point(i, stockData[1, i]));
                    Point ptClose = cs.NormalizePoint(new Point(i, stockData[4, i]));
                    Point ptOpen1 = new Point(ptOpen.X - barWidth, ptOpen.Y);
                    Point ptClose1 = new Point(ptClose.X + barWidth, ptClose.Y);
                    Point ptOpen2 = new Point(ptOpen.X + barWidth, ptOpen.Y);
                    Point ptClose2 = new Point(ptClose.X - barWidth, ptClose.Y);

                    switch (StockChartType)
                    {
                        case StockChartTypeEnum.Line:   // Draw Line stock chart:
                            if (i > 0)
                            {
                                Point pt1 = cs.NormalizePoint(new Point(i - 1, stockData[4, i - 1]));
                                Point pt2 = cs.NormalizePoint(new Point(i, stockData[4, i]));
                                DrawLine(cs.ChartCanvas, pt1, pt2, ds.LineColor, ds.LineThickness);
                            }
                            break;
                        case StockChartTypeEnum.HiLo:   // Draw Hi-Lo stock chart:
                            DrawLine(cs.ChartCanvas, ptLow, ptHigh, ds.LineColor, ds.LineThickness);
                            break;
                        case StockChartTypeEnum.HiLoOpenClose:  // Draw Hi-Lo-Open-Close stock chart:
                            DrawLine(cs.ChartCanvas, ptLow, ptHigh, ds.LineColor, ds.LineThickness);
                            DrawLine(cs.ChartCanvas, ptOpen, ptOpen1, ds.LineColor, ds.LineThickness);
                            DrawLine(cs.ChartCanvas, ptClose, ptClose1, ds.LineColor, ds.LineThickness);
                            break;
                        case StockChartTypeEnum.Candle: // Draw candle stock chart:
                            DrawLine(cs.ChartCanvas, ptLow, ptHigh, ds.LineColor, ds.LineThickness);
                            Polygon plg = new Polygon();
                            plg.Stroke = ds.LineColor;
                            plg.StrokeThickness = ds.LineThickness;
                            Brush fillColor = ds.FillColor;
                            if (stockData[1, i] < stockData[4, i])
                                fillColor = Brushes.White;
                            plg.Fill = fillColor;
                            plg.Points.Add(ptOpen1);
                            plg.Points.Add(ptOpen2);
                            plg.Points.Add(ptClose1);
                            plg.Points.Add(ptClose2);
                            cs.ChartCanvas.Children.Add(plg);
                            break;
                    }
                }
            }
        }

        private void DrawLine(Canvas canvas, Point pt1, Point pt2, Brush lineColor, double lineThickness)
        {
            Line line = new Line();
            line.Stroke = lineColor;
            line.StrokeThickness = lineThickness;
            line.X1 = pt1.X;
            line.Y1 = pt1.Y;
            line.X2 = pt2.X;
            line.Y2 = pt2.Y;
            canvas.Children.Add(line);
        }

        /*public void AddLines(ChartStyle cs)
        {
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
                    ds.LineSeries.Points[i] = cs.NormalizePoint(ds.LineSeries.Points[i]);
                }
                cs.ChartCanvas.Children.Add(ds.LineSeries);
                j++;
            }
        }*/

        public enum StockChartTypeEnum
        {
            HiLo = 0,
            HiLoOpenClose = 1,
            Candle = 2,
            Line = 3
        }
    }
}
