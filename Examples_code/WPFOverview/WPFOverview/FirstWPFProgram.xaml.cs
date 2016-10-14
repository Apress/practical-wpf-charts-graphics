using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFOverview
{
    /// <summary>
    /// Interaction logic for FirstWPFProgram.xaml
    /// </summary>
    public partial class FirstWPFProgram : Window
    {
        public FirstWPFProgram()
        {
            InitializeComponent();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            textBlock.Text = textBox.Text;
        }

        private void btnChangeColor_Click(object sender, RoutedEventArgs e)
        {
            if (textBlock.Foreground == Brushes.Red)
            {
                textBlock.Foreground = Brushes.Black;
                textBox.Foreground = Brushes.Black;
            }
            else
            {
                textBlock.Foreground = Brushes.Red;
                textBox.Foreground = Brushes.Red;
            }
        }

        private void btnChangeSize_Click(object sender, RoutedEventArgs e)
        {
            if (textBlock.FontSize == 12)
            {
                textBlock.FontSize = 24;
                textBox.FontSize = 24;
            }
            else
            {
                textBlock.FontSize = 12;
                textBox.FontSize = 12;
            }
        }
    }
}
