using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Dal.MessengerService;
using MessengerClient.Model;
using MessengerClient.Presentation;
using WcfContrib.Hosting;
using Contact = MessengerClient.Model.Contact;


namespace MessengerClient.Dal
{
    public class ConnectionServer : IConnection
    {
        private readonly string _address;
        private MessengerService.MessengerServerServiceClient _client;

        public ConnectionServer()
        {
            //InstanceContext context = new InstanceContext(this);

            //_client = new MessengerServerServiceClient(context, "NetTcpBinding_IMessengerServerService");
        }

        public void SendMessage(string name, string message)
        {
            // _client.SendMessage();
        }

        public Contact AddContact(string name)
        {
            //_client.FindUser(name);

            return new Contact();
        }

        
        public MyProfile LogIn(string name)
        {
            Profile profile = _client.UploadUserData(name);

            return TransformProfileView(profile);
        }

        private MyProfile TransformProfileView(Profile serverProfile)
        {
            var serverContactList = serverProfile.Contacts;

            var myContactList = serverContactList.Select(cont => new Contact
            {
                Name = cont.Name,
                MessageHistory = cont.Message,
                Online = cont.Online
            }).ToList();

            MyProfile profile = new MyProfile
            {
                MyName = serverProfile.Name,
                MyContacts = myContactList
            };

            return profile;
        }

        public void DeleteContact(string name)
        {


        }

        public List<KeyValuePair<string, List<string>>> Messages { get; set; }

        public List<Contact> ContactsList { get; set; }

        public string LoadMessage(string name, string message)
        {
            return name;
        }
    }
}