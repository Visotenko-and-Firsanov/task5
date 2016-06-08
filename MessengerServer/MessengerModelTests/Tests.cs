using Model;
using NUnit.Framework;

namespace MessengerModelTests
{
    class Program
    {
        private static void Main(string[] args)
        {
        }
    }
    [TestFixture]
    class Tests
    {
        [Test]
        public void TestCreateContatc()
        {
            var contact = new Contact{ContactId = 1, ProfileId = 0, UserId = 2};
            Assert.That(contact.ProfileId, Is.EqualTo(0));
            Assert.That(contact.UserId, Is.EqualTo(2));
            Assert.That(contact.ContactId, Is.EqualTo(1));
        }

        [Test]
        public void TestCreateProfile()
        {
            var contact = new Profile{ ProfileId = 0, Online = true, ProfileName = "Andrew"};
            Assert.That(contact.ProfileId, Is.EqualTo(0));
            Assert.That(contact.Online, Is.True);
            Assert.That(contact.ProfileName, Is.EqualTo("Andrew"));
        }


        [Test]
        public void TestCreateMessage()
        {
            var contact = new Message{ SenderId = 1, ProfileId = 0, UserId = 2, Messages = "Hi"};
            Assert.That(contact.ProfileId, Is.EqualTo(0));
            Assert.That(contact.UserId, Is.EqualTo(2));
            Assert.That(contact.SenderId, Is.EqualTo(1));
            Assert.That(contact.Messages, Is.EqualTo("Hi"));
        }
    }
}
