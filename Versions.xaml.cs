using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Betacraft_Launcher
{
    /// <summary>
    /// Interaction logic for Versions.xaml
    /// </summary>
    public partial class Versions : Window
    {
        public Versions()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        private void Versions_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
