using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Betacraft_Launcher
{
    public partial class MainWindow : Window
    {
        static string appData = Environment.GetEnvironmentVariable("APPDATA");
        string path = appData + @"\betacraft\versions.txt";
        string path2 = appData + @"\betacraft\nick\nick.txt";
        string path3 = appData + @"\betacraft\versionsadmin.txt";
        string createText = "Nick" + Environment.NewLine;
        string nick;
        string readnick;
        string readText;
        string labele;

        WebClient client = new WebClient();
        About ab = new About();
        Password pw = new Password();

        public MainWindow()
        {
            InitializeComponent();

            labele = client.DownloadString("https://betacraft.ovh/client/version.txt");

            if (!Directory.Exists(appData + @"\betacraft\"))
            {
                Directory.CreateDirectory(appData + @"\betacraft\");
            }

            if (!Directory.Exists(appData + @"\betacraft\nick"))
            {
                Directory.CreateDirectory(appData + @"\betacraft\nick");
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                File.WriteAllText(appData + @"\betacraft\versions.txt", createText);
            }

            if (!File.Exists(path2))
            {
                File.Create(path2).Close();
                File.WriteAllText(appData + @"\betacraft\nick\nick.txt", createText);
            }
            
            if (!File.Exists(path3))
            {
                File.Create(path3).Close();
                File.WriteAllText(appData + @"\betacraft\versionsadmin.txt", createText);
            }


            readnick = File.ReadLines(appData + @"\betacraft\nick\nick.txt").First();
            readText = File.ReadLines(appData + @"\betacraft\versions.txt").First();
            textbox1.Text = readnick;

            if (readText != labele)
            {
                Launcher.Download(client.DownloadString("https://betacraft.ovh/client/version.txt"), "https://betacraft.ovh/client/betacraft.rar", appData + @"\betacraft\betacraft.rar", appData + @"\betacraft\versions.txt");
            }
            else
            {

            }

    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textbox1.Text.Length < 3)
            {
                MessageBox.Show("Nick musi zawierać więcej niż 3 znaki. Wydłuż swój nick!");
            }
            else if (textbox1.Text.Contains("ć") || textbox1.Text.Contains("ą") || textbox1.Text.Contains("ę") || textbox1.Text.Contains("ł") || textbox1.Text.Contains("ń") || textbox1.Text.Contains("ó") || textbox1.Text.Contains("ś") || textbox1.Text.Contains("ź") || textbox1.Text.Contains("ż") || textbox1.Text.Contains("~") || textbox1.Text.Contains("!") || textbox1.Text.Contains("@") || textbox1.Text.Contains("#") || textbox1.Text.Contains("$") || textbox1.Text.Contains("%") || textbox1.Text.Contains("^") || textbox1.Text.Contains("&") || textbox1.Text.Contains("*") || textbox1.Text.Contains("(") || textbox1.Text.Contains(")") || textbox1.Text.Contains("+") || textbox1.Text.Contains("=") || textbox1.Text.Contains("[") || textbox1.Text.Contains("]") || textbox1.Text.Contains("{") || textbox1.Text.Contains("}") || textbox1.Text.Contains(":") || textbox1.Text.Contains(";") || textbox1.Text.Contains("'") || textbox1.Text.Contains("|") || textbox1.Text.Contains("<") || textbox1.Text.Contains(",") || textbox1.Text.Contains(">") || textbox1.Text.Contains(".") || textbox1.Text.Contains("Ć") || textbox1.Text.Contains("Ą") || textbox1.Text.Contains("Ę") || textbox1.Text.Contains("Ł") || textbox1.Text.Contains("Ń") || textbox1.Text.Contains("Ó") || textbox1.Text.Contains("Ś") || textbox1.Text.Contains("Ź") || textbox1.Text.Contains("Ż") || textbox1.Text.Contains(" "))
            {
                MessageBox.Show("Nick nie może zawierać polskich znaków, spacji oraz znaków typu &, # i tym podobnych.");
            }
            else
            {
                Launcher.LaunchGame(textbox1.Text);
            }
        }

        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {

            nick = textbox1.Text + Environment.NewLine;

            if (textbox1.Text.Length > 16)
            {
                textbox1.Clear();
                MessageBox.Show("Maksymalna długość nicku to 16 znaków!");
            }

            File.WriteAllText(appData + @"\betacraft\nick\nick.txt", nick);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textbox1.Text.Length < 3)
            {
                MessageBox.Show("Nick musi zawierać więcej niż 3 znaki. Wydłuż swój nick!");
            }
            else if (textbox1.Text.Contains("ć") || textbox1.Text.Contains("ą") || textbox1.Text.Contains("ę") || textbox1.Text.Contains("ł") || textbox1.Text.Contains("ń") || textbox1.Text.Contains("ó") || textbox1.Text.Contains("ś") || textbox1.Text.Contains("ź") || textbox1.Text.Contains("ż") || textbox1.Text.Contains("~") || textbox1.Text.Contains("!") || textbox1.Text.Contains("@") || textbox1.Text.Contains("#") || textbox1.Text.Contains("$") || textbox1.Text.Contains("%") || textbox1.Text.Contains("^") || textbox1.Text.Contains("&") || textbox1.Text.Contains("*") || textbox1.Text.Contains("(") || textbox1.Text.Contains(")") || textbox1.Text.Contains("+") || textbox1.Text.Contains("=") || textbox1.Text.Contains("[") || textbox1.Text.Contains("]") || textbox1.Text.Contains("{") || textbox1.Text.Contains("}") || textbox1.Text.Contains(":") || textbox1.Text.Contains(";") || textbox1.Text.Contains("'") || textbox1.Text.Contains("|") || textbox1.Text.Contains("<") || textbox1.Text.Contains(",") || textbox1.Text.Contains(">") || textbox1.Text.Contains(".") || textbox1.Text.Contains("Ć") || textbox1.Text.Contains("Ą") || textbox1.Text.Contains("Ę") || textbox1.Text.Contains("Ł") || textbox1.Text.Contains("Ń") || textbox1.Text.Contains("Ó") || textbox1.Text.Contains("Ś") || textbox1.Text.Contains("Ź") || textbox1.Text.Contains("Ż") || textbox1.Text.Contains(" "))
            {
                MessageBox.Show("Nick nie może zawierać polskich znaków, spacji oraz znaków typu &, # i tym podobnych.");
            }
            else
            {
                pw.Visibility = Visibility.Visible;
            }
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(System.Windows.Input.Key.Enter))
            {
                Launcher.LaunchGame(textbox1.Text);
            }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            ab.Visibility = Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
