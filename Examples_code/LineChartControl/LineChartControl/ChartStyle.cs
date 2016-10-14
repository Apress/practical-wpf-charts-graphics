using System;
using System.Windows.Controls;
using System.Windows;

namespace LineCharts
{
    public class ChartStyle
    {
        private double xmin = 0;
        private double xmax = 10;
        private double ymin = 0;
        private double ymax = 10;

        private Canvas chartCanvas;

        public ChartStyle()
        {
        }

        public Canvas ChartCanvas
        {
            get { return chartCanvas; }
            set { chartCanvas = value; }
        }

        public double Xmin
        {
            get { return xmin; }
            set { xmin = value; }
        }

        public double Xmax
        {
            get { return xmax; }
            set { xmax = value; }
        }

        public double Ymin
        {
            get { return ymin; }
            set { ymin = value; }
        }

        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }

        public Point NormalizePoint(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 270;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = ChartCanvas.Height - (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);
            return result;
        }
    }
}
