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
        private ChooseContactWindow _newContactWindow;
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

        public IChooseContactWindowView CreateChooseContactWindow()
        {
            _newContactWindow = new ChooseContactWindow();

            return _newContactWindow;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddContact?.Invoke(this, EventArgs.Empty);
        }
    }
}