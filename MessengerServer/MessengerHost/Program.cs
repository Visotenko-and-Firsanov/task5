using System;
using System.ServiceModel;

namespace MessengerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(MessengerServer.MessengerServerService)))
            {
                host.Open();
                Console.WriteLine("Host strated...");
                Console.ReadLine();
                host.Close();
            }
            //var temp = new Storage();
            //temp.SaveProfile(new Profile { Online = true, ProfileId = 34, ProfileName = "Tina" });
            //var res = temp.UpdateProfile(new Profile { Online = true, ProfileId = 1, ProfileName = "Tina" });
            //var st = temp.CheckStatus(1);
            //temp.UpdateProfile(new Profile { Online = false, ProfileId = 1, ProfileName = "Tina"});
            //st = temp.CheckStatus(1);
            //var prof = temp.LoadProfile(1);
            //temp.SaveContact(new Contact{ContactId = 2, ProfileId = 2, UserId = 1});
            //temp.SaveContact(new Contact { ContactId = 5, ProfileId = 5, UserId = 1 });
            //var con = temp.LoadContacts(1);
            //temp.SaveMessage(new Message{Messages = "Hello World", ProfileId = 34, SenderId = 2, UserId = 1});
            //temp.SaveMessage(new Message { Messages = "Ohaio", ProfileId = 34, SenderId = 5, UserId = 1 });
            //var mes = temp.LoadMessages(1);
            
        }
    }
}
