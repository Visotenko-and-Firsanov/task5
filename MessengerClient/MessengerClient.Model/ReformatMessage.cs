using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Model
{
    public static class ReformatMessage
    {
        public static string Reformat(string name, string message)
        {
            StringBuilder reformatMessage = new StringBuilder();

            reformatMessage.Append(string.Format("{0} : \n {1} \n", name, message));

            return reformatMessage.ToString();
        }
    }
}