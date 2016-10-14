using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicsBasics
{
    /// <summary>
    /// Interaction logic for Polylines.xaml
    /// </summary>
    public partial class Polylines : Window
    {
        public Polylines()
        {
            InitializeComponent();

            for (int i = 0; i < 71; i++)
            {
                double x = i * Math.PI;
                double y = 40 + 30 * Math.Sin(x/10);
                polyline1.Points.Add(new Point(x, y));
            }
        }
    }
}
