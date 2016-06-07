using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MessengerClient.Model.Tests
{
    public class Tests
    {
        [TestFixture]

        public class Test
        {

            [Test]
            public void TestContactCreate()
            {
                var contact = new Contact
                {
                    MessageHistory = "2412",
                    Name = "42",
                    Online = true
                };

                Assert.That(contact.MessageHistory == "2412" &&
                            contact.Name == "42" &&
                            contact.Online, Is.True);
            }

            [Test]
            public void TestProfileCreate()
            {
                var student = new Contact
                {
                    MessageHistory = "2412",
                    Name = "42",
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
                    MyName = "SuperCat"
                };

                Assert.That(profile.MyName == "SuperCat" &&
                            profile.MyContacts == contactList);
            }
        }
    }
}
