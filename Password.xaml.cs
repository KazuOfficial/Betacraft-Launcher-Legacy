using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Betacraft_Launcher
{
    public partial class Password : Window
    {
        string labeleadmin;
        string readadmin;
        string appData = Environment.GetEnvironmentVariable("APPDATA");

        WebClient client = new WebClient();

        public Password()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            readadmin = File.ReadLines(appData + @"\betacraft\versionsadmin.txt").First();
            //labeleadmin = client.DownloadString("https://betacraft.ovh/client/versionadmin.txt");

            if (haslo.Text == "tytuz")
                {
                    if (readadmin != labeleadmin)
                    {
                        //Launcher.Download(client.DownloadString("https://betacraft.ovh/client/versionadmin.txt"), "https://betacraft.ovh/client/betacraftadmin.rar", appData + @"\betacraft\betacraftadmin.rar", appData + @"\betacraft\versionsadmin.txt");
                        //Launcher.Fix();
                        MessageBox.Show("W wersji demonstracyjnej aktualizacje nie funkcjonują z powodu zaprzestania wspierania programu!");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe hasło!");
                }
            }

        private void haslo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (haslo.Text.Length >= 15)
            {
                haslo.Clear();
                MessageBox.Show("Za długie hasło!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }
    }
    }
