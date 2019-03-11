using System.Windows;

namespace Betacraft_Launcher
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }
    }
}
