using System;
using System.Collections.Generic;

namespace MessengerClient.Presentation
{
    public interface IChooseContactWindowView
    {
        List<string> OnlineContactList { get; set; }

        string ActiveContact { get; set; }

        void ShowWindow();
        void Close();

        void UpdateContacts(List<string> contaktsList, List<string> onlineContactList);
        event EventHandler ChooseContact;
    }
}