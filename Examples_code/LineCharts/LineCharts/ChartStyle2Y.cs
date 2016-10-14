using System;
using System.Windows.Controls;
using System.Windows;

namespace LineCharts
{
    public class ChartStyle2Y : ChartStyle
    {
        private double y2min = 0;
        private double y2max = 10;
        private double y2Tick = 2;

        public double Y2min
        {
            get { return y2min; }
            set { y2min = value; }
        }

        public double Y2max
        {
            get { return y2max; }
            set { y2max = value; }
        }
        public double Y2Tick
        {
            get { return y2Tick; }
            set { y2Tick = value; }
        }

        public Point NormalizePoint2Y(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 270;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = ChartCanvas.Height - (pt.Y - Y2min) * ChartCanvas.Height / (Y2max - Y2min);
            return result;
        }
    }
}
