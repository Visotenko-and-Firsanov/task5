using System;
using System.ServiceModel;
using log4net;
using MessengerDal;
using MessengerServer;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace MessengerHost
{
    internal class Host
    {
        /// <summary>
        /// регестрирую когда сервер был запущен
        /// </summary>
        static ILog Log
        {
            get
            {
                return LogManager.GetLogger(typeof(Host));
            }
        }
        /// <summary>
        /// запуск сервера
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof (MessengerServerService)))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("Host started...");
                    Console.ReadLine();
                    host.Close();
                }
                catch (Exception exception)
                {
                    Log.Error("Сервер не был запущен из-за возникшей ошибки",exception);
                }
            }
        }
    }
}