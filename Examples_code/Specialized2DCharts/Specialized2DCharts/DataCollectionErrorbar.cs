using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class DataCollectionErrorbar : DataCollection
    {
        private List<DataSeriesErrorbar> errorList;

        public DataCollectionErrorbar()
        {
            errorList = new List<DataSeriesErrorbar>();
        }

        public List<DataSeriesErrorbar> ErrorList
        {
            get { return errorList; }
            set { errorList = value; }
        }

        public void AddErrorbars(ChartStyleGridlines csg)
        {
            Line line;
            foreach (DataSeriesErrorbar ds in DataList)
            {
                double barLength = (csg.NormalizePoint(ds.LineSeries.Points[1]).X - 
                                    csg.NormalizePoint(ds.LineSeries.Points[0]).X) / 3; 

                for (int i = 0; i < ds.ErrorLineSeries.Points.Count; i++)
                {
                    Point ep = ds.ErrorLineSeries.Points[i];
                    Point dp = ds.LineSeries.Points[i];
                    Point[] pts = new Point[2];
                    pts[0] = csg.NormalizePoint(new Point(dp.X, dp.Y - ep.Y / 2));
                    pts[1] = csg.NormalizePoint(new Point(dp.X, dp.Y + ep.Y / 2));
                    line = new Line();
                    line.Stroke = ds.ErrorLineColor;
                    line.StrokeThickness = ds.ErrorLineThickness;
                    ds.AddErrorLinePattern();
                    line.X1 = pts[0].X;
                    line.Y1 = pts[0].Y;
                    line.X2 = pts[1].X;
                    line.Y2 = pts[1].Y;
                    csg.ChartCanvas.Children.Add(line);
                    line = new Line();
                    line.Stroke = ds.ErrorLineColor;
                    line.StrokeThickness = ds.ErrorLineThickness;
                    ds.AddErrorLinePattern();
                    line.X1 = pts[0].X - barLength / 2;
                    line.Y1 = pts[0].Y;
                    line.X2 = pts[0].X + barLength / 2;
                    line.Y2 = pts[0].Y;
                    csg.ChartCanvas.Children.Add(line);
                    line = new Line();
                    line.Stroke = ds.ErrorLineColor;
                    line.StrokeThickness = ds.ErrorLineThickness;
                    ds.AddErrorLinePattern();
                    line.X1 = pts[1].X - barLength / 2;
                    line.Y1 = pts[1].Y;
                    line.X2 = pts[1].X + barLength / 2;
                    line.Y2 = pts[1].Y;
                    csg.ChartCanvas.Children.Add(line);
                }
            }
        }
    }
}
