using System.Collections.Generic;
using MessengerClient.Model;
using Moq;
using NUnit.Framework;

namespace MessengerClient.Presentation.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ContactListPresenterTest1()
        {
            var mockView = new Mock<IMainWindowView>();

            var student = new Contact
            {
                MessageHistory = "2412",
                Name = "40",
                Online = true
            };

            var student2 = new Contact
            {
                MessageHistory = "2412",
                Name = "42",
                Online = true
            };

            var contactList = new List<Contact> { student, student2 };

            List<string> contactsNameList = new List<string>();
            Dictionary<string, string> contactsMessegeList = new Dictionary<string, string>();
            Dictionary<string, bool> onlineContactsList = new Dictionary<string, bool>();

            foreach (var cont in contactList)
            {
                var name = cont.Name;

                contactsNameList.Add(name);
                contactsMessegeList.Add(name, cont.MessageHistory);
                onlineContactsList.Add(name, cont.Online);
            }

            ContaktsListPresenter.GetContaktsViewModel(contactList, mockView.Object);

            mockView.VerifySet(view => view.ContaktsMessageHistory = contactsMessegeList);
            mockView.Verify(foo => foo.UpdateContacts(contactsNameList), Times.Once);
        }

        [Test]
        public void ContactListPresenterTest2()
        {
            var mockView = new Mock<IMainWindowView>();

            ContaktsListPresenter.GetContaktsViewModel(null, mockView.Object);

            mockView.Verify(foo => foo.UpdateContacts(new List<int>()), Times.Never);
        }

    

    }
}
