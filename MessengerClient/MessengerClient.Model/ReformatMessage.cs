using System.Text;

namespace MessengerClient.Model
{
    public static class ReformatMessage
    {
        public static string Reformat(string name, string message)
        {
            var reformatMessage = new StringBuilder();

            reformatMessage.Append($"{name} : \n {message} \n");

            return reformatMessage.ToString();
        }
    }
}