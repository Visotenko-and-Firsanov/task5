using System;
using System.Collections.Generic;
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
using MessengerClient.Dal;
using MessengerClient.Presentation;

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for NameWindows.xaml
    /// </summary>
    public partial class NameWindows : Window, INameWindowView
    {
        public new string Name { get; set; }

        public NameWindows()
        {
            InitializeComponent();
        }

        public event EventHandler LoadProfile;

        private void button_Click(object sender, RoutedEventArgs e)
        {

            Name = textBox.Text;


            LocalStorage storage = new LocalStorage();

            ConnectionServer awayStorage = new ConnectionServer();

            //NameWindows nameWindows = new NameWindows();

            MainWindow mainWindow = new MainWindow();

            MyProfilePresenter myProfile = new MyProfilePresenter(storage, awayStorage, this, mainWindow);

            myProfile.Initialize();

            //nameWindows.Show();

            Hide();

            //MainWindow windows = new MainWindow();

            LoadProfile?.Invoke(this, EventArgs.Empty);

            mainWindow.Show();

            Close();
        }
    }
}