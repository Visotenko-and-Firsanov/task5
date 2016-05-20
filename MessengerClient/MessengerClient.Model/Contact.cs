using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Model
{
    [Serializable]
    public class Contact
    {
        public Stack<string> MessageHistory { get; set; }
        public string Name { get; set; }
        public bool Online { get; set; }
        public bool Friend { get; set; }
    }
}
