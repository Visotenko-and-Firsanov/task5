using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using MessengerClient.Model;
using Moq;
using NUnit.Framework;
using static MessengerClient.Presentation.ContaktsListPresenter;

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

            foreach (var cont in contactList)
            {
                var name = cont.Name;

                contactsNameList.Add(name);
                contactsMessegeList.Add(name, cont.MessageHistory);
            }

            GetContaktsViewModel(contactList, mockView.Object);

            mockView.Verify(foo => foo.UpdateContacts(contactsNameList), Times.Once);
        }

        [Test]
        public void ContactListPresenterTest2()
        {
            var mockView = new Mock<IMainWindowView>();

            GetContaktsViewModel(null, mockView.Object);

            mockView.Verify(foo => foo.UpdateContacts(new List<int>()), Times.Never);
        }

        [Test]
        public void ContactListPresenterTest3()
        {
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

            var mockView = new Mock<IMainWindowView>();

            ContaktsListPresenter.FillContactsMessages(contactList, mockView.Object);
            Dictionary<string, string> contactsMessegeList = new Dictionary<string, string>();

            foreach (var cont in contactList)
            {
                var name = cont.Name;

                contactsMessegeList.Add(name, cont.MessageHistory);
            }


            mockView.VerifySet(view => view.UnreadMessages = new List<string> { student.Name, student2.Name });
            mockView.VerifySet(view => view.ContaktsMessageHistory = contactsMessegeList);
        }


        [Test]
        public void MyProfileEdditorTest1()
        {

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
            Assert.That(0, Is.EqualTo(MyProfileEdditor.FindIndex(contactList, "40")));
        }

        [Test]
        public void MyProfileEdditorTest2()
        {

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

            var profile = new MyProfile
            {
                MyContacts = contactList,
                MyName = "Vova"
            };


            MyProfileEdditor.AddMessage(profile, "40", "123");

            Assert.That("2412Vova : \n 123 \n", Is.EqualTo(profile.MyContacts[0].MessageHistory));

            Assert.That(1, Is.EqualTo(MyProfileEdditor.FindIndex(contactList, "42")));
            Assert.That(0, Is.EqualTo(MyProfileEdditor.FindIndex(contactList, "40")));
        }

        [Test]
        public void MyProfileEdditorTest3()
        {

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

            var profile = new MyProfile
            {
                MyContacts = contactList,
                MyName = "Vova"
            };


            MyProfileEdditor.ChangeStatus(profile, "40", false);

            Assert.False(profile.MyContacts[0].Online);

            MyProfileEdditor.DeleteContact(profile, "40");

            Assert.That(1, Is.EqualTo(profile.MyContacts.Count));
        }

        [Test]
        public void MyProfileEdditorTest4()
        {

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

            var profile = new MyProfile
            {
                MyContacts = contactList,
                MyName = "Vova"
            };


            MyProfileEdditor.ChangeStatus(profile, "esfs", false);

            Assert.True(profile.MyContacts[0].Online);

            MyProfileEdditor.DeleteContact(profile, "fdsgds");

            Assert.That(2, Is.EqualTo(profile.MyContacts.Count));
        }

        [ExpectedException]
        [Test]
        public void MyProfileEdditorTest5()
        {

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

            var profile = new MyProfile
            {
                MyContacts = contactList,
                MyName = "Vova"
            };


            MyProfileEdditor.AddMessage(profile, "fdsgds", "123");
        }

        [Test]
        public void MyProfilePresenterTest1()
        {

            var mock1 = new Mock<IMainWindowView>();
            var mock2 = new Mock<INameWindowView>();
            var mock3 = new Mock<IConnection>();

            var presenter = new MyProfilePresenter(mock3.Object, mock2.Object, mock1.Object);

            presenter.Initialize();

            mock1.Setup(m => m.ContaktsMessageHistory).Returns(new Dictionary<string, string> { { "Bron", "Bron" } });
            mock1.Setup(m => m.ActiveContact).Returns("Bron");

            mock3.Setup(m => m.LogIn(It.IsAny<string>())).Returns(new MyProfile { MyName = "Lol" });

            mock2.Raise(m => m.LoadProfile += null, EventArgs.Empty);

            mock1.Raise(m => m.SendMessage += null, EventArgs.Empty);

            mock3.Verify(m => m.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void MyProfilePresenterTest2()
        {

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

            var mock1 = new Mock<IMainWindowView>();
            var mock2 = new Mock<INameWindowView>();
            var mock3 = new Mock<IConnection>();

            var presenter = new MyProfilePresenter(mock3.Object, mock2.Object, mock1.Object);

            presenter.Initialize();

            mock1.Setup(m => m.ContaktsMessageHistory).Returns(new Dictionary<string, string> { { "Bron", "Bron" } });
            mock1.Setup(m => m.ActiveContact).Returns("Bron");
            mock3.Setup(m => m.Messages).Returns(new KeyValuePair<string, string>("dsafdas", "dasfsa"));
            mock1.Setup(m => m.UnreadMessages).Returns(new List<string>());

            mock3.Setup(m => m.LogIn(It.IsAny<string>())).Returns(new MyProfile { MyName = "Lol", MyContacts = contactList });

            mock2.Raise(m => m.LoadProfile += null, EventArgs.Empty);

            mock3.Raise(m => m.MessangePropertyChanged += null, new PropertyChangedEventArgs("Bar"));

            mock1.Verify(m => m.UpdateMessageScreen(), Times.Once);
        }

        [Test]
        public void MyProfilePresenterTest3()
        {

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

            var mock1 = new Mock<IMainWindowView>();
            var mock2 = new Mock<INameWindowView>();
            var mock3 = new Mock<IConnection>();

            var presenter = new MyProfilePresenter(mock3.Object, mock2.Object, mock1.Object);

            presenter.Initialize();

            mock1.Setup(m => m.ContaktsMessageHistory).Returns(new Dictionary<string, string> { { "Bron", "Bron" } });
            mock1.Setup(m => m.ActiveContact).Returns("Bron");
            mock3.Setup(m => m.Messages).Returns(new KeyValuePair<string, string>("dsafdas", "dasfsa"));
            mock1.Setup(m => m.UnreadMessages).Returns(new List<string>());

            mock3.Setup(m => m.LogIn(It.IsAny<string>())).Returns(new MyProfile { MyName = "Lol", MyContacts = contactList });

            mock2.Raise(m => m.LoadProfile += null, EventArgs.Empty);

            mock1.Raise(m => m.SaveProfile += null, EventArgs.Empty);

            mock3.Verify(m => m.Save(It.IsAny<MyProfile>()), Times.Once);
        }

        [Test]
        public void MyProfilePresenterTest4()
        {

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

            var mock1 = new Mock<IMainWindowView>();
            var mock2 = new Mock<INameWindowView>();
            var mock3 = new Mock<IConnection>();

            var presenter = new MyProfilePresenter(mock3.Object, mock2.Object, mock1.Object);

            presenter.Initialize();

            mock1.Setup(m => m.ContaktsMessageHistory).Returns(new Dictionary<string, string> { { "Bron", "Bron" } });
            mock1.Setup(m => m.ActiveContact).Returns("Bron");
            mock3.Setup(m => m.Messages).Returns(new KeyValuePair<string, string>("dsafdas", "dasfsa"));
            mock1.Setup(m => m.UnreadMessages).Returns(new List<string>());

            mock3.Setup(m => m.LogIn(It.IsAny<string>())).Returns(new MyProfile { MyName = "Lol", MyContacts = contactList });

            mock2.Raise(m => m.LoadProfile += null, EventArgs.Empty);

            mock1.Raise(m => m.DeleteContact += null, EventArgs.Empty);

            mock3.Verify(m => m.Save(It.IsAny<MyProfile>()), Times.Never);
        }

        [Test]
        public void MyProfilePresenterTest5()
        {

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

            var mock1 = new Mock<IMainWindowView>();
            var mock2 = new Mock<INameWindowView>();
            var mock3 = new Mock<IConnection>();
            var mock4 = new Mock<INewContactWindowView>();
            var mock5 = new Mock<IChooseContactWindowView>();

            var presenter = new MyProfilePresenter(mock3.Object, mock2.Object, mock1.Object);

            presenter.Initialize();

            mock1.Setup(m => m.ContaktsMessageHistory).Returns(new Dictionary<string, string> { { "Bron", "Bron" } });
            mock1.Setup(m => m.ActiveContact).Returns("Bron");
            mock3.Setup(m => m.Messages).Returns(new KeyValuePair<string, string>("dsafdas", "dasfsa"));
            mock1.Setup(m => m.UnreadMessages).Returns(new List<string>());
            mock3.Setup(m => m.AddContact(It.IsAny<string>())).Returns(contactList);
            mock4.Setup(m => m.CreateChooseContactWindow()).Returns(mock5.Object);
            mock5.Setup(m => m.ActiveContact).Returns("Bron");
            mock5.Setup(m => m.OnlineContactList).Returns(new List<string>());

            mock1.Setup(m => m.CreateContactWindow()).Returns(mock4.Object);

            mock3.Setup(m => m.LogIn(It.IsAny<string>())).Returns(new MyProfile { MyName = "Lol", MyContacts = contactList });
            mock2.Raise(m => m.LoadProfile += null, EventArgs.Empty);

            mock1.Raise(m => m.AddNewContakt += null, EventArgs.Empty);

            mock4.Verify(m => m.ShowWindow(), Times.Once);

            mock4.Raise(m => m.AddContact += null, EventArgs.Empty);

            mock5.Verify(m => m.UpdateContacts(It.IsAny<List<string>>(), It.IsAny<List<string>>()), Times.Once);

            mock5.Raise(m => m.ChooseContact += null, EventArgs.Empty);

            mock5.Verify(m => m.Close(), Times.Once);
        }
    }
}
