using System;
using System.Collections.Generic;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public class MyProfilePresenter
    {
        private readonly IStorage _storage;
        private readonly IConnection _connection;
        private readonly INameWindowView _nameWindow;
        private readonly IMainWindowView _mainWindow;
        private INewContactWindowView _newContactWindow;
        private MyProfile _profile;


        public MyProfilePresenter(IStorage storage, IConnection connection, INameWindowView nameWindow, IMainWindowView mainWindow)
        {
            _storage = storage;
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
        }

        private void DeleteContact(object sender, EventArgs eventArgs)
        {
            _connection.DeleteContact(_mainWindow.ActiveContact);

            _profile = MyProfileEdditor.DeleteContact(_profile, _mainWindow.ActiveContact);
        }

        private void SendMessage(object sender, EventArgs eventArgs)
        {
            _connection.SendMessage(_mainWindow.ActiveContact, _mainWindow.Message);

            _profile = MyProfileEdditor.SendMessage(_profile, _mainWindow.ActiveContact, _mainWindow.Message);

            ContaktsListPresenter.GetContaktsViewModel(_profile.MyContacts, _mainWindow);
        }

        private void CreateNewContactWindow(object sender, EventArgs eventArgs)
        {
            _newContactWindow = _mainWindow.CreateContactWindow();
            _newContactWindow.AddContact += AddNewContact;
            _newContactWindow.ShowWindow();
        }

        private void AddNewContact(object sender, EventArgs eventArgs)
        {
            //будет реализация через IConnection

            var contact = new Contact { Name = _newContactWindow.GetName() };

            _profile.MyContacts.Add(contact);

            _newContactWindow.Close();

            ContaktsListPresenter.GetContaktsViewModel(_profile.MyContacts, _mainWindow);
        }

        private void SaveProfile(object sender, EventArgs eventArgs)
        {
            _storage.Save(_profile);
        }

        private void LoadProfile(object sender, EventArgs eventArgs)
        {
           /* _profile = _connection.LogIn(_nameWindow.Name) ?? new MyProfile
             {
                 MyName = _nameWindow.Name
             };
             */
            _profile = _storage.Load(_nameWindow.Name) ?? new MyProfile
            {
                MyName = _nameWindow.Name
            };

            if (_profile.MyContacts == null)
                _profile.MyContacts = new List<Contact>();

            ContaktsListPresenter.GetContaktsViewModel(_profile.MyContacts, _mainWindow);
        }
    }
}
