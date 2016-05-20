using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public interface IConection
    {
        string LoadMessage(string name, string message);
    }
}
