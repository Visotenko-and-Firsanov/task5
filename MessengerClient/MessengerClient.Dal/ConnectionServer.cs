using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using MessengerClient.Dal.MessengerServerReference;
using MessengerClient.Model;
using MessengerClient.Presentation;

namespace MessengerClient.Dal
{
    public class ConnectionServer : IConnection, IMessengerServerServiceCallback
    {
        private IMessengerServerService _client;
        private KeyValuePair<string, string> _messages;
        private KeyValuePair<string, bool> _statusUpdate;

        public ConnectionServer()
        {
            var context = new InstanceContext(this);

            _client = new MessengerServerServiceClient(context, "NetTcpBinding_IMessengerServerService");
        }

        public IMessengerServerService Client
        {
            set { _client = value; }
        }

        public void Save(MyProfile profile)
        {
            if (profile == null)
                return;

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

        public List<Contact> AddContact(string name)
        {
            var tempCont = _client.FindUser(name);

            return tempCont?.Select(cont => new Contact
            {
                Name = cont.Name,
                Online = cont.Online
            }).ToList();
        }

        public MyProfile LogIn(string name)
        {
            var profile = _client.UploadUserData(name);

            return TransformProfileView(profile);
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

        public void LoadMessage(string name, string message)
        {
            Messages = new KeyValuePair<string, string>(name, ReformatMessage.Reformat(name, message));
        }

        public void ContactsStatusUpdate(string usernameReceiver, bool online)
        {
            StatusUpdate = new KeyValuePair<string, bool>(usernameReceiver, online);
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

            var profile = new MyProfile
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
                if (message.Value == contactName)
                {
                    resultMessage.Append(ReformatMessage.Reformat(message.Value, message.Key));

                    break;
                }
            }

            return resultMessage.ToString();
        }

        protected virtual void MessangeOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            MessangePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void StatusOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            StatusPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}