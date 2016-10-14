using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    class DataCollectionStem : DataCollection
    {
        public void AddStems(ChartStyleGridlines csg)
        {
            foreach (DataSeries ds in DataList)
            {
                Point[] pts = new Point[2];
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {
                    pts[0] = csg.NormalizePoint(new Point(ds.LineSeries.Points[i].X, 0));
                    pts[1] = csg.NormalizePoint(ds.LineSeries.Points[i]);

                    Line line = new Line();
                    line.Stroke = ds.LineColor;
                    line.StrokeThickness = ds.LineThickness;
                    line.X1 = pts[0].X;
                    line.Y1 = pts[0].Y;
                    line.X2 = pts[1].X;
                    line.Y2 = pts[1].Y;
                    csg.ChartCanvas.Children.Add(line);
                    ds.Symbols.AddSymbol(csg.ChartCanvas, csg.NormalizePoint(ds.LineSeries.Points[i]));
                }              
            }
        }
    }
}
