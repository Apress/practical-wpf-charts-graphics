using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class DataSeriesErrorbar : DataSeries
    {
        private Polyline errorLineSeries = new Polyline();
        private Brush errorLineColor;
        private double errorLineThickness = 1;
        private ErrorLinePatternEnum errorLinePattern;

        public Brush ErrorLineColor
        {
            get { return errorLineColor; }
            set { errorLineColor = value; }
        }

        public Polyline ErrorLineSeries
        {
            get { return errorLineSeries; }
            set { errorLineSeries = value; }
        }

        public double ErrorLineThickness
        {
            get { return errorLineThickness; }
            set { errorLineThickness = value; }
        }

        public ErrorLinePatternEnum ErrorLinePattern
        {
            get { return errorLinePattern; }
            set { errorLinePattern = value; }
        }

        public void AddErrorLinePattern()
        {
            ErrorLineSeries.Stroke = ErrorLineColor;
            ErrorLineSeries.StrokeThickness = ErrorLineThickness;

            switch (ErrorLinePattern)
            {
                case ErrorLinePatternEnum.Dash:
                    ErrorLineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case ErrorLinePatternEnum.Dot:
                    ErrorLineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case ErrorLinePatternEnum.DashDot:
                    ErrorLineSeries.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case ErrorLinePatternEnum.None:
                    ErrorLineSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        public enum ErrorLinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }
    }
}
