using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Specialized2DCharts
{
    public class PieLegend
    {
        private bool isLegendVisible = false;

        public bool IsLegendVisible
        {
            get { return isLegendVisible; }
            set { isLegendVisible = value; }
        }

        public void AddLegend(Canvas canvas, PieStyle ps)
        {
            TextBlock tb = new TextBlock();
            if (ps.DataList.Count < 1 || !IsLegendVisible)
                return;

            double legendWidth = 0;
            Size size = new Size(0, 0);
            for (int i = 0; i < ps.LabelList.Count; i++)
            {
                tb = new TextBlock();
                tb.Text = ps.LabelList[i];
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (legendWidth < size.Width)
                    legendWidth = size.Width;
            }

            legendWidth += 20;
            canvas.Width = legendWidth + 5;
            double legendHeight = 17 * ps.DataList.Count;
            double sx = 6;
            double sy = 0;
            double textHeight = size.Height;
            double lineLength = 34;
            Rectangle legendRect = new Rectangle();
            legendRect.Stroke = Brushes.Black;
            legendRect.Fill = Brushes.White;
            legendRect.Width = legendWidth + 18;
            legendRect.Height = legendHeight;

            if (IsLegendVisible)
                canvas.Children.Add(legendRect);

            Rectangle rect;
            int n = 1;
            foreach (double data in ps.DataList)
            {
                double xText = 2 * sx + lineLength;
                double yText = n * sy + (2 * n - 1) * textHeight / 2;

                rect = new Rectangle();
                rect.Stroke = ps.BorderColor;
                rect.StrokeThickness = ps.BorderThickness;
                rect.Fill = ps.ColormapBrushes.ColormapBrushes()[n - 1];
                rect.Width = 10;
                rect.Height = 10;
                Canvas.SetLeft(rect, sx + lineLength / 2 - 15);
                Canvas.SetTop(rect, yText - 2);
                canvas.Children.Add(rect);

                tb = new TextBlock();
                tb.Text = ps.LabelList[n - 1];
                canvas.Children.Add(tb);
                Canvas.SetTop(tb, yText - size.Height / 2 + 2);
                Canvas.SetLeft(tb, xText - 15);
                n++;
            }
            canvas.Width = legendRect.Width;
            canvas.Height = legendRect.Height;
        }
    }
}
