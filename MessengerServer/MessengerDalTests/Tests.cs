using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MessengerDal;
using NUnit.Framework;

namespace MessengerDalTests
{
    [TestFixture]
    public class Tests
    {
        private Storage _storage;
        [SetUp] 
        public void CreateAndFillDb()
        {
            _storage = new Storage("TestDb");
            _storage.Load("Andrew");
            _storage.Load("Tina");
            _storage.Load("Alex");
            _storage.Load("Nikita");
            _storage.Load("Vladimir");
            _storage.Save(new User{Name = "Andrew", Contacts = new List<Friend>{new Friend{Name = "Vladimir", Online = true}, new Friend{Name = "Tina", Online = false}, new Friend{Name = "Alex", Online = false}}, MessageBySender = new List<KeyValuePair<string, string>>()});
            _storage.Save(new User{Name = "Tina", Contacts = new List<Friend>{ new Friend{Name = "Andrew", Online = false}, new Friend{Name = "Alex", Online = false}}, MessageBySender = new List<KeyValuePair<string, string>>{new KeyValuePair<string, string>("Hi Tina", "Alex")}});
            _storage.Save(new User{Name = "Alex", Contacts = new List<Friend>{new Friend{Name = "Vladimir", Online = true}}, MessageBySender = new List<KeyValuePair<string, string>>()});
            _storage.Save(new User{Name = "Nikita", Contacts = new List<Friend>(), MessageBySender = new List<KeyValuePair<string, string>>()});
        }

        [TearDown]
        public void DeleteDb()
        {
            _storage.DropDb();
        }

        [Test]
        public void TestLoadUser()
        {
            var user = _storage.Load("Andrew");
            var contacts = new List<Friend>{new Friend{Name = "Vladimir", Online = true}, new Friend{Name = "Tina", Online = true}, new Friend{Name = "Alex", Online = true}};
            Assert.That(user.Name, Is.EqualTo("Andrew"));
            var counter = 0;
            foreach (var contact in user.Contacts)
            {
                Assert.That(contact.Name, Is.EqualTo(contacts[counter].Name));
                Assert.That(contact.Online, Is.EqualTo(contacts[counter].Online));
                ++counter;
            }
            Assert.That(user.MessageBySender.Count, Is.EqualTo(0));
        }


        [Test]
        public void TestSave()
        {
            var user = _storage.Load("Andrew");
            var contacts = new List<Friend> {new Friend { Name = "Tina", Online = true }, new Friend { Name = "Alex", Online = true } };
            user.Contacts = contacts;
            _storage.Save(user);
            user = _storage.Load("Andrew");
            Assert.That(user.Name, Is.EqualTo("Andrew"));
            var counter = 0;
            foreach (var contact in user.Contacts)
            {
                Assert.That(contact.Name, Is.EqualTo(contacts[counter].Name));
                Assert.That(contact.Online, Is.EqualTo(contacts[counter].Online));
                ++counter;
            }
            Assert.That(user.MessageBySender.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestCheckStatus()
        {
            Assert.That(_storage.CheckStatus("Andrew"), Is.True);
        }

        [Test]
        public void TestUpdateStaus()
        {
            Assert.That(_storage.CheckStatus("Andrew"), Is.True);
            var list = _storage.UpdateStatus("Andrew", false);
            Assert.That(_storage.CheckStatus("Andrew"), Is.False);
            var counter = 0;
            var contacts = new List<string> {"Vladimir", "Tina", "Alex"};
            foreach (var contact in list)
            {
                Assert.That(contact, Is.EqualTo(contacts[counter]));
                ++counter;
            }
        }

        [Test]
        public void TestUpdateStaus2()
        {
            var storage = new Storage("TestDb");
            Assert.That(storage.CheckStatus("Andrew"), Is.True);
            var list = storage.UpdateStatus("Andrew", false);
            Assert.That(storage.CheckStatus("Andrew"), Is.False);
            var counter = 0;
            var contacts = new List<string> { "Vladimir", "Tina", "Alex" };
            foreach (var contact in list)
            {
                Assert.That(contact, Is.EqualTo(contacts[counter]));
                ++counter;
            }
        }

        [Test]
        public void TestSendMessage()
        {
            _storage.SendMessage("Tina", "Andrew", "Hello");
            var user = _storage.Load("Andrew");
            Assert.That(new KeyValuePair<string,string>("Hello", "Tina"), Is.EqualTo(user.MessageBySender.First()));
        }

        [Test]
        public void TestFindUser()
        {
            _storage.Load("Andrewnio");
            var users = _storage.FindUser("Andrew");
            var list = new List<Friend>{new Friend{Name = "Andrew", Online = true}, new Friend{Name = "Andrewnio", Online = true}};
            var count = 0;
            foreach (var friend in users)
            {
                Assert.That(friend.Name, Is.EqualTo(list[count].Name));
                Assert.That(friend.Online, Is.EqualTo(list[count].Online));
                ++count;
            }
        }

        [Test]
        public void TestExeption()
        {
            try
            {
                _storage.CheckStatus("Romero");
            }
            catch (Exception exception)
            {
                Assert.That("Ссылка на объект не указывает на экземпляр объекта.", Is.EqualTo(exception.Message));
            }
            
        }
    }
}
