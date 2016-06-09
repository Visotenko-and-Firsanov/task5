using System.Collections.Generic;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public static class ContaktsListPresenter
    {
        public static void GetContaktsViewModel(List<Contact> contacts, IMainWindowView view)
        {
            if (contacts == null)
                return;

            List<string> contactsNameList = new List<string>();
            List<string> onlineContactsList = new List<string>();

            foreach (var cont in contacts)
            {
                contactsNameList.Add(cont.Name);
                if (cont.Online && !onlineContactsList.Contains(cont.Name))
                    onlineContactsList.Add(cont.Name);
            }

            view.OnlineContactsList = onlineContactsList;

            view.UpdateContacts(contactsNameList);
        }

        public static void FillContactsMessages(List<Contact> contacts, IMainWindowView view)
        {
            if (contacts == null)
                return;

            Dictionary<string, string> contactsMessegeList = new Dictionary<string, string>();

            var unreadMessages = new List<string>();

            foreach (var cont in contacts)
            {
                if (cont.MessageHistory != "")
                    unreadMessages.Add(cont.Name);

                if (contactsMessegeList.ContainsKey(cont.Name))
                    contactsMessegeList[cont.Name] += cont.MessageHistory;
                else
                {
                    contactsMessegeList.Add(cont.Name, cont.MessageHistory);
                }

            }

            view.UnreadMessages = unreadMessages;
            view.ContaktsMessageHistory = contactsMessegeList;
        }
    }
}
