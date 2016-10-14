using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GraphicsBasics
{
    /// <summary>
    /// Interaction logic for CustomShape.xaml
    /// </summary>

    public partial class CustomShape : System.Windows.Window
    {
        public CustomShape()
        {
            InitializeComponent();
            AddUSFlag(10, 10, 280);
            StartAnimation();
        }

        private void AddUSFlag(double x0, double y0, double width)
        {
            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
            SolidColorBrush blueBrush = new SolidColorBrush(Colors.DarkBlue);
            SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
            Rectangle rect = new Rectangle();
            double height = 10 * width / 19;

            //Draw white rectangle background:
            rect.Fill = whiteBrush;
            rect.Width = width;
            rect.Height = height;
            Canvas.SetLeft(rect, x0);
            Canvas.SetTop(rect, y0);
            canvas1.Children.Add(rect);

            // Draw seven red stripes:
            for (int i = 0; i < 7; i++)
            {
                rect = new Rectangle();
                rect.Fill = redBrush;
                rect.Width = width;
                rect.Height = height / 13;
                Canvas.SetLeft(rect, x0);
                Canvas.SetTop(rect, y0 + 2 * i * height / 13);
                canvas1.Children.Add(rect);
            }

            // Draw blue box:
            rect = new Rectangle();
            rect.Fill = blueBrush;
            rect.Width = 2 * width / 5;
            rect.Height = 7 * height / 13;
            Canvas.SetLeft(rect, x0);
            Canvas.SetTop(rect, y0);
            canvas1.Children.Add(rect);

            // Draw fifty stars:
            double offset = rect.Width / 40;
            double dx = (rect.Width - 2 * offset) / 11;
            double dy = (rect.Height - 2 * offset) / 9;
            for (int j = 0; j < 9; j++)
            {
                double y = y0 + offset + j * dy + dy / 2;
                for (int i = 0; i < 11; i++)
                {
                    double x = x0 + offset + i * dx + dx / 2;
                    if ((i + j) % 2 == 0)
                    {
                        Star star = new Star();
                        star.Fill = whiteBrush;
                        star.SizeR = width / 55;
                        star.Center = new Point(x, y);
                        canvas1.Children.Add(star);
                    }
                }
            }
        }

        private void StartAnimation()
        {
            // Animating the star:
            AnimationTimeline at = new DoubleAnimation(
              0.1, 1.2, new Duration(new TimeSpan(0, 0, 5)));
            at.RepeatBehavior = RepeatBehavior.Forever;
            at.AutoReverse = true;
            starScale.BeginAnimation(ScaleTransform.ScaleXProperty, at);
            starScale.BeginAnimation(ScaleTransform.ScaleYProperty, at);
            at = new DoubleAnimation(0, 200, new Duration(new TimeSpan(0, 0, 3)));
            at.RepeatBehavior = RepeatBehavior.Forever;
            at.AutoReverse = true;
            starTranslate.BeginAnimation(TranslateTransform.XProperty, at);

            // Animating arrowline1:
            at = new DoubleAnimation(0, 2.5, new Duration(new TimeSpan(0, 0, 4)));
            at.RepeatBehavior = RepeatBehavior.Forever;
            at.AutoReverse = true;
            line1Scale.BeginAnimation(ScaleTransform.ScaleXProperty, at);
            line1Scale.BeginAnimation(ScaleTransform.ScaleYProperty, at);

            // Animating arrowline2:
            at = new DoubleAnimation(0, 50, new Duration(new TimeSpan(0, 0, 5)));
            at.RepeatBehavior = RepeatBehavior.Forever;
            at.AutoReverse = true;
            arrowLine2.BeginAnimation(ArrowLine.ArrowheadSizeXProperty, at);
            arrowLine2.BeginAnimation(ArrowLine.ArrowheadSizeYProperty, at);
            at = new DoubleAnimation(0, 360, new Duration(new TimeSpan(0, 0, 5)));
            at.RepeatBehavior = RepeatBehavior.Forever;
            line2Rotate.BeginAnimation(RotateTransform.AngleProperty, at);

        }
    }
}