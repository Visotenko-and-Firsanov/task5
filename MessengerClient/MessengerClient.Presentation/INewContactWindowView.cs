using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Presentation
{
    public interface INewContactWindowView
    {
        event EventHandler AddContact;
        string GetName();
        void ShowMessage(string message);
        void ShowWindow();
        void Close();
    }
}