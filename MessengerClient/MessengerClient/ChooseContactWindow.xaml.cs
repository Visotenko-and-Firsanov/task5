using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MessengerClient.Presentation;

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for ChooseContactWindow.xaml
    /// </summary>
    public partial class ChooseContactWindow : Window, IChooseContactWindowView
    {


        public List<string> OnlineContactList { get; set; }

        public string ActiveContact { get; set; }

        public ChooseContactWindow()
        {
            InitializeComponent();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveContact = _contactList.SelectedItem.ToString();

            searchButton.IsEnabled = true;
        }

        public void ShowWindow()
        {
            Show();
        }

        public void UpdateContacts(List<string> contaktsList, List<string> onlineContactList)
        {
            OnlineContactList = onlineContactList;

            foreach (var contact in contaktsList.Where(contact => !_contactList.Items.Contains(contact)))
            {
                _contactList.Items.Add(contact);
            }
        }

        public event EventHandler ChooseContact;

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseContact?.Invoke(this, EventArgs.Empty);
        }
    }
}