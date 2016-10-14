using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Chart3DNoWPFEngine
{
    /// <summary>
    /// Interaction logic for Cube.xaml
    /// </summary>
    public partial class Cube : Window
    {
        private double side = 50;
        private Point center;
        private Point3D[] vertices0;
        private Point3D[] vertices;
        private Face[] faces;
        private bool isVisible;

        public Cube()
        {
            InitializeComponent();

            center = new Point(canvas1.Width / 2, canvas1.Height / 2);
            center = new Point(canvas1.Width / 2, canvas1.Height / 2);
            vertices0 = new Point3D[] { new Point3D(-side,-side,-side),
                                        new Point3D( side,-side,-side),
                                        new Point3D( side, side,-side),
                                        new Point3D(-side, side,-side),
                                        new Point3D(-side, side, side),
                                        new Point3D( side, side, side),
                                        new Point3D( side,-side, side),
                                        new Point3D(-side,-side, side)};
            faces = new Face[] {new Face(0,1,2,3), new Face(4,5,6,7),
                                new Face(3,4,7,0), new Face(2,1,6,5),
                                new Face(5,4,3,2), new Face(0,7,6,1)};
            AddCube();
        }

        public void AddCube()
        {
            double elevation = double.Parse(tbElevation.Text);
            double azimuth = double.Parse(tbAzimuth.Text);
            Matrix3D transformMatrix = Utility.AzimuthElevation(elevation, azimuth);
            vertices = new Point3D[8];

            for (int i = 0; i < vertices0.Length; i++)
            {
                vertices[i] = Point3D.Multiply(vertices0[i], transformMatrix);
            }

            canvas1.Children.Clear();
            int ii = 0;
            foreach (Face face in this.faces)
            {
                ii++;
                Point3D va = vertices[face.VertexA];
                Point3D vb = vertices[face.VertexB];
                Point3D vc = vertices[face.VertexC];
                Point3D vd = vertices[face.VertexD];

                Vector3D normal = Utility.NormalVector(va, vb, vc);
                Vector3D viewDirection = new Vector3D();
                viewDirection = new Vector3D(0, 0, -1);
                double mixProduct = Vector3D.DotProduct(normal, viewDirection);
                isVisible = mixProduct > 0;
                if (isVisible)
                {
                    byte red = 0;
                    byte green = 0;
                    byte blue = 0;
                    if (ii == 1)
                    {
                        red = 255;
                        green = 0;
                        blue = 0;
                    }
                    else if (ii == 2)
                    {
                        red = 0;
                        green = 255;
                        blue = 0;
                    }
                    else if (ii == 3)
                    {
                        red = 0;
                        green = 0;
                        blue = 255;
                    }
                    else if (ii == 4)
                    {
                        red = 255;
                        green = 0;
                        blue = 255;
                    }
                    else if (ii == 5)
                    {
                        red = 255;
                        green = 255;
                        blue = 0;
                    }
                    else if (ii == 6)
                    {
                        red = 0;
                        green = 255;
                        blue = 255;
                    }
                    Polygon polygon = new Polygon();
                    PointCollection collection = new PointCollection();
                    collection.Add(new Point(va.X, va.Y));
                    collection.Add(new Point(vb.X, vb.Y));
                    collection.Add(new Point(vc.X, vc.Y));
                    collection.Add(new Point(vd.X, vd.Y));
                    polygon.Points = collection;
                    polygon.Fill = new SolidColorBrush(Color.FromArgb(255, red, green, blue));
                    TranslateTransform tt = new TranslateTransform();
                    tt.X = center.X;
                    tt.Y = center.Y;
                    polygon.RenderTransform = tt;
                    canvas1.Children.Add(polygon);
                }
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            AddCube();
        }
    }

    public class Face
    {
        public int VertexA, VertexB, VertexC, VertexD;
        public Face(int vertexA, int vertexB, int vertexC, int vertexD)
        {
            this.VertexA = vertexA;
            this.VertexB = vertexB;
            this.VertexC = vertexC;
            this.VertexD = vertexD;
        }
    }
}
