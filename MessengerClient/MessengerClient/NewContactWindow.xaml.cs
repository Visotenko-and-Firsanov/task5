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

        void INewContactWindowView.Close()
        {
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddContact?.Invoke(this, EventArgs.Empty);
        }
    }
}