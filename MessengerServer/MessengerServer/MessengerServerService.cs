using System;
using MySql.Data.MySqlClient;

namespace MessengerServer
{
    public class MessengerServerService : IMessengerServerService
    {
        public StorageFromDbBase Context { get; }

        public MessengerServerService()
        {
            using (var conn = new MySqlConnection("server=localhost;port=3306;uid=root;password=1234;"))
            {
                conn.Open();

                using (Context = new StorageFromDbBase(conn, false))
                {
                }
            }
        }

        public void UploadUserData(string username)
        {
            throw new NotImplementedException();
        }

        public void RefreshUserData(string username)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
            throw new NotImplementedException();
        }

        public void FindUser(string requiredUsername)
        {
            throw new NotImplementedException();
        }

        public void UserStatusCheck(string requiredUsername)
        {
            throw new NotImplementedException();
        }

        public void GetMessage(string usernameSenders, string usernameReceiver)
        {
            throw new NotImplementedException();
        }

        public void UploadingUserData(string username)
        {
            throw new NotImplementedException();
        }
    }
}
