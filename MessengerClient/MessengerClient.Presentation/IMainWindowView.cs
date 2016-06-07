﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MessengerClient.Presentation
{
    public interface IMainWindowView
    {
        Dictionary<string, string> ContaktsMessageHistory { get; set; }
        List<string> OnlineContactsList { get; set; }
        string Message { get; set; }
        string ActiveContact { get; set; }

        void UpdateMessageScreen();
        void UpdateContacts(IEnumerable contaktsList);

        INewContactWindowView CreateContactWindow();

        event EventHandler DeleteContact;
        event EventHandler SendMessage;
        event EventHandler AddNewContakt;
        event EventHandler SaveProfile;
    }
}