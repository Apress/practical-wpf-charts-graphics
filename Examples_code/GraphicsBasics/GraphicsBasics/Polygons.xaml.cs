using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicsBasics
{
    /// <summary>
    /// Interaction logic for Polygons.xaml
    /// </summary>
    public partial class Polygons : Window
    {
        public Polygons()
        {
            InitializeComponent();
            for (int i = 0; i < 71; i++)
            {
                double x = i * Math.PI;
                double y = 40 + 30 * Math.Sin(x / 10);
                polygon1.Points.Add(new Point(x, y));
            }
        }
    }
}
