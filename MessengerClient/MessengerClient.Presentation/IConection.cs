using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public interface IConnection
    {
        void LoadMessage(string name, string message);

        void SendMessage(string myName, string receverName, string message);

        void Save(MyProfile profile);
        Contact AddContact(string name);

        MyProfile LogIn(string name);

        KeyValuePair<string, string> Messages { get; set; }
        KeyValuePair<string, bool> StatusUpdate { get; set; }

        event PropertyChangedEventHandler MessangePropertyChanged;
        event PropertyChangedEventHandler StatusPropertyChanged;
    }
}