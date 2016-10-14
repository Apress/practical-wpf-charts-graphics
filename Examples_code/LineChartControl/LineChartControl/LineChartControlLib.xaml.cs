using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LineCharts;

namespace LineChartControl
{
    /// <summary>
    /// Interaction logic for LineChartControl.xaml
    /// </summary>
    public partial class LineChartControlLib : UserControl
    {
        private ChartStyleGridlines cs;
        private DataCollection dc;
        private DataSeries ds;
        private Legend lg;

        public LineChartControlLib()
        {
            InitializeComponent();
            this.cs = new ChartStyleGridlines();
            this.dc = new DataCollection();
            this.ds = new DataSeries();
            this.lg = new Legend();
            cs.TextCanvas = textCanvas;
            cs.ChartCanvas = chartCanvas;
            lg.LegendCanvas = legendCanvas;
        }
        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            legendCanvas.Children.Clear();
            chartCanvas.Children.RemoveRange(1, chartCanvas.Children.Count - 1);
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart();
        }

        private void AddChart()
        {
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);
            if (dc.DataList.Count != 0)
            {
                dc.AddLines(cs);
                Legend.AddLegend(chartCanvas, dc);
            }
        }

        public ChartStyleGridlines ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public DataCollection DataCollection
        {
            get { return dc; }
            set { dc = value; }
        }

        public DataSeries DataSeries
        {
            get { return ds; }
            set { ds = value; }
        }

        public Legend Legend
        {
            get { return lg; }
            set { lg = value; }
        }

        public static DependencyProperty XminProperty = DependencyProperty.Register("Xmin", typeof(double), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnPropertyChanged)));

        public double Xmin
        {
            get { return (double)GetValue(XminProperty); }
            set
            {
                SetValue(XminProperty, value);
                cs.Xmin = value;
            }
        }

        public static DependencyProperty XmaxProperty = DependencyProperty.Register("Xmax", typeof(double), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(10.0, new PropertyChangedCallback(OnPropertyChanged)));

        public double Xmax
        {
            get { return (double)GetValue(XmaxProperty); }
            set
            {
                SetValue(XmaxProperty, value);
                cs.Xmax = value;
            }
        }

        public static DependencyProperty YminProperty = DependencyProperty.Register("Ymin", typeof(double), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnPropertyChanged)));

        public double Ymin
        {
            get { return (double)GetValue(YminProperty); }
            set
            {
                SetValue(YminProperty, value);
                cs.Ymin = value;
            }
        }

        public static DependencyProperty YmaxProperty = DependencyProperty.Register("Ymax", typeof(double), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(10.0, new PropertyChangedCallback(OnPropertyChanged)));

        public double Ymax
        {
            get { return (double)GetValue(YmaxProperty); }
            set
            {
                SetValue(YmaxProperty, value);
                cs.Ymax = value;
            }
        }

        public static DependencyProperty XTickProperty = DependencyProperty.Register("XTick", typeof(double), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(2.0, new PropertyChangedCallback(OnPropertyChanged)));

        public double XTick
        {
            get { return (double)GetValue(XTickProperty); }
            set
            {
                SetValue(XTickProperty, value);
                cs.XTick = value;
            }
        }

        public static DependencyProperty YTickProperty = DependencyProperty.Register("YTick", typeof(double), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(2.0, new PropertyChangedCallback(OnPropertyChanged)));

        public double YTick
        {
            get { return (double)GetValue(YTickProperty); }
            set
            {
                SetValue(YTickProperty, value);
                cs.YTick = value;
            }
        }

        public static DependencyProperty XLabelProperty = DependencyProperty.Register("XLabel", typeof(string), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata("X Axis", new PropertyChangedCallback(OnPropertyChanged)));

        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set
            {
                SetValue(XLabelProperty, value);
                cs.XLabel = value;
            }
        }

        public static DependencyProperty YLabelProperty = DependencyProperty.Register("YLabel", typeof(string), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata("Y Axis", new PropertyChangedCallback(OnPropertyChanged)));

        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set
            {
                SetValue(YLabelProperty, value);
                cs.YLabel = value;
            }
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata("Title", new PropertyChangedCallback(OnPropertyChanged)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
                cs.Title = value;
            }
        }

        public static DependencyProperty IsXGridProperty = DependencyProperty.Register("IsXGrid", typeof(bool), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnPropertyChanged)));

        public bool IsXGrid
        {
            get { return (bool)GetValue(IsXGridProperty); }
            set
            {
                SetValue(IsXGridProperty, value);
                cs.IsXGrid = value;
            }
        }

        public static DependencyProperty IsYGridProperty = DependencyProperty.Register("IsYGrid", typeof(bool), typeof(LineChartControlLib),
           new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnPropertyChanged)));

        public bool IsYGrid
        {
            get { return (bool)GetValue(IsYGridProperty); }
            set
            {
                SetValue(IsYGridProperty, value);
                cs.IsYGrid = value;
            }
        }

        public static DependencyProperty GridlineColorProperty = DependencyProperty.Register("GridlineColor", typeof(Brush), typeof(LineChartControlLib),
           new FrameworkPropertyMetadata(Brushes.Gray, new PropertyChangedCallback(OnPropertyChanged)));

        public Brush GridlineColor
        {
            get { return (Brush)GetValue(GridlineColorProperty); }
            set
            {
                SetValue(GridlineColorProperty, value);
                cs.GridlineColor = value;
            }
        }

        public static DependencyProperty GridlinePatternProperty = DependencyProperty.Register("GridlinePattern",
            typeof(ChartStyleGridlines.GridlinePatternEnum), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(ChartStyleGridlines.GridlinePatternEnum.Solid,
            new PropertyChangedCallback(OnPropertyChanged)));

        public ChartStyleGridlines.GridlinePatternEnum GridlinePattern
        {
            get { return (ChartStyleGridlines.GridlinePatternEnum)GetValue(GridlinePatternProperty); }
            set
            {
                SetValue(GridlinePatternProperty, value);
                cs.GridlinePattern = value;
            }
        }

        public static DependencyProperty IsLegendProperty = DependencyProperty.Register("IsLegend", typeof(bool), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnPropertyChanged)));

        public bool IsLegend
        {
            get { return (bool)GetValue(IsLegendProperty); }
            set
            {
                SetValue(IsLegendProperty, value);
                lg.IsLegend = value;
            }
        }

        public static DependencyProperty LegendPositionProperty = DependencyProperty.Register("LegendPosition",
            typeof(Legend.LegendPositionEnum), typeof(LineChartControlLib),
            new FrameworkPropertyMetadata(Legend.LegendPositionEnum.NorthEast,
            new PropertyChangedCallback(OnPropertyChanged)));

        public Legend.LegendPositionEnum LegendPosition
        {
            get { return (Legend.LegendPositionEnum)GetValue(LegendPositionProperty); }
            set
            {
                SetValue(LegendPositionProperty, value);
                lg.LegendPosition = value;
            }
        }

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LineChartControlLib lcc = sender as LineChartControlLib;
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
            else if (e.Property == GridlinePatternProperty)
                lcc.GridlinePattern = (ChartStyleGridlines.GridlinePatternEnum)e.NewValue;
            else if (e.Property== GridlineColorProperty)
                lcc.GridlineColor = (Brush)e.NewValue;
            else if (e.Property == TitleProperty)
                lcc.Title = (string)e.NewValue;
            else if (e.Property == XLabelProperty)
                lcc.XLabel = (string)e.NewValue;
            else if (e.Property == YLabelProperty)
                lcc.YLabel = (string)e.NewValue;
            else if (e.Property == IsXGridProperty)
                lcc.IsXGrid = (bool)e.NewValue;
            else if (e.Property == IsYGridProperty)
                lcc.IsYGrid = (bool)e.NewValue;
            else if (e.Property == IsLegendProperty)
                lcc.IsLegend = (bool)e.NewValue;
            else if(e.Property== LegendPositionProperty)
                lcc.LegendPosition = (Legend.LegendPositionEnum)e.NewValue;
        }
        
        
        
       
    }
}

