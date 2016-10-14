using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class DataSeriesStairstep : DataSeries
    {
        private Polyline stairstepLineSeries = new Polyline();
        private Brush stairstepLineColor;
        private double stairstepLineThickness = 1;
        private StairstepLinePatternEnum stairstepLinePattern;

        public Brush StairstepLineColor
        {
            get { return stairstepLineColor; }
            set { stairstepLineColor = value; }
        }

        public Polyline StairstepLineSeries
        {
            get { return stairstepLineSeries; }
            set { stairstepLineSeries = value; }
        }

        public double StairstepLineThickness
        {
            get { return stairstepLineThickness; }
            set { stairstepLineThickness = value; }
        }

        public StairstepLinePatternEnum StairstepLinePattern
        {
            get { return stairstepLinePattern; }
            set { stairstepLinePattern = value; }
        }

        public void AddStairstepLinePattern()
        {
            StairstepLineSeries.Stroke = StairstepLineColor;
            StairstepLineSeries.StrokeThickness = StairstepLineThickness;

            switch (StairstepLinePattern)
            {
                case StairstepLinePatternEnum.Dash:
                    StairstepLineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case StairstepLinePatternEnum.Dot:
                    StairstepLineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case StairstepLinePatternEnum.DashDot:
                    StairstepLineSeries.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case StairstepLinePatternEnum.None:
                    StairstepLineSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        public enum StairstepLinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }
    }
}
