using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public class MyProfilePresenter
    {
        private readonly IConnection _connection;
        private readonly IMainWindowView _mainWindow;
        private readonly INameWindowView _nameWindow;
        private IChooseContactWindowView _chooseContactWindow;
        private INewContactWindowView _newContactWindow;
        private MyProfile _profile;


        public MyProfilePresenter(IConnection connection, INameWindowView nameWindow,
            IMainWindowView mainWindow)
        {
            _connection = connection;
            _nameWindow = nameWindow;
            _mainWindow = mainWindow;
        }

        public void Initialize()
        {
            _nameWindow.LoadProfile += LoadProfile;
            _mainWindow.AddNewContakt += CreateNewContactWindow;
            _mainWindow.DeleteContact += DeleteContact;
            _mainWindow.SendMessage += SendMessage;
            _mainWindow.SaveProfile += SaveProfile;
            _connection.MessangePropertyChanged += LoadMessage;
            _connection.StatusPropertyChanged += UpdateStatus;
        }

        private void UpdateStatus(object sender, PropertyChangedEventArgs e)
        {
            _profile = MyProfileEdditor.ChangeStatus(_profile, _connection.StatusUpdate.Key,
                _connection.StatusUpdate.Value);


            ContaktsListPresenter.GetContaktsViewModel(_profile.MyContacts, _mainWindow);


            _mainWindow.ChangeStatusContact(_connection.StatusUpdate.Key);
        }

        private void DeleteContact(object sender, EventArgs eventArgs)
        {
            _profile = MyProfileEdditor.DeleteContact(_profile, _mainWindow.ActiveContact);

            _mainWindow.ContaktsMessageHistory.Remove(_mainWindow.ActiveContact);
        }

        private void SendMessage(object sender, EventArgs eventArgs)
        {
            _connection.SendMessage(_profile.MyName, _mainWindow.ActiveContact, _mainWindow.Message);

            _mainWindow.ContaktsMessageHistory[_mainWindow.ActiveContact] += ReformatMessage.Reformat(_profile.MyName,
                _mainWindow.Message);
        }

        private void LoadMessage(object sender, EventArgs eventArgs)
        {
            var contactName = _connection.Messages.Key;
            var contactMessage = _connection.Messages.Value;

            if (MyProfileEdditor.FindIndex(_profile.MyContacts, contactName) == -1)
            {
                AddNewContact(new Contact { Name = contactName, Online = true });
            }
            UpdateStatus(sender, null);

            if (contactMessage != null && !_mainWindow.UnreadMessages.Contains(contactName))
                _mainWindow.UnreadMessages.Add(contactName);

            var keysList = _mainWindow.ContaktsMessageHistory.Keys;

            if (keysList.Contains(_connection.Messages.Key))
                _mainWindow.ContaktsMessageHistory[contactName] += contactMessage;
            else
            {
                _mainWindow.ContaktsMessageHistory.Add(contactName, contactMessage);
            }

            _mainWindow.UpdateMessageScreen();
        }

        private void CreateNewContactWindow(object sender, EventArgs eventArgs)
        {
            _newContactWindow = _mainWindow.CreateContactWindow();
            _newContactWindow.AddContact += CreateChooseContactWindow;
            _newContactWindow.ShowWindow();
        }

        private void CreateChooseContactWindow(object sender, EventArgs eventArgs)
        {
            var nameContact = _newContactWindow.GetName();

            var contacts = _connection.AddContact(nameContact);

            if (contacts.Count == 0)
            {
                _newContactWindow.ShowMessage("Таких контакта не найдено");
                return;
            }

            _chooseContactWindow = _newContactWindow.CreateChooseContactWindow();

            var namesContactList = new List<string>();
            var onlineContactList = new List<string>();

            foreach (var cont in contacts)
            {
                namesContactList.Add(cont.Name);

                if (cont.Online)
                    onlineContactList.Add(cont.Name);
            }

            _chooseContactWindow.UpdateContacts(namesContactList, onlineContactList);

            _chooseContactWindow.ChooseContact += AddNewContactFromWindow;
            _chooseContactWindow.ShowWindow();

            _newContactWindow.Close();
        }

        private void AddNewContactFromWindow(object sender, EventArgs eventArgs)
        {
            var contact = new Contact
            {
                Name = _chooseContactWindow.ActiveContact,
                Online =
                    _chooseContactWindow.OnlineContactList.Contains(_chooseContactWindow.ActiveContact)
            };

            AddNewContact(contact);

            _chooseContactWindow.Close();
        }

        private void AddNewContact(Contact contact)
        {
            if (_profile.MyName == contact.Name || _profile.MyContacts.Contains(contact) || _mainWindow.ContaktsMessageHistory.ContainsKey(contact.Name))
                return;

            _profile.MyContacts.Add(contact);

            _mainWindow.ContaktsMessageHistory.Add(contact.Name, contact.MessageHistory);
            ContaktsListPresenter.GetContaktsViewModel(_profile.MyContacts, _mainWindow);
        }

        private void SaveProfile(object sender, EventArgs eventArgs)
        {
            _connection.Save(_profile);
        }

        private void LoadProfile(object sender, EventArgs eventArgs)
        {
            _profile = _connection.LogIn(_nameWindow.Name) ?? new MyProfile
            {
                MyName = _nameWindow.Name
            };


            if (_profile.MyContacts == null)
                _profile.MyContacts = new List<Contact>();

            ContaktsListPresenter.FillContactsMessages(_profile.MyContacts, _mainWindow);

            ContaktsListPresenter.GetContaktsViewModel(_profile.MyContacts, _mainWindow);
        }
    }
}