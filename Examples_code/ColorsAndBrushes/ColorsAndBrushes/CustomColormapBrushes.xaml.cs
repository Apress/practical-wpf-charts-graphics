using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorsAndBrushes
{
    /// <summary>
    /// Interaction logic for CustomColormapBrushes.xaml
    /// </summary>
    public partial class CustomColormapBrushes : Window
    {
        public CustomColormapBrushes()
        {
            InitializeComponent();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartCanvas.Width = chartGrid.ActualWidth -20 ;
            chartCanvas.Height = chartGrid.ActualHeight - 20;
            chartCanvas.Children.Clear();
            AddColormap();
        }

        private void AddColormap()
        {
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Spring, 0);
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Summer, 1);
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Autumn, 2);
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Winter, 3);
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Gray, 4);
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Jet, 5);
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Hot, 6);
            DrawColorbar(ColormapBrush.ColormapBrushEnum.Cool, 7);
        }

        private void DrawColorbar(ColormapBrush.ColormapBrushEnum brushType, double offset)
        {
            double width = 30.0;
            double height = 20.0;
            ColormapBrush cb = new ColormapBrush();
            cb.Ydivisions = 10;
            cb.ColormapBrushType = brushType;
            SolidColorBrush[] brush = cb.ColormapBrushes();
            Rectangle rect;

            for (int i = 0; i < 10; i++)
            {
                rect = new Rectangle();
                rect.Width = width;
                rect.Height = height;
                Canvas.SetTop(rect, 10 + i * 23);
                Canvas.SetLeft(rect, 10 + 40 * offset);
                rect.Fill = brush[i];
                chartCanvas.Children.Add(rect);
            }
        }
    }
}
