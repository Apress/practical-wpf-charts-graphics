using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Specialized2DCharts
{
    public class LineStyle
    {
        private Brush lineColor = Brushes.Black;
        private double lineThickness = 1;
        private LinePatternEnum linePattern;

        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        public double LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }

        public LinePatternEnum LinePattern
        {
            get { return linePattern; }
            set { linePattern = value; }
        }

        public DoubleCollection AddLinePattern()
        {
            DoubleCollection dc = new DoubleCollection();
            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    dc = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    dc = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    dc = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
            }
            return dc;
        }

        public enum LinePatternEnum
        {
            Solid = 0,
            Dash = 1,
            Dot = 2,
            DashDot = 3,
        }
    }
}
