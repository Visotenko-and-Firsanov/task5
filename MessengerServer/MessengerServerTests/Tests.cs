using System;
using System.Collections.Generic;
using System.Diagnostics;
using MessengerDal;
using MessengerServer;
using Moq;
using NUnit.Framework;

namespace MessengerServerTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestCreate()
        {
            var mock = new Mock<IStorage>();
            var server = new MessengerServerService(mock.Object);
            Assert.That(server != null, Is.True);
        }

        [Test]
        public void TestFindUser()
        {

            var mock = new Mock<IStorage>();
            var server = new MessengerServerService(mock.Object);
            mock.Setup(m => m.Load("Andrew"))
                .Returns(new User
                {
                    Name = "Andrew",
                    Contacts = new List<Friend>(),
                    MessageBySender = new List<KeyValuePair<string, string>>()
                });
            mock.Setup(m => m.FindUser("Andrew")).Returns(new List<Friend>{new Friend{Name = "Andrew", Online = true}});
            server.FindUser("Andrew");
            mock.Verify(m => m.FindUser("Andrew"), Times.Once);
        }

        [Test]
        public void TestSendMessage()
        {

            var mock = new Mock<IStorage>();
            var server = new MessengerServerService(mock.Object);
            mock.Setup(m => m.SendMessage("Andrew", "Vladimir", "Hi"));
            mock.Setup(m => m.CheckStatus("Vladimir")).Returns(false);
            server.SendMessage("Andrew", "Vladimir", "Hi");
            mock.Verify(m => m.SendMessage("Andrew", "Vladimir", "Hi"), Times.Once);
        }

        [Test]
        public void TestSendMessageEx()
        {
            try
            {
                var mock = new Mock<IStorage>();
                var server = new MessengerServerService(mock.Object);
                mock.Setup(m => m.SendMessage("Andrew", "Vladimir", "Hi"));
                mock.Setup(m => m.CheckStatus("Vladimir")).Returns(true);
                server.SendMessage("Andrew", "Vladimir", "Hi");
            }
            catch (Exception exeption)
            {
                Assert.That("Данный ключ отсутствует в словаре.", Is.EqualTo(exeption.Message));
            }
        }
        [Test]
        public void TestUploadingUserDataEx()
        {
            try
            {
                var mock = new Mock<IStorage>();
                var server = new MessengerServerService(mock.Object);
                mock.Setup(m => m.UpdateStatus("Andrew",true)).Returns(new List<string>());
                var ys = new User
                {
                    Name = "Andrew",
                    Contacts = new List<Friend>(),
                    MessageBySender = new List<KeyValuePair<string, string>>()
                };
                server.UploadingUserData(ys);
            }
            catch (Exception exeption)
            {
                Assert.That("Данный ключ отсутствует в словаре.", Is.EqualTo(exeption.Message));
            }
        }
        [Test]
        public void TestUploadUserDataEx()
        {
            try
            {
                var mock = new Mock<IStorage>();
                var server = new MessengerServerService(mock.Object);
                mock.Setup(m => m.Load("Andrew"))
                  .Returns(new User
                  {
                      Name = "Andrew",
                      Contacts = new List<Friend>(),
                      MessageBySender = new List<KeyValuePair<string, string>>()
                  });
                server.UploadUserData("Andrew");
            }
            catch (Exception exeption)
            {
                Assert.That("Невозможно получить коллбэк", Is.EqualTo(exeption.Message));
            }
        }
    }
}
