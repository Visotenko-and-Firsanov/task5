using System;
using System.Windows;
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

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public event EventHandler LoadProfile;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Name = textBox.Text;

            Name = Name.Trim();

            MainWindow mainWindow = new MainWindow(Name);

            var awayStorage = new ConnectionServer();

            MyProfilePresenter myProfile = new MyProfilePresenter(awayStorage, this, mainWindow);

            myProfile.Initialize();

            try
            {
                LoadProfile?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                MessageBox.Show("Нет соединения с сервером, попробуйте позже");
                mainWindow.Close();
                Close();
                return;
            }

            Hide();

            mainWindow.Show();

            Close();
        }
    }
}