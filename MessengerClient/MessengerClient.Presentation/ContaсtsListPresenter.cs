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
            Dictionary<string, string> contactsMessegeList = new Dictionary<string, string>();
            Dictionary<string, bool> onlineContactsList = new Dictionary<string, bool>();

            foreach (var cont in contacts)
            {
                contactsNameList.Add(cont.Name);
                contactsMessegeList.Add(cont.Name, cont.MessageHistory);
                onlineContactsList.Add(cont.Name, cont.Online);
            }

            view.ContaktsMessageHistory = contactsMessegeList;

            view.UpdateContacts(contactsNameList);
        }
    }
}
