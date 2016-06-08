using System.Collections.Generic;

namespace MessengerDal
{

    public class User
    {
        public string Name { get; set; }
        public List<Friend> Contacts { get; set; }
        public List<KeyValuePair<string, string>> MessageBySender { get; set; }
    }

    public class Friend
    {
        public bool Online { get; set; }
        public string Name { get; set; }
    }
}