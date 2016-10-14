using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for MultipleCharts.xaml
    /// </summary>
    public partial class MultipleCharts : Window
    {
        public MultipleCharts()
        {
            InitializeComponent();
        }

        private void grid1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chart1.Width = grid1.ActualWidth;
            chart1.Height = grid1.ActualHeight;
            AddData1();
        }

        private void grid2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chart2.Width = grid2.ActualWidth;
            chart2.Height = grid2.ActualHeight;
            AddData2();
        }
        

        private void grid3_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chart3.Width = grid3.ActualWidth;
            chart3.Height = grid3.ActualHeight;
            AddData3();
        }

        private void grid4_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chart4.Width = grid4.ActualWidth;
            chart4.Height = grid4.ActualHeight;
            AddData4();
        }

        private void grid5_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chart5.Width = grid5.ActualWidth;
            chart5.Height = grid5.ActualHeight;
            AddData5();
        }

        private void grid6_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chart6.Width = grid6.ActualWidth;
            chart6.Height = grid6.ActualHeight;
            AddData6();
        }

         private void AddData1()
        {
            chart1.DataCollection.DataList.Clear();
            Specialized2DCharts.DataSeries ds = new Specialized2DCharts.DataSeries();

            chart1.ChartStyle.Title = "Stem Chart";
            chart1.ChartStyle.Xmin = 0;
            chart1.ChartStyle.Xmax = 8;
            chart1.ChartStyle.Ymin = -1.5;
            chart1.ChartStyle.Ymax = 1.5;
            chart1.ChartStyle.XTick = 1;
            chart1.ChartStyle.YTick = 0.5;
            chart1.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
            chart1.ChartStyle.GridlineColor = Brushes.Black;

            // Generate the stair step data:
            for (int i = 0; i < 50; i++)
            {
                ds.LineSeries.Points.Add(new Point(0.4 * i, Math.Sin(0.4 * i)));
            }

            ds.LineColor = Brushes.Red;
            ds.Symbols.SymbolType = Specialized2DCharts.Symbols.SymbolTypeEnum.Diamond;
            ds.Symbols.FillColor = Brushes.Yellow;
            ds.Symbols.BorderColor = Brushes.DarkGreen;
            chart1.DataCollection.DataList.Add(ds);
        }

         private void AddData2()
         {
             chart2.DataCollection.DataList.Clear();
             chart2.ChartStyle.Title = "Stair Step Chart";
             Specialized2DCharts.DataSeriesStairstep ds = new Specialized2DCharts.DataSeriesStairstep();


             chart2.ChartStyle.Xmin = 0;
             chart2.ChartStyle.Xmax = 8;
             chart2.ChartStyle.Ymin = -1.5;
             chart2.ChartStyle.Ymax = 1.5;
             chart2.ChartStyle.XTick = 1;
             chart2.ChartStyle.YTick = 0.5;
             chart2.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
             chart2.ChartStyle.GridlineColor = Brushes.Black;

             // Draw the stair step chart:
             ds.StairstepLineColor = Brushes.Red;
             for (int i = 0; i < 50; i++)
             {
                 ds.LineSeries.Points.Add(new Point(0.4 * i, Math.Sin(0.4 * i)));
             }

             ds.LineColor = Brushes.Black;
             ds.LinePattern = Specialized2DCharts.DataSeries.LinePatternEnum.Dash;
             ds.Symbols.SymbolType = Specialized2DCharts.Symbols.SymbolTypeEnum.Circle;
             chart2.DataCollection.DataList.Add(ds);
         }

         private void AddData3()
         {
             chart3.DataCollection.AreaList.Clear();
             Specialized2DCharts.DataSeriesArea area = new Specialized2DCharts.DataSeriesArea();
             chart3.ChartStyle.Title = "Area Chart";
             chart3.ChartStyle.Xmin = 0;
             chart3.ChartStyle.Xmax = 10;
             chart3.ChartStyle.Ymin = 0;
             chart3.ChartStyle.Ymax = 10;
             chart3.ChartStyle.XTick = 2;
             chart3.ChartStyle.YTick = 2;
             chart3.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
             chart3.ChartStyle.GridlineColor = Brushes.Black;

             // Add sine data:
             area = new Specialized2DCharts.DataSeriesArea();
             area.BorderColor = Brushes.Black;
             area.FillColor = Brushes.LightPink;
             for (int i = 0; i < 21; i++)
             {
                 area.AreaSeries.Points.Add(new Point(0.5 * i, 2.0 + Math.Sin(0.5 * i)));
             }
             chart3.DataCollection.AreaList.Add(area);

             // Add cosine data:
             area = new Specialized2DCharts.DataSeriesArea();
             area.BorderColor = Brushes.Black;
             area.FillColor = Brushes.LightBlue;
             for (int i = 0; i < 21; i++)
             {
                 area.AreaSeries.Points.Add(new Point(0.5 * i, 2.0 + Math.Cos(0.5 * i)));
             }
             chart3.DataCollection.AreaList.Add(area);

             // Add another sine data:
             area = new Specialized2DCharts.DataSeriesArea();
             area.BorderColor = Brushes.Black;
             area.FillColor = Brushes.LightGreen;
             for (int i = 0; i < 21; i++)
             {
                 area.AreaSeries.Points.Add(new Point(0.5 * i, 3.0 + Math.Sin(0.5 * i)));
             }
             chart3.DataCollection.AreaList.Add(area);
         }

         private void AddData4()
         {
             chart4.DataCollection.DataList.Clear();
             chart4.DataCollection.ErrorList.Clear();
             Specialized2DCharts.DataSeriesErrorbar ds = new Specialized2DCharts.DataSeriesErrorbar();

             chart4.ChartStyle.Title = "Error Bar Chart";
             chart4.ChartStyle.Xmin = 0;
             chart4.ChartStyle.Xmax = 12;
             chart4.ChartStyle.Ymin = -1;
             chart4.ChartStyle.Ymax = 6;
             chart4.ChartStyle.XTick = 2;
             chart4.ChartStyle.YTick = 1;
             chart4.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
             chart4.ChartStyle.GridlineColor = Brushes.Black;

             ds.LineColor = Brushes.Blue;
             ds.Symbols.SymbolType = Specialized2DCharts.Symbols.SymbolTypeEnum.Circle;
             ds.Symbols.BorderColor = Brushes.DarkGreen;
             ds.ErrorLineColor = Brushes.Red;

             for (int i = 2; i < 22; i++)
             {
                 ds.LineSeries.Points.Add(new Point(0.5 * i, 10.0 * Math.Exp(-0.5 * i)));
                 ds.ErrorLineSeries.Points.Add(new Point(0.5 * i, 3.0 / (0.5 * i)));
             }
             chart4.DataCollection.DataList.Add(ds);
         }

         private void AddData5()
         {
             chart5.DataCollection.DataList.Clear();
             Specialized2DCharts.DataSeriesBar ds = new Specialized2DCharts.DataSeriesBar();
             chart5.DataCollection.BarType = Specialized2DCharts.DataCollectionBar.BarTypeEnum.Vertical;
             chart5.ChartStyle.Title = "Bar Chart";
             chart5.ChartStyle.Xmin = 0;
             chart5.ChartStyle.Xmax = 5;
             chart5.ChartStyle.Ymin = 0;
             chart5.ChartStyle.Ymax = 10;
             chart5.ChartStyle.XTick = 1;
             chart5.ChartStyle.YTick = 2;
             chart5.ChartStyle.GridlinePattern = Specialized2DCharts.ChartStyleGridlines.GridlinePatternEnum.Dot;
             chart5.ChartStyle.GridlineColor = Brushes.Black;

             // Add the first bar series:
             ds = new Specialized2DCharts.DataSeriesBar();
             ds.BorderColor = Brushes.Red;
             ds.FillColor = Brushes.Green;
             ds.BarWidth = 0.9;

             for (int i = 0; i < 5; i++)
             {
                 double x = i + 1.0;
                 double y = 2.0 * x;
                 ds.LineSeries.Points.Add(new Point(x, y));
             }
             chart5.DataCollection.DataList.Add(ds);

             // Add the second bar series:
             ds = new Specialized2DCharts.DataSeriesBar();
             ds.BorderColor = Brushes.Red;
             ds.FillColor = Brushes.Yellow;
             ds.BarWidth = 0.9;

             for (int i = 0; i < 5; i++)
             {
                 double x = i + 1.0;
                 double y = 1.5 * x;
                 ds.LineSeries.Points.Add(new Point(x, y));
             }
             chart5.DataCollection.DataList.Add(ds);

             // Add the third bar series:
             ds = new Specialized2DCharts.DataSeriesBar();
             ds.BorderColor = Brushes.Red;
             ds.FillColor = Brushes.Blue;
             ds.BarWidth = 0.9;

             for (int i = 0; i < 5; i++)
             {
                 double x = i + 1.0;
                 double y = 1.0 * x;
                 ds.LineSeries.Points.Add(new Point(x, y));
             }
             chart5.DataCollection.DataList.Add(ds);
             chart5.DataCollection.AddBars(chart5.ChartStyle);
         }


         private void AddData6()
         {
             chart6.DataCollection.DataList.Clear();
             Specialized2DCharts.DataSeries ds = new Specialized2DCharts.DataSeries();

             chart6.ChartStyle.Rmax = 0.5;
             chart6.ChartStyle.Rmin = 0;
             chart6.ChartStyle.NTicks = 4;
             chart6.ChartStyle.AngleStep = 30;
             chart6.ChartStyle.AngleDirection = Specialized2DCharts.ChartStylePolar.AngleDirectionEnum.CounterClockWise;
             chart6.ChartStyle.LinePattern = Specialized2DCharts.ChartStylePolar.LinePatternEnum.Dot;
             chart6.ChartStyle.LineColor = Brushes.Black;

             ds.LineColor = Brushes.Red;
             for (int i = 0; i < 360; i++)
             {
                 double theta = 1.0 * i;
                 double r = Math.Abs(Math.Cos(2.0 * theta * Math.PI / 180) * Math.Sin(2.0 * theta * Math.PI / 180));
                 ds.LineSeries.Points.Add(new Point(theta, r));
             }
             chart6.DataCollection.DataList.Add(ds);
         }
    }
}
