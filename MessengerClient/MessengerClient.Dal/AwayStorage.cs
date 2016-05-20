using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Presentation;
using WcfContrib.Hosting;


namespace MessengerClient.Dal
{
    public class AwayStorage : IConection
    {
        private readonly string _address;

        AwayStorage(string addres)
        {
        //   ServiceHost das = new ServiceHost(); вернемся к этому позже
        }

        public string LoadMessage(string name, string message)
        {
            return name;
        }
    }
}
