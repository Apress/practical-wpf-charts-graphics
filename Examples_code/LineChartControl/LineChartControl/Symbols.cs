using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineCharts
{
    public class Symbols
    {
        private SymbolTypeEnum symbolType;
        private double symbolSize;
        private Brush borderColor;
        private Brush fillColor;
        private double borderThickness;

        public Symbols()
        {
            symbolType = SymbolTypeEnum.None;
            symbolSize = 8.0;
            borderColor = Brushes.Black;
            fillColor = Brushes.Black;
            borderThickness = 1.0;
        }

        public double BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }

        public Brush BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public Brush FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public double SymbolSize
        {
            get { return symbolSize; }
            set { symbolSize = value; }
        }

        public SymbolTypeEnum SymbolType
        {
            get { return symbolType; }
            set { symbolType = value; }
        }


        public enum SymbolTypeEnum
        {
            Box = 0,
            Circle = 1,
            Cross = 2,
            Diamond = 3,
            Dot = 4,
            InvertedTriangle = 5,
            None = 6,
            OpenDiamond = 7,
            OpenInvertedTriangle = 8,
            OpenTriangle = 9,
            Square = 10,
            Star = 11,
            Triangle = 12,
            Plus = 13          
        }

        public void AddSymbol(Canvas canvas, Point pt)
        {
            Polygon plg = new Polygon();
            plg.Stroke = BorderColor;
            plg.StrokeThickness = BorderThickness;
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = BorderColor;
            ellipse.StrokeThickness = BorderThickness;
            Line line = new Line();
            double halfSize = 0.5 * SymbolSize;

            Canvas.SetZIndex(plg, 5);
            Canvas.SetZIndex(ellipse, 5);

            switch (SymbolType)
            {
                case SymbolTypeEnum.Square:
                    plg.Fill = Brushes.White;
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    canvas.Children.Add(plg);
                    break;
                case SymbolTypeEnum.OpenDiamond:
                    plg.Fill = Brushes.White;
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y));
                    plg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y));
                    plg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    canvas.Children.Add(plg);
                    break;
                case SymbolTypeEnum.Circle:
                    ellipse.Fill = Brushes.White;
                    ellipse.Width = SymbolSize;
                    ellipse.Height = SymbolSize;
                    Canvas.SetLeft(ellipse, pt.X - halfSize);
                    Canvas.SetTop(ellipse, pt.Y - halfSize);
                    canvas.Children.Add(ellipse);
                    break;
                case SymbolTypeEnum.OpenTriangle:
                    plg.Fill = Brushes.White;
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    plg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    canvas.Children.Add(plg);
                    break;
                case SymbolTypeEnum.None:
                    break;
                case SymbolTypeEnum.Cross:
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y + halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y - halfSize;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    Canvas.SetZIndex(line, 5);
                    break;
                case SymbolTypeEnum.Star:
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y + halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y - halfSize;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    break;
                case SymbolTypeEnum.OpenInvertedTriangle:
                    plg.Fill = Brushes.White;
                    plg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    canvas.Children.Add(plg);
                    break;
                case SymbolTypeEnum.Plus:
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.X1 = pt.X;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    break;
                case SymbolTypeEnum.Dot:
                    ellipse.Fill = FillColor;
                    ellipse.Width = SymbolSize;
                    ellipse.Height = SymbolSize;
                    Canvas.SetLeft(ellipse, pt.X - halfSize);
                    Canvas.SetTop(ellipse, pt.Y - halfSize);
                    canvas.Children.Add(ellipse);
                    break;
                case SymbolTypeEnum.Box:
                    plg.Fill = FillColor;
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    canvas.Children.Add(plg);
                    break;
                case SymbolTypeEnum.Diamond:
                    plg.Fill = FillColor;
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y));
                    plg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y));
                    plg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    canvas.Children.Add(plg);
                    break;
                case SymbolTypeEnum.InvertedTriangle:
                    plg.Fill = FillColor;
                    plg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    canvas.Children.Add(plg);
                    break;
                case SymbolTypeEnum.Triangle:
                    plg.Fill = FillColor;
                    plg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    plg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    plg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    canvas.Children.Add(plg);
                    break;
            }

        }
    }
}
