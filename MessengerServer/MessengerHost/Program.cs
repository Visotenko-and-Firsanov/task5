using System;
using System.ServiceModel;
using MessengerServer;

namespace MessengerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(MessengerServer.MessengerServerService)))
            {
                host.Open();
                var service = new MessengerServerService();
                Console.WriteLine("Host strated...");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
