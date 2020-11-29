using System.ComponentModel;
using System.Windows;

namespace Betacraft_Launcher
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        private void About_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
