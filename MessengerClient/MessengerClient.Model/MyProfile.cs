using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Model
{
    [Serializable]
    public class MyProfile
    {
        public List<Contact> MyContacts { get; set; }
        public string MyName { get; set; }
    }
}