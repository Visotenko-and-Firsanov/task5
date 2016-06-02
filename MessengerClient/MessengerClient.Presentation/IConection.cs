using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public interface IConnection
    {
        string LoadMessage(string name, string message);

        void SendMessage(string name, string message);

        Contact AddContact(string name);

        MyProfile LogIn(string name);

        void DeleteContact(string name);

        List<KeyValuePair<string, List<string>>> Messages { get; set; }
    }
}