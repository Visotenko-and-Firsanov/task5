using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Model
{
    public class Contact
    {
        public string MessageHistory { get; set; }
        public string Name { get; set; }
        public bool Online { get; set; }
    }
}