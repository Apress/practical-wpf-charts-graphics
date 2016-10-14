using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFOverview
{
    /// <summary>
    /// Interaction logic for CodeOnly.xaml
    /// </summary>
    public partial class CodeOnly : Window
    {
        private TextBlock textBlock;
        private TextBox textBox;

        public CodeOnly()
        {
            InitializeComponent();          
            Initialization();
        }
        
        private void Initialization()
        {
            // Configure the window:
            this.Height = 300;
            this.Width = 300;
            this.Title = "Code Only Example";   

            // Create Grid and StackPanel and add them to window:
            Grid grid = new Grid();
            StackPanel stackPanel = new StackPanel();
            grid.Children.Add(stackPanel);
            this.AddChild(grid);

            // Add a text block to stackPanel:
            textBlock = new TextBlock();
            textBlock.Margin = new Thickness(5);
            textBlock.Height = 30;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Text = "Hello, WPF!";
            stackPanel.Children.Add(textBlock);

            // Add a text box to stackPanel:
            textBox = new TextBox();
            textBox.Margin = new Thickness(5);
            textBox.Width = 200;
            textBox.Text = "Hello, WPF!";
            textBox.TextAlignment = TextAlignment.Center;
            textBox.TextChanged += OnTextChanged;
            stackPanel.Children.Add(textBox);

            // Add button to stackPanel used to change text color:
            Button btnColor = new Button();
            btnColor.Margin = new Thickness(5);
            btnColor.Width = 200;
            btnColor.Content = "Change Text Color";
            btnColor.Click += btnChangeColor_Click;
            stackPanel.Children.Add(btnColor);

            // Add button to stackPanel used to change text font size:
            Button btnSize = new Button();
            btnSize.Margin = new Thickness(5);
            btnSize.Width = 200;
            btnSize.Content = "Change Text Color";
            btnSize.Click += btnChangeSize_Click;
            stackPanel.Children.Add(btnSize);
        }

        private void OnTextChanged(object sender,
            TextChangedEventArgs e)
        {
            textBlock.Text = textBox.Text;
        }

        private void btnChangeColor_Click(object sender,
            RoutedEventArgs e)
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

        private void btnChangeSize_Click(object sender,
            RoutedEventArgs e)
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
