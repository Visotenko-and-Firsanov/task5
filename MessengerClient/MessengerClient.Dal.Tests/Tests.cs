using System.Collections.Generic;
using MessengerClient.Dal.MessengerServerReference;
using MessengerClient.Model;
using Moq;
using NUnit.Framework;

namespace MessengerClient.Dal.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
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

            ConnectionServer server = new ConnectionServer();

            var mock = new Mock<IMessengerServerService>();

            server.Client = mock.Object;

            server.Save(profile);

            mock.Verify(foo => foo.UploadingUserData(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Test2()
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
                MyName = "i"
            };

            ConnectionServer server = new ConnectionServer();

            var mock = new Mock<IMessengerServerService>();

            server.Client = mock.Object;

            server.SendMessage("i", "you", "we");

            mock.Verify(foo => foo.SendMessage("i", "you", "we"), Times.Once);
        }

        [Test]
        public void Test3()
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
                MyName = "i"
            };

            ConnectionServer server = new ConnectionServer();

            var mock = new Mock<IMessengerServerService>();

            server.Client = mock.Object;

            server.AddContact("vally");

            mock.Verify(foo => foo.FindUser("vally"), Times.Once);
        }

        [Test]
        public void Test4()
        {
            var student = new Contact
            {
                MessageHistory = "42",
                Name = "Man",
                Online = true
            };

            var student2 = new Contact
            {
                MessageHistory = "42",
                Name = "Man2",
                Online = true
            };

            var contactList = new List<Contact> { student, student2 };

            var profile = new MyProfile
            {
                MyContacts = contactList,
                MyName = "i"
            };


            var fr1 = new Friend
            {
                Name = "Man",
                Online = true
            };

            var fr2 = new Friend
            {
                Name = "Man2",
                Online = true
            };

            Friend[] friends = new Friend[] { fr2, fr1 };

            KeyValuePair<string, string>[] mess = new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("42", "Man") };

            var user = new User
            {
                Name = "i",
                Contacts = friends,
                MessageBySender = mess
            };

            ConnectionServer server = new ConnectionServer();

            var mock = new Mock<IMessengerServerService>();

            mock.Setup(foo => foo.UploadUserData("i")).Returns(user);

            server.Client = mock.Object;

            var prof = server.LogIn("i");

            mock.Verify(foo => foo.UploadUserData("i"), Times.Once);

            Assert.That(prof.MyName, Is.EqualTo(profile.MyName));
        }

        [Test]
        public void Test5()
        {

            ConnectionServer server = new ConnectionServer();

            server.LoadMessage("v", "v");

            Assert.True(server.Messages.Value != null);

            server.ContactsStatusUpdate("v", true);

            Assert.True(server.StatusUpdate.Key == new KeyValuePair<string, bool>("v", true).Key);
        }

        [Test]
        public void Test6()
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
                MyName = "i"
            };

            ConnectionServer server = new ConnectionServer();

            var mock = new Mock<IMessengerServerService>();

            var list = new[]
            {
                new Friend
                {
                    Name = "Man",
                    Online = true
                },
                new Friend
                {
                    Name = "LOL",
                    Online = true
                }
            };

            mock.Setup(foo => foo.FindUser("dsa")).Returns(list);

            server.Client = mock.Object;

            server.AddContact("dsa");

            mock.Verify(foo => foo.FindUser("dsa"), Times.Once);
        }

        [Test]
        public void Test7()
        {
            ConnectionServer server = new ConnectionServer();

            var mock = new Mock<IMessengerServerService>();

            server.Client = mock.Object;

            server.Save(null);

            mock.Verify(foo => foo.UploadingUserData(It.IsAny<User>()), Times.Never);
        }


    }
}