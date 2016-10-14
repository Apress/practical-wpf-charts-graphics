using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class DataSeriesBar : DataSeries
    {
        private Brush fillColor = Brushes.Black;
        private Brush borderColor = Brushes.Black;
        private double borderThickness = 1.0;
        private double barWidth = 0.8;

        public Brush FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public Brush BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public double BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }

        public double BarWidth
        {
            get { return barWidth; }
            set { barWidth = value; }
        }
    }
}
