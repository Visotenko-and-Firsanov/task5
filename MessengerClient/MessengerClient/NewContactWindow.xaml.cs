using System;
using System.Windows;
using MessengerClient.Presentation;

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window, INewContactWindowView
    {

        public NewContactWindow()
        {
            InitializeComponent();
        }

        public event EventHandler AddContact;

        public void ShowWindow()
        {
            ShowDialog();
        }

        public string GetName()
        {
            return textBox.Text;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        void INewContactWindowView.Close()
        {
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (AddContact != null) AddContact.Invoke(this, EventArgs.Empty);
        }
    }
}