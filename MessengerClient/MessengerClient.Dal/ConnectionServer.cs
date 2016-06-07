using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using MessengerClient.Dal.MessengerServerReference;
using MessengerClient.Model;
using MessengerClient.Presentation;
using Contact = MessengerClient.Model.Contact;


namespace MessengerClient.Dal
{
    public class ConnectionServer : IConnection, IMessengerServerServiceCallback
    {
        private readonly MessengerServerServiceClient _client;
        private KeyValuePair<string, string> _messages;
        private KeyValuePair<string, bool> _statusUpdate;

        public ConnectionServer()
        {
            InstanceContext context = new InstanceContext(this);

            _client = new MessengerServerServiceClient(context, "NetTcpBinding_IMessengerServerService");
        }

        public void Save(MyProfile profile)
        {
            var friendList = profile.MyContacts.Select(cont => new Friend { Name = cont.Name }).ToArray();

            var myUser = new User
            {
                Name = profile.MyName,
                Contacts = friendList
            };

            _client.UploadingUserData(myUser);
        }

        public void SendMessage(string myName, string receiverName, string message)
        {
            _client.SendMessage(myName, receiverName, message);
        }

        public Contact AddContact(string name)
        {
            var tempCont = _client.FindUser(name);

            if (tempCont == null)
                return null;

            return new Contact
            {
                Name = tempCont.Name,
                Online = tempCont.Online
            };
        }

        public MyProfile LogIn(string name)
        {
            User profile = _client.UploadUserData(name);

            return TransformProfileView(profile);
        }

        private MyProfile TransformProfileView(User serverProfile)
        {
            var serverContactList = serverProfile.Contacts;

            var myContactList = serverContactList.Select(cont => new Contact
            {
                Name = cont.Name,
                MessageHistory = TakeMessageHistory(serverProfile, cont.Name),
                Online = cont.Online
            }).ToList();

            MyProfile profile = new MyProfile
            {
                MyName = serverProfile.Name,
                MyContacts = myContactList
            };

            return profile;
        }

        private string TakeMessageHistory(User serverProfile, string contactName)
        {
            var resultMessage = new StringBuilder();

            foreach (var message in serverProfile.MessageBySender)
            {
                if (message.Key == contactName)
                {
                    resultMessage.Append(ReformatMessage.Reformat(message.Key, message.Value));
                }
            }

            return resultMessage.ToString();
        }

        public KeyValuePair<string, string> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                MessangeOnPropertyChanged();
            }
        }

        public KeyValuePair<string, bool> StatusUpdate
        {
            get { return _statusUpdate; }
            set
            {
                _statusUpdate = value;
                StatusOnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler MessangePropertyChanged;
        public event PropertyChangedEventHandler StatusPropertyChanged;

        protected virtual void MessangeOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (MessangePropertyChanged != null)
                MessangePropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void StatusOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (StatusPropertyChanged != null)
                StatusPropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Contact> ContactsList { get; set; }

        public void LoadMessage(string name, string message)
        {
            Messages = new KeyValuePair<string, string>(name, ReformatMessage.Reformat(name, message));
        }

        public void ContactsStatusUpdate(string usernameReceiver, bool online)
        {
            StatusUpdate = new KeyValuePair<string, bool>(usernameReceiver, online);
        }
    }
}