using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MessengerClient.Presentation;

namespace MessengerClient
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainWindowView
    {
        private NewContactWindow _newContactWindow;

        public List<KeyValuePair<string, string>> ConactsList;

        public MainWindow()
        {
            InitializeComponent();
        }

        public string NewContactName { get; set; }

        public List<string> OnlineContactsList { get; set; }
        public string Message { get; set; }
        public string ActiveContact { get; set; }

        public Dictionary<string, string> ContaktsMessageHistory { get; set; }


        public void UpdateMessageScreen()
        {
            messegeHistory.Text = ContaktsMessageHistory[(string)listView.SelectedItem];
        }

        public void UpdateContacts(IEnumerable contaktsList)
        {
            var newContactsList = new List<string>();

            newContactsList.AddRange(contaktsList.Cast<string>());

            foreach (var contact in newContactsList)
            {
                if (listView.Items.Contains(contact))
                    continue;

                listView.Items.Add(contact);
            }
        }

        public INewContactWindowView CreateContactWindow()
        {
            _newContactWindow = new NewContactWindow();

            return _newContactWindow;
        }

        public event EventHandler DeleteContact;
        public event EventHandler SendMessage;
        public event EventHandler AddNewContakt;
        public event EventHandler SaveProfile;

        private void Send(object sender, RoutedEventArgs e)
        {
            Message = messageField.Text;

            messageField.Text = "";

            if (SendMessage != null) SendMessage.Invoke(this, EventArgs.Empty);

            messegeHistory.Text = ContaktsMessageHistory[(string)listView.SelectedItem];
        }

        private void Messenger_Unload(object sender, EventArgs eventArgs)
        {
            if (SaveProfile != null) SaveProfile.Invoke(this, EventArgs.Empty);
        }

        //Вынести из окна
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedIndex == -1)
            {
                messegeHistory.Text = "";
                sendMessageButton.IsEnabled = false;
                return;
            }

            sendMessageButton.IsEnabled = true;

            ActiveContact = (string)listView.SelectedItem;

            removeButton.IsEnabled = true;

            if (ContaktsMessageHistory.Count >= 1)
                messegeHistory.Text = ContaktsMessageHistory[(string)listView.SelectedItem];
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.RemoveAt(listView.SelectedIndex);

            if (DeleteContact != null) DeleteContact.Invoke(this, EventArgs.Empty);
        }

        private void addContactButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveContact = null;

            removeButton.IsEnabled = false;

            listView.SelectedIndex = listView.Items.Count;

            if (AddNewContakt != null) AddNewContakt.Invoke(this, EventArgs.Empty);
        }
    }
}