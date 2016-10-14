using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class DataSeriesArea : DataSeries
    {
        private Polygon areaSeries = new Polygon();
        private Brush borderColor = Brushes.Black;
        private double borderThickness = 1;
        private BorderPatternEnum borderPattern;
        private Brush fillColor = Brushes.White;

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

        public Polygon AreaSeries
        {
            get { return areaSeries; }
            set { areaSeries = value; }
        }

        public double BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }

        public BorderPatternEnum BorderPattern
        {
            get { return borderPattern; }
            set { borderPattern = value; }
        }

        public void AddBorderPattern()
        {
            AreaSeries.Stroke = BorderColor;
            AreaSeries.StrokeThickness = BorderThickness;
            AreaSeries.Fill = FillColor;

            switch (BorderPattern)
            {
                case BorderPatternEnum.Dash:
                    AreaSeries.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case BorderPatternEnum.Dot:
                    AreaSeries.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case BorderPatternEnum.DashDot:
                    AreaSeries.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case BorderPatternEnum.None:
                    AreaSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        public enum BorderPatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }

    }
}
