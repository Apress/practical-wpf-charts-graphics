using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Reflection;
using System.Windows.Shapes;

namespace Chart3DWithWPFEngine
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

        private void StartMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.Source;
            if (btn.Content.ToString() != "Close")
            {
                Type type = this.GetType();
                Assembly assembly = type.Assembly;
                Window window = (Window)assembly.CreateInstance(
                    type.Namespace + "." + btn.Content);
                window.ShowDialog();
            }
            else
                this.Close();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            SimpleSurfaceTest s = new SimpleSurfaceTest();
            s.ShowDialog();
        }
    }
}
