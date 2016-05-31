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
        }
    }
}
