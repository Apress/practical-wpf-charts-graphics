using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineCharts
{
    public partial class LineChartControl : UserControl
    {
        private ChartStyleLineChartControl controlChartStyle;
        private DataCollectionLineChartControl controlDataCollection;
        private DataSeriesLineChartControl controlDataSeries;
        
        public LineChartControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.controlChartStyle = new ChartStyleLineChartControl(this);
            this.controlDataCollection = new DataCollectionLineChartControl(this);
            this.controlDataSeries = new DataSeriesLineChartControl();
        }

        public ChartStyleLineChartControl ControlChartStyle
        {
            get { return controlChartStyle; }
            set { controlChartStyle = value; }
        }

        public DataCollectionLineChartControl ControlDataCollection
        {
            get { return controlDataCollection; }
            set { controlDataCollection = value; }
        }

        public DataSeriesLineChartControl ControlDataSeries
        {
            get { return controlDataSeries; }
            set { controlDataSeries = value; }
        }

        public static DependencyProperty XminProperty = DependencyProperty.Register(
            "Xmin", typeof(double), typeof(LineChartControl), new FrameworkPropertyMetadata(0.0,
                new PropertyChangedCallback(OnDoubleChanged)));
        public double Xmin
        {
            get { return (double)GetValue(XminProperty); }
            set { SetValue(XminProperty, value); }
        }

        public static DependencyProperty XmaxProperty = DependencyProperty.Register(
            "Xmax", typeof(double), typeof(LineChartControl), new FrameworkPropertyMetadata(1.0,
                new PropertyChangedCallback(OnDoubleChanged)));
        public double Xmax
        {
            get { return (double)GetValue(XmaxProperty); }
            set { SetValue(XmaxProperty, value); }
        }

        public static DependencyProperty YminProperty = DependencyProperty.Register(
           "Ymin", typeof(double), typeof(LineChartControl), new FrameworkPropertyMetadata(0.0,
               new PropertyChangedCallback(OnDoubleChanged)));
        public double Ymin
        {
            get { return (double)GetValue(YminProperty); }
            set { SetValue(YminProperty, value); }
        }

        public static DependencyProperty YmaxProperty = DependencyProperty.Register(
            "Ymax", typeof(double), typeof(LineChartControl), new FrameworkPropertyMetadata(1.0,
                new PropertyChangedCallback(OnDoubleChanged)));
        public double Ymax
        {
            get { return (double)GetValue(YmaxProperty); }
            set { SetValue(YmaxProperty, value); }
        }

        public static DependencyProperty XTickProperty = DependencyProperty.Register(
           "XTick", typeof(double), typeof(LineChartControl), new FrameworkPropertyMetadata(0.5,
               new PropertyChangedCallback(OnDoubleChanged)));
        public double XTick
        {
            get { return (double)GetValue(XTickProperty); }
            set { SetValue(XTickProperty, value); }
        }

        public static DependencyProperty YTickProperty = DependencyProperty.Register(
           "YTick", typeof(double), typeof(LineChartControl), new FrameworkPropertyMetadata(0.5,
               new PropertyChangedCallback(OnDoubleChanged)));
        public double YTick
        {
            get { return (double)GetValue(YTickProperty); }
            set { SetValue(YTickProperty, value); }
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(LineChartControl), new FrameworkPropertyMetadata("My Chart",
                new PropertyChangedCallback(OnStringChanged)));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static DependencyProperty XLabelProperty = DependencyProperty.Register(
           "XLabel", typeof(string), typeof(LineChartControl), new FrameworkPropertyMetadata("X Axis",
               new PropertyChangedCallback(OnStringChanged)));
        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set { SetValue(XLabelProperty, value); }
        }

        public static DependencyProperty YLabelProperty = DependencyProperty.Register(
           "YLabel", typeof(string), typeof(LineChartControl), new FrameworkPropertyMetadata("Y Axis",
               new PropertyChangedCallback(OnStringChanged)));
        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set { SetValue(YLabelProperty, value); }
        }

        public static DependencyProperty IsXGridProperty = DependencyProperty.Register(
           "IsXGrid", typeof(bool), typeof(LineChartControl), new FrameworkPropertyMetadata(true,
               new PropertyChangedCallback(OnBoolChanged)));
        public bool IsXGrid
        {
            get { return (bool)GetValue(IsXGridProperty); }
            set { SetValue(IsXGridProperty, value); }
        }

        public static DependencyProperty IsYGridProperty = DependencyProperty.Register(
           "IsYGrid", typeof(bool), typeof(LineChartControl), new FrameworkPropertyMetadata(true,
               new PropertyChangedCallback(OnBoolChanged)));
        public bool IsYGrid
        {
            get { return (bool)GetValue(IsYGridProperty); }
            set { SetValue(IsYGridProperty, value); }
        }

        public static DependencyProperty GridlineColorProperty = DependencyProperty.Register(
          "GridlineColor", typeof(Brush), typeof(LineChartControl), new FrameworkPropertyMetadata(Brushes.Gray,
          new PropertyChangedCallback(OnColorChanged)));
        public Brush GridlineColor
        {
            get { return (Brush)GetValue(GridlineColorProperty); }
            set { SetValue(GridlineColorProperty, value); }
        }
        
        public static DependencyProperty GridlinePatternProperty = DependencyProperty.Register(
            "GridLinePattern", typeof(ChartStyleLineChartControl.GridlinePatternEnum), 
            typeof(LineChartControl), new FrameworkPropertyMetadata(
            ChartStyleLineChartControl.GridlinePatternEnum.Solid,
            new PropertyChangedCallback(OnGridlinePatternChanged)));
        public ChartStyleLineChartControl.GridlinePatternEnum GridlinePattern
        {
            get { return (ChartStyleLineChartControl.GridlinePatternEnum)GetValue(GridlinePatternProperty); }
            set { SetValue(GridlinePatternProperty, value); }
        }

        private static void OnGridlinePatternChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LineChartControl lcc = (LineChartControl)sender;
            lcc.GridlinePattern = (ChartStyleLineChartControl.GridlinePatternEnum)e.NewValue;
        }
        
        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LineChartControl lcc = (LineChartControl)sender;
            lcc.GridlineColor = (Brush)e.NewValue;
        }

        private static void OnDoubleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {  
            LineChartControl lcc = (LineChartControl)sender;
            if (e.Property == XminProperty)
                lcc.Xmin = (double)e.NewValue;
            else if (e.Property == XmaxProperty)
                lcc.Xmax = (double)e.NewValue;
            else if (e.Property == YminProperty)
                lcc.Ymin = (double)e.NewValue;
            else if (e.Property == YmaxProperty)
                lcc.Ymax = (double)e.NewValue;
            else if (e.Property == XTickProperty)
                lcc.XTick = (double)e.NewValue;
            else if (e.Property == YTickProperty)
                lcc.YTick = (double)e.NewValue;
        }

        private static void OnStringChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LineChartControl lcc = (LineChartControl)sender;
            if (e.Property == TitleProperty)
            {
                lcc.Title = (string)e.NewValue;
            }
            else if (e.Property == XLabelProperty)
            {
                lcc.XLabel = (string)e.NewValue;
            }
            else if (e.Property == YLabelProperty)
            {
                lcc.YLabel = (string)e.NewValue;
            }
        }

        private static void OnBoolChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LineChartControl lcc = (LineChartControl)sender;
            if (e.Property == IsXGridProperty)
                lcc.IsXGrid = (bool)e.NewValue;
            else if (e.Property == IsYGridProperty)
                lcc.IsYGrid = (bool)e.NewValue;
        }

    }

    public class ChartStyleLineChartControl
    {
        private double Xmin;
        private double Xmax;
        private double Ymin;
        private double Ymax;
        private string Title;
        private string XLabel;
        private string YLabel;
        private double XTick;
        private double YTick;
        private bool IsXGrid;
        private bool IsYGrid;

        private Brush GridlineColor;
        private GridlinePatternEnum GridlinePattern;

        private double leftOffset = 20;
        private double bottomOffset = 15;
        private double rightOffset = 10;
        private Line gridline = new Line();
        private LineChartControl lcc;
        private Canvas TextCanvas;
        private Canvas ChartCanvas;

        public ChartStyleLineChartControl(LineChartControl lcc)
        {
            this.lcc = lcc;
            TextCanvas = lcc.textCanvas;
            ChartCanvas = lcc.chartCanvas;
            UpdateChartStyle();           
        }

        public void UpdateChartStyle()
        {
            Xmin = lcc.Xmin;
            Xmax = lcc.Xmax;
            Ymin = lcc.Ymin;
            Ymax = lcc.Ymax;
            XTick = lcc.XTick;
            YTick = lcc.YTick;
            Title = lcc.Title;
            XLabel = lcc.XLabel;
            YLabel = lcc.YLabel;
            IsXGrid = lcc.IsXGrid;
            IsYGrid = lcc.IsYGrid;
            GridlineColor = lcc.GridlineColor;
            GridlinePattern = lcc.GridlinePattern;
        }
       
        public void AddChartStyle()
        {
            UpdateChartStyle();

            Point pt = new Point();
            Line tick = new Line();
            double offset = 0;
            double dx, dy;
            TextBlock tb = new TextBlock();

            //  determine right offset:
            tb.Text = Xmax.ToString();
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size size = tb.DesiredSize;
            rightOffset = size.Width / 2 + 2;

            // Determine left offset:
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.TextAlignment = TextAlignment.Right;
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (offset < size.Width)
                    offset = size.Width;
            }
            leftOffset = offset + 5;

            Canvas.SetLeft(ChartCanvas, leftOffset);
            Canvas.SetBottom(ChartCanvas, bottomOffset);
            ChartCanvas.Width = TextCanvas.Width - leftOffset - rightOffset;
            ChartCanvas.Height = TextCanvas.Height - bottomOffset - size.Height / 2;
            Rectangle chartRect = new Rectangle();
            chartRect.Stroke = Brushes.Black;
            chartRect.Width = ChartCanvas.Width;
            chartRect.Height = ChartCanvas.Height;
            ChartCanvas.Children.Add(chartRect);

            // Create vertical gridlines:
            if (IsYGrid == true)
            {
                for (dx = Xmin + XTick; dx < Xmax; dx += XTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = NormalizePoint(new Point(dx, Ymin)).X;
                    gridline.Y1 = NormalizePoint(new Point(dx, Ymin)).Y;
                    gridline.X2 = NormalizePoint(new Point(dx, Ymax)).X;
                    gridline.Y2 = NormalizePoint(new Point(dx, Ymax)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }

            // Create horizontal gridlines:
            if (IsXGrid == true)
            {
                for (dy = Ymin + YTick; dy < Ymax; dy += YTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = NormalizePoint(new Point(Xmin, dy)).X;
                    gridline.Y1 = NormalizePoint(new Point(Xmin, dy)).Y;
                    gridline.X2 = NormalizePoint(new Point(Xmax, dy)).X;
                    gridline.Y2 = NormalizePoint(new Point(Xmax, dy)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }

            // Create x-axis tick marks:
            for (dx = Xmin; dx <= Xmax; dx += XTick)
            {
                pt = NormalizePoint(new Point(dx, Ymin));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X;
                tick.Y2 = pt.Y - 5;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dx.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, leftOffset + pt.X - size.Width / 2);
                Canvas.SetTop(tb, pt.Y + 2 + size.Height / 2);
            }

            // Create y-axis tick marks:
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X + 5;
                tick.Y2 = pt.Y;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetRight(tb, ChartCanvas.Width + 10);
                Canvas.SetTop(tb, pt.Y);
            }
        }

        public void AddLinePattern()
        {
            gridline.Stroke = GridlineColor;
            gridline.StrokeThickness = 1;

            switch (GridlinePattern)
            {
                case GridlinePatternEnum.Dash:
                    gridline.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case GridlinePatternEnum.Dot:
                    gridline.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case GridlinePatternEnum.DashDot:
                    gridline.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
            }
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

        public enum GridlinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4
        }
    }

    public class DataSeriesLineChartControl
    {
        private Polyline lineSeries = new Polyline();
        private Brush lineColor;
        private double lineThickness = 1;
        private LinePatternEnum linePattern;
        private string seriesName = "Default Name";

        public DataSeriesLineChartControl()
        {
            LineColor = Brushes.Black;
        }

        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        public Polyline LineSeries
        {
            get { return lineSeries; }
            set { lineSeries = value; }
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

        public string SeriesName
        {
            get { return seriesName; }
            set { seriesName = value; }
        }

        public void AddLinePattern()
        {
            LineSeries.Stroke = LineColor;
            LineSeries.StrokeThickness = LineThickness;

            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    LineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    LineSeries.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    LineSeries.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    LineSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        public enum LinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }
    }

    public class DataCollectionLineChartControl
    {
        private List<DataSeriesLineChartControl> dataList;
        private LineChartControl lcc;
        private ChartStyleLineChartControl cs;

        public DataCollectionLineChartControl(LineChartControl lcc)
        {
            dataList = new List<DataSeriesLineChartControl>();
            this.lcc = lcc;
            this.cs = lcc.ControlChartStyle;
        }

        public List<DataSeriesLineChartControl> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        public void AddLines()
        {
            lcc.ControlChartStyle.AddChartStyle();
            int j = 0;
            foreach (DataSeriesLineChartControl ds in DataList)
            {
                if (ds.SeriesName == "Default Name")
                {
                    ds.SeriesName = "DataSeries" + j.ToString();
                }
                ds.AddLinePattern();
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {
                    ds.LineSeries.Points[i] = cs.NormalizePoint(ds.LineSeries.Points[i]);
                }
                lcc.chartCanvas.Children.Add(ds.LineSeries);
                j++;
            }
        }
    }
}