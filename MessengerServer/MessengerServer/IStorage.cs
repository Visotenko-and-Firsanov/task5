using System;
using System.Collections.Generic;
using System.Linq;

namespace MessengerServer
{
    public interface IStorage : IDisposable
    {
        void Save(Profile contact);
        void Save(List<Profile> contacts);
        void Delete(List<Profile> contacts);
        void UpdateStatus(string userName, bool status);
        void SendMessage(string usernameSenders, string usernameReceiver, string message);
        List<string> GetOnlineMembers(string userName);
        bool CheckStatus(string userName);
        Profile LoadOneProfile(string userName);
        List<Profile> Load(string userName);
    }
}
