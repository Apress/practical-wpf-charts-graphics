using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineCharts
{
    public class Legend
    {
        private bool isLegend;
        private bool isBorder;
        private Canvas legendCanvas;
        private LegendPositionEnum legendPosition;

        public Legend()
        {
            isLegend = false;
            isBorder = true;
            legendPosition = LegendPositionEnum.NorthEast;
        }

        public LegendPositionEnum LegendPosition
        {
            get { return legendPosition; }
            set { legendPosition = value; }
        }

        public Canvas LegendCanvas
        {
            get { return legendCanvas; }
            set { legendCanvas = value; }
        }

        public bool IsLegend
        {
            get { return isLegend; }
            set { isLegend = value; }
        }

        public bool IsBorder
        {
            get { return isBorder; }
            set { isBorder = value; }
        }

        public enum LegendPositionEnum
        {
            North,
            NorthWest,
            West,
            SouthWest,
            South,
            SouthEast,
            East,
            NorthEast
        }


        public void AddLegend(Canvas canvas, DataCollection dc)
        {
            TextBlock tb = new TextBlock();
            if (dc.DataList.Count < 1 || !IsLegend)
                return;
            int n = 0;
            string[] legendLabels = new string[dc.DataList.Count];
            foreach (DataSeries ds in dc.DataList)
            {
                legendLabels[n] = ds.SeriesName;
                n++;
            }

            double legendWidth = 0;
            Size size = new Size(0, 0);
            for (int i = 0; i < legendLabels.Length; i++)
            {
                tb = new TextBlock();
                tb.Text = legendLabels[i];
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (legendWidth < size.Width)
                    legendWidth = size.Width;
            }

            legendWidth += 50;
            legendCanvas.Width = legendWidth + 5;
            double legendHeight = 17 * dc.DataList.Count;
            double sx = 6;
            double sy = 0;
            double textHeight = size.Height;
            double lineLength = 34;
            Rectangle legendRect = new Rectangle();
            legendRect.Stroke = Brushes.Black;
            legendRect.Fill = Brushes.White;
            legendRect.Width = legendWidth;
            legendRect.Height = legendHeight;

            if (IsLegend && IsBorder)
                LegendCanvas.Children.Add(legendRect);
            Canvas.SetZIndex(LegendCanvas, 10);

            n = 1;
            foreach (DataSeries ds in dc.DataList)
            {
                double xSymbol = sx + lineLength / 2;
                double xText = 2 * sx + lineLength;
                double yText = n * sy + (2 * n - 1) * textHeight / 2;
                Line line = new Line();
                AddLinePattern(line, ds);
                line.X1 = sx;
                line.Y1 = yText;
                line.X2 = sx + lineLength;
                line.Y2 = yText;
                LegendCanvas.Children.Add(line);
                ds.Symbols.AddSymbol(legendCanvas, new Point(0.5 * (line.X2 - line.X1 + ds.Symbols.SymbolSize) + 1, line.Y1));

                tb = new TextBlock();
                tb.Text = ds.SeriesName;
                LegendCanvas.Children.Add(tb);
                Canvas.SetTop(tb, yText - size.Height / 2);
                Canvas.SetLeft(tb, xText);
                n++;
            }
            legendCanvas.Width = legendRect.Width;
            legendCanvas.Height = legendRect.Height;

            double offSet = 7.0;
            switch (LegendPosition)
            {
                case LegendPositionEnum.East:
                    Canvas.SetRight(legendCanvas, offSet);
                    Canvas.SetTop(legendCanvas, canvas.Height / 2 - legendRect.Height / 2);
                    break;
                case LegendPositionEnum.NorthEast:
                    Canvas.SetTop(legendCanvas, offSet);
                    Canvas.SetRight(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.North:
                    Canvas.SetTop(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas, canvas.Width / 2 - legendRect.Width / 2);
                    break;
                case LegendPositionEnum.NorthWest:
                    Canvas.SetTop(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.West:
                    Canvas.SetTop(legendCanvas, canvas.Height / 2 - legendRect.Height / 2);
                    Canvas.SetLeft(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.SouthWest:
                    Canvas.SetBottom(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.South:
                    Canvas.SetBottom(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas, canvas.Width / 2 - legendRect.Width / 2);
                    break;
                case LegendPositionEnum.SouthEast:
                    Canvas.SetBottom(legendCanvas, offSet);
                    Canvas.SetRight(legendCanvas, offSet);
                    break;
            }
        }

        private void AddLinePattern(Line line, DataSeries ds)
        {
            line.Stroke = ds.LineColor;
            line.StrokeThickness = ds.LineThickness;

            switch (ds.LinePattern)
            {
                case DataSeries.LinePatternEnum.Dash:
                    line.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case DataSeries.LinePatternEnum.Dot:
                    line.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case DataSeries.LinePatternEnum.DashDot:
                    line.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case DataSeries.LinePatternEnum.None:
                    line.Stroke = Brushes.Transparent;
                    break;
            }
        }
    }
}
