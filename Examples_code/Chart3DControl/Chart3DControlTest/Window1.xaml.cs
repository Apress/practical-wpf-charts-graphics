using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chart3DControlTest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void rootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myChart3D.Width = rootGrid.ActualWidth;
            myChart3D.Height = rootGrid.ActualHeight;
            AddChart();
        }

        private void AddChart()
        {
            myChart3D.IsColorBar = true;
            myChart3D.IsColormap = true;
            //myChart3D.Draw3DChart.IsLineColorMatch = true;
            myChart3D.DataSeries.LineColor = Brushes.Black;
            myChart3D.DataSeries.LineThickness = 1;
            myChart3D.Draw3DChart.IsInterp = true;
            myChart3D.Draw3DChart.NumberContours = 15;
            myChart3D.Draw3DChart.NumberInterp = 2;

            Utility.Peak3D(myChart3D.ChartStyle, myChart3D.DataSeries);
            myChart3D.ChartType = Chart3DControl.Chart3DControlLib.ChartTypeEnum.SurfaceFillContour3D;
        }
    }
}
