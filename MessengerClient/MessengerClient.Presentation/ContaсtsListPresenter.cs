using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public static class ContaсtsListPresenter
    {
        public static IEnumerable GetContaсtsViewModel(List<Contact> contacts)
        {
            List<string> contactsNameList = contacts.Select(contact => contact.Name).ToList();

            return (IEnumerable)contactsNameList;
        }

    }
}
