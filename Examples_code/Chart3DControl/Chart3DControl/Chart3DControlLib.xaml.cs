using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Chart3DControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Chart3DControlLib : UserControl
    {
        private ChartStyle2D cs;
        private Bar3DStyle ds;
        private Draw3DChart d3c;
        private ChartTypeEnum chartType;

        public Chart3DControlLib()
        {
            InitializeComponent();
            this.cs = new ChartStyle2D();
            this.ds = new Bar3DStyle();
            this.d3c = new Draw3DChart();
            this.cs.ChartCanvas = chartCanvas;
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartCanvas.Width = chartGrid.ActualWidth;
            chartCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            AddChart();
        }

        public enum ChartTypeEnum
        {
            Surface = 1,
            Mesh = 2,
            MeshZ = 3,
            Waterfall = 4,
            XYColor = 5,
            Contour = 6,
            FillContour = 7,
            XYColor3D = 8,
            MeshContour3D = 9,
            SurfaceContour3D = 10,
            SurfaceFillContour3D = 11,
            BarChart3D = 12
        }

        public void AddChart()
        {
            switch (ChartType)
            {
                case ChartTypeEnum.Mesh:
                    d3c.SurfaceChartType = DrawSurfaceChart.SurfaceChartTypeEnum.Mesh;
                    d3c.AddSurfaceChart(cs, ds);
                    break;

                case ChartTypeEnum.MeshZ:
                    d3c.SurfaceChartType = DrawSurfaceChart.SurfaceChartTypeEnum.MeshZ;
                    d3c.AddSurfaceChart(cs, ds);
                    break;

                case ChartTypeEnum.Waterfall:
                    d3c.SurfaceChartType = DrawSurfaceChart.SurfaceChartTypeEnum.Waterfall;
                    d3c.AddSurfaceChart(cs, ds);
                    break;

                case ChartTypeEnum.Surface:
                    d3c.SurfaceChartType = DrawSurfaceChart.SurfaceChartTypeEnum.Surface;
                    d3c.AddSurfaceChart(cs, ds);
                    break;

                case ChartTypeEnum.XYColor:
                    d3c.ChartType = Draw3DChart.ChartTypeEnum.XYColor;
                    d3c.AddSpecializedChart(cs, ds);
                    break;

                case ChartTypeEnum.Contour:
                    d3c.ChartType = Draw3DChart.ChartTypeEnum.Contour;
                    d3c.AddSpecializedChart(cs, ds);
                    break;

                case ChartTypeEnum.FillContour:
                    d3c.ChartType = Draw3DChart.ChartTypeEnum.FillContour;
                    d3c.AddSpecializedChart(cs, ds);
                    break;

                case ChartTypeEnum.MeshContour3D:
                    d3c.ChartType = Draw3DChart.ChartTypeEnum.MeshContour3D;
                    d3c.AddSpecializedChart(cs, ds);
                    break;

                case ChartTypeEnum.SurfaceContour3D:
                    d3c.ChartType = Draw3DChart.ChartTypeEnum.SurfaceContour3D;
                    d3c.AddSpecializedChart(cs, ds);
                    break;

                case ChartTypeEnum.SurfaceFillContour3D:
                    d3c.ChartType = Draw3DChart.ChartTypeEnum.SurfaceFillContour3D;
                    d3c.AddSpecializedChart(cs, ds);
                    break;
                default:
                    cs.AddChartStyle();
                    break;
            }
        }

        public ChartTypeEnum ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        public ChartStyle2D ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }

        public Bar3DStyle DataSeries
        {
            get { return ds; }
            set { ds = value; }
        }

        public Draw3DChart Draw3DChart
        {
            get { return d3c; }
            set { d3c = value; }
        }

        public static DependencyProperty XminProperty = DependencyProperty.Register("Xmin", typeof(double), typeof(Chart3DControlLib),
           new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnDoubleChanged)));

        public double Xmin
        {
            get { return (double)GetValue(XminProperty); }
            set
            {
                SetValue(XminProperty, value);
                cs.Xmin = value;
            }
        }

        public static DependencyProperty XmaxProperty = DependencyProperty.Register("Xmax", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnDoubleChanged)));

        public double Xmax
        {
            get { return (double)GetValue(XmaxProperty); }
            set
            {
                SetValue(XmaxProperty, value);
                cs.Xmax = value;
            }
        }

        public static DependencyProperty YminProperty = DependencyProperty.Register("Ymin", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnDoubleChanged)));

        public double Ymin
        {
            get { return (double)GetValue(YminProperty); }
            set
            {
                SetValue(YminProperty, value);
                cs.Ymin = value;
            }
        }

        public static DependencyProperty YmaxProperty = DependencyProperty.Register("Ymax", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnDoubleChanged)));

        public double Ymax
        {
            get { return (double)GetValue(YmaxProperty); }
            set
            {
                SetValue(YmaxProperty, value);
                cs.Ymax = value;
            }
        }

        public static DependencyProperty ZminProperty = DependencyProperty.Register("Zmin", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnDoubleChanged)));

        public double Zmin
        {
            get { return (double)GetValue(ZminProperty); }
            set
            {
                SetValue(ZminProperty, value);
                cs.Zmin = value;
            }
        }

        public static DependencyProperty ZmaxProperty = DependencyProperty.Register("Zmax", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnDoubleChanged)));

        public double Zmax
        {
            get { return (double)GetValue(ZmaxProperty); }
            set
            {
                SetValue(ZmaxProperty, value);
                cs.Zmax = value;
            }
        }

        public static DependencyProperty XTickProperty = DependencyProperty.Register("XTick", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(0.2, new PropertyChangedCallback(OnDoubleChanged)));

        public double XTick
        {
            get { return (double)GetValue(XTickProperty); }
            set
            {
                SetValue(XTickProperty, value);
                cs.XTick = value;
            }
        }

        public static DependencyProperty YTickProperty = DependencyProperty.Register("YTick", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(0.2, new PropertyChangedCallback(OnDoubleChanged)));

        public double YTick
        {
            get { return (double)GetValue(YTickProperty); }
            set
            {
                SetValue(YTickProperty, value);
                cs.YTick = value;
            }
        }

        public static DependencyProperty ZTickProperty = DependencyProperty.Register("ZTick", typeof(double), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(0.2, new PropertyChangedCallback(OnDoubleChanged)));

        public double ZTick
        {
            get { return (double)GetValue(ZTickProperty); }
            set
            {
                SetValue(ZTickProperty, value);
                cs.ZTick = value;
            }
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata("No Title", new PropertyChangedCallback(OnStringChanged)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
                cs.Title = value;
            }
        }

        public static DependencyProperty XLabelProperty = DependencyProperty.Register("XLabel", typeof(string), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata("X Axis", new PropertyChangedCallback(OnStringChanged)));

        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set
            {
                SetValue(XLabelProperty, value);
                cs.XLabel = value;
            }
        }

        public static DependencyProperty YLabelProperty = DependencyProperty.Register("YLabel", typeof(string), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata("Y Axis", new PropertyChangedCallback(OnStringChanged)));

        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set
            {
                SetValue(YLabelProperty, value);
                cs.YLabel = value;
            }
        }

        public static DependencyProperty ZLabelProperty = DependencyProperty.Register("ZLabel", typeof(string), typeof(Chart3DControlLib),
           new FrameworkPropertyMetadata("Z Axis", new PropertyChangedCallback(OnStringChanged)));

        public string ZLabel
        {
            get { return (string)GetValue(ZLabelProperty); }
            set
            {
                SetValue(ZLabelProperty, value);
                cs.ZLabel = value;
            }
        }

        public static DependencyProperty IsXGridProperty = DependencyProperty.Register("IsXGrid", typeof(bool), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnBoolChanged)));

        public bool IsXGrid
        {
            get { return (bool)GetValue(IsXGridProperty); }
            set
            {
                SetValue(IsXGridProperty, value);
                cs.IsXGrid = value;
            }
        }

        public static DependencyProperty IsYGridProperty = DependencyProperty.Register("IsYGrid", typeof(bool), typeof(Chart3DControlLib),
           new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnBoolChanged)));

        public bool IsYGrid
        {
            get { return (bool)GetValue(IsYGridProperty); }
            set
            {
                SetValue(IsYGridProperty, value);
                cs.IsYGrid = value;
            }
        }

        public static DependencyProperty IsZGridProperty = DependencyProperty.Register("IsZGrid", typeof(bool), typeof(Chart3DControlLib),
          new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnBoolChanged)));

        public bool IsZGrid
        {
            get { return (bool)GetValue(IsZGridProperty); }
            set
            {
                SetValue(IsZGridProperty, value);
                cs.IsZGrid = value;
            }
        }

        public static DependencyProperty GridlineColorProperty = DependencyProperty.Register("GridlineColor", typeof(Brush), typeof(Chart3DControlLib),
           new FrameworkPropertyMetadata(Brushes.LightGray, new PropertyChangedCallback(OnColorChanged)));

        public Brush GridlineColor
        {
            get { return (Brush)GetValue(GridlineColorProperty); }
            set
            {
                SetValue(GridlineColorProperty, value);
                cs.GridlineColor = value;
            }
        }

        public static DependencyProperty GridlineThicknessProperty = DependencyProperty.Register("GridlineThickness", typeof(double), typeof(Chart3DControlLib),
          new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnDoubleChanged)));

        public double GridlineThickness
        {
            get { return (double)GetValue(GridlineThicknessProperty); }
            set
            {
                SetValue(GridlineThicknessProperty, value);
                cs.GridlineThickness = value;
            }
        }

       public static DependencyProperty GridlinePatternProperty = DependencyProperty.Register("GridlinePattern",
            typeof(ChartStyle.GridlinePatternEnum), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(ChartStyle2D.GridlinePatternEnum.Solid,
            new PropertyChangedCallback(OnGridlinePatternChanged)));

        public ChartStyle.GridlinePatternEnum GridlinePattern
        {
            get { return (ChartStyle.GridlinePatternEnum)GetValue(GridlinePatternProperty); }
            set
            {
                SetValue(GridlinePatternProperty, value);
                cs.GridlinePattern = value;
            }
        }

        public static DependencyProperty IsHiddenLineProperty = DependencyProperty.Register("IsHiddenLine", typeof(bool), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnBoolChanged)));

        public bool IsHiddenLine
        {
            get { return (bool)GetValue(IsHiddenLineProperty); }
            set
            {
                SetValue(IsHiddenLineProperty, value);
                d3c.IsHiddenLine = value;
            }
        }

        public static DependencyProperty IsColorBarProperty = DependencyProperty.Register("IsColorBar", typeof(bool), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnBoolChanged)));

        public bool IsColorBar
        {
            get { return (bool)GetValue(IsColorBarProperty); }
            set
            {
                SetValue(IsColorBarProperty, value);
                cs.IsColorBar = value;
            }
        }

        public static DependencyProperty IsColormapProperty = DependencyProperty.Register("IsColormap", typeof(bool), typeof(Chart3DControlLib),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnBoolChanged)));

        public bool IsColormap
        {
            get { return (bool)GetValue(IsColormapProperty); }
            set
            {
                SetValue(IsColormapProperty, value);
                d3c.IsColormap = value;
            }
        }

        private static void OnDoubleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Chart3DControlLib cc = sender as Chart3DControlLib;
            if (e.Property == XminProperty)
                cc.Xmin = (double)e.NewValue;
            else if (e.Property == XmaxProperty)
                cc.Xmax = (double)e.NewValue;
            else if (e.Property == YminProperty)
                cc.Ymin = (double)e.NewValue;
            else if (e.Property == YmaxProperty)
                cc.Ymax = (double)e.NewValue;
            else if (e.Property == ZminProperty)
                cc.Zmin = (double)e.NewValue;
            else if (e.Property == ZmaxProperty)
                cc.Zmax = (double)e.NewValue;
            else if (e.Property == XTickProperty)
                cc.XTick = (double)e.NewValue;
            else if (e.Property == YTickProperty)
                cc.YTick = (double)e.NewValue;
            else if (e.Property == ZTickProperty)
                cc.ZTick = (double)e.NewValue;
            else if (e.Property == GridlineThicknessProperty)
                cc.GridlineThickness = (double)e.NewValue;
        }

        private static void OnStringChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Chart3DControlLib cc = sender as Chart3DControlLib;
            if (e.Property == TitleProperty)
                cc.Title = (string)e.NewValue;
            else if (e.Property == XLabelProperty)
                cc.XLabel = (string)e.NewValue;
            else if (e.Property == YLabelProperty)
                cc.YLabel = (string)e.NewValue;
            else if (e.Property == ZLabelProperty)
                cc.ZLabel = (string)e.NewValue;
        }

        private static void OnBoolChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Chart3DControlLib cc = sender as Chart3DControlLib;
            if (e.Property == IsXGridProperty)
                cc.IsXGrid = (bool)e.NewValue;
            else if (e.Property == IsYGridProperty)
                cc.IsYGrid = (bool)e.NewValue;
            else if (e.Property == IsZGridProperty)
                cc.IsZGrid = (bool)e.NewValue;
            else if (e.Property == IsHiddenLineProperty)
                cc.IsHiddenLine = (bool)e.NewValue;
            else if (e.Property == IsColorBarProperty)
                cc.IsColorBar = (bool)e.NewValue;
            else if (e.Property == IsColormapProperty)
                cc.IsColormap = (bool)e.NewValue;
        }

        private static void OnGridlinePatternChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Chart3DControlLib cc = sender as Chart3DControlLib;
            cc.GridlinePattern = (ChartStyle2D.GridlinePatternEnum)e.NewValue;
        }

        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Chart3DControlLib cc = sender as Chart3DControlLib;
            cc.GridlineColor = (Brush)e.NewValue;
        }
    }
}

