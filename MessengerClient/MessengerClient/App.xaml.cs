using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MessengerClient.Dal;

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App() //ересь надо переделать, не возможно ввести имя из-за цикла
        {
            // Create a window
            NameWindows window = new NameWindows();

            MainWindow mainWindow = new MainWindow();

            mainWindow.Hide();

            // Open a window
            window.Show();

            string name;

            while (true)
            {
                if (window.BottonWasClicked)
                {
                    name = window.textBox.Text;
                    break;
                }
                
            }          

            var localStorage = new LocalStorage(name);

            window.Hide();

            mainWindow.Show();


        }
    }
}
