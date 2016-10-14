using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphicsBasics
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void defaultLine_Click(object sender, RoutedEventArgs e)
        {
            LineInDefaultSystem line = new LineInDefaultSystem();
            line.ShowDialog();
        }

        private void customLine_Click(object sender, RoutedEventArgs e)
        {
            LineInCustomSystem line = new LineInCustomSystem();
            line.ShowDialog();
        }

        private void scaleCustom_Click(object sender, RoutedEventArgs e)
        {
            ScaleInCustomSystem scale = new ScaleInCustomSystem();
            scale.ShowDialog();
        }

        private void chart2D_Click(object sender, RoutedEventArgs e)
        {
            Chart2DSystem chart = new Chart2DSystem();
            chart.ShowDialog();
        }

        private void rect_Click(object sender, RoutedEventArgs e)
        {
            RectangleShape r = new RectangleShape();
            r.ShowDialog();
        }

        private void ellipse_Click(object sender, RoutedEventArgs e)
        {
            EllipseShape ell = new EllipseShape();
            ell.ShowDialog();
        }

        private void shapePlacement_Click(object sender, RoutedEventArgs e)
        {
            PlaceShapes shape = new PlaceShapes();
            shape.ShowDialog();
        }

        private void polyline_Click(object sender, RoutedEventArgs e)
        {
            Polylines pl = new Polylines();
            pl.ShowDialog();
        }

        private void polygon_Click(object sender, RoutedEventArgs e)
        {
            Polygons plg = new Polygons();
            plg.ShowDialog();
        }

        private void fillRule_Click(object sender, RoutedEventArgs e)
        {
            PolygonFillRule fillrule = new PolygonFillRule();
            fillrule.ShowDialog();
        }

        private void resizableCanvas_Click(object sender, RoutedEventArgs e)
        {
            ResizableCanvas canvas = new ResizableCanvas();
            canvas.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void line_Click(object sender, RoutedEventArgs e)
        {
            LineDashStyles ld = new LineDashStyles();
            ld.ShowDialog();
        }

        private void geometryGroup_Click(object sender, RoutedEventArgs e)
        {
            GeometryGroupExample gg = new GeometryGroupExample();
            gg.ShowDialog();
        }

        private void lineCurve_Click(object sender, RoutedEventArgs e)
        {
            LineCurveExample lc = new LineCurveExample();
            lc.ShowDialog();
        }

        private void hitTest_Click(object sender, RoutedEventArgs e)
        {
            HitTestExample ht = new HitTestExample();
            ht.ShowDialog();
        }

        private void combined_Click(object sender, RoutedEventArgs e)
        {
            CombinedGeometryExample cg = new CombinedGeometryExample();
            cg.ShowDialog();
        }

        private void bezier_Click(object sender, RoutedEventArgs e)
        {
            AnimateBezierCurve bezier = new AnimateBezierCurve();
            bezier.ShowDialog();
        }

        private void customShape_Click(object sender, RoutedEventArgs e)
        {
            CustomShape cs = new CustomShape();
            cs.ShowDialog();
        }
    }
}
