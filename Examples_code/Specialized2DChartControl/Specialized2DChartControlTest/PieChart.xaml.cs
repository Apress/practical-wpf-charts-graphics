using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Specialized2DChartControlTest
{
    /// <summary>
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : Window
    {
        public PieChart()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myPieChart.Height = rootGrid.ActualHeight;
            myPieChart.Width = rootGrid.ActualWidth;
            AddData();
        }

        private void AddData()
        {
            myPieChart.PieStyle = new Specialized2DCharts.PieStyle();
            myPieChart.PieLegend = new Specialized2DCharts.PieLegend();
            double[] data = new double[] { 30, 35, 15, 10, 8 };
            int[] explode = new int[] { 20, 0, 20, 0, 0 };
            string[] labels = new string[] { "Soc. Sec. Tax", "Income Tax", "Borrowing", "Corp. Tax", "Misc." };
            for (int i = 0; i < data.Length; i++)
            {
                myPieChart.PieStyle.DataList.Add(data[i]);
                myPieChart.PieStyle.ExplodeList.Add(explode[i]);
                myPieChart.PieStyle.LabelList.Add(labels[i]);
            }
            myPieChart.PieStyle.ColormapBrushes.ColormapBrushType= Specialized2DCharts.ColormapBrush.ColormapBrushEnum.Summer;          
            myPieChart.PieLegend.IsLegendVisible = true;
        }
    }
}
