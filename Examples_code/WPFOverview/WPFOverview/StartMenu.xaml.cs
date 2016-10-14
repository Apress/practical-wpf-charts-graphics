using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFOverview
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

        private void firstWPF_Click(object sender, RoutedEventArgs e)
        {
            FirstWPFProgram first = new FirstWPFProgram();
            first.ShowDialog();
        }

        private void codeOnly_Click(object sender, RoutedEventArgs e)
        {
            CodeOnly code = new CodeOnly();
            code.ShowDialog();
        }

        private void xamlOnly_Click(object sender, RoutedEventArgs e)
        {
            XAMLOnly xaml = new XAMLOnly();
            xaml.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
