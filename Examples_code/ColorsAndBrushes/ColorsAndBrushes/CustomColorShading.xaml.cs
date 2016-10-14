using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorsAndBrushes
{
    /// <summary>
    /// Interaction logic for CustomColorShading.xaml
    /// </summary>
    public partial class CustomColorShading : Window
    {
        public CustomColorShading()
        {
            InitializeComponent();
            AddShading();
        }

        public void AddShading()
        {
            chartCanvas.Children.Clear();
            double width = chartCanvas.Width / 2 - 20;
            double height = chartCanvas.Height - 20;

            BilinearInterpolation bi = new BilinearInterpolation();
            bi.Cdivisions = 32;
            bi.NInterps = 20;
            bi.Cmin = -3;
            bi.Cmax = 4;
            bi.ChartCanvas = chartCanvas;

            // Original color map:
            double x0 = 10;
            double y0 = 10;
            bi.SetOriginalShading(3, x0, y0, x0+width/2, y0+height/2);

            x0 = 10 + width / 2;
            bi.SetOriginalShading(0, x0, y0, x0+width/2, y0+height/2);

            x0 = 10;
            y0 = 10 + height / 2;
            bi.SetOriginalShading(-2, x0, y0, x0 + width / 2, y0 + height / 2);
            
            x0 = 10 + width / 2;
            y0 = 10 + height / 2;
            bi.SetOriginalShading(3, x0, y0, x0 + width / 2, y0 + height / 2);

            // Bilinear interpolation:
            x0 = 20 + width;
            y0 = 10;
            bi.SetInterpShading(x0, y0, x0 + width / 2, y0 + height / 2, 3, 0, -2, 3);

            x0 = x0 + width / 2;
            bi.SetInterpShading(x0, y0, x0 + width / 2, y0 + height / 2, 0, 4, 3, 1);
           
            x0 = 20 + width;
            y0 = 10 + height / 2;
            bi.SetInterpShading(x0, y0, x0 + width / 2, y0 + height / 2, -2, 3, -1, 2);

            x0 = x0 + width / 2;
            y0 = 10 + height / 2;
            bi.SetInterpShading(x0, y0, x0 + width / 2, y0 + height / 2, 3, 1, 2, -3);


        }
    }
}
