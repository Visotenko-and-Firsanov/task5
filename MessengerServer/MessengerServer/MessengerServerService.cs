using System;
using System.Collections.Generic;
using System.ServiceModel;
using MessengerDal;

namespace MessengerServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class MessengerServerService : IMessengerServerService
    {
        private readonly Dictionary<string, IMessengerServerServiceCallback> _callbacks =
            new Dictionary<string, IMessengerServerServiceCallback>();
        private readonly Dictionary<string, string> _clientSession =
           new Dictionary<string, string>();

        private readonly Storage _storage = new Storage();

        private void Disconnect(object sender, EventArgs e)
        {
            var userName = _clientSession[((IClientChannel) sender).SessionId];   
            Console.WriteLine("Client {0} is disconnect", userName);
            _callbacks[userName] = null;
            _clientSession[userName] = null;
            CallAllNotificationUsers(_storage.UpdateStatus(userName, false), userName, false);
            
        }

        public User UploadUserData(string username)
        {
            var user = _storage.Load(username);
            _clientSession[OperationContext.Current.Channel.SessionId] = username;
            _callbacks[user.Name] = OperationContext.Current.GetCallbackChannel<IMessengerServerServiceCallback>();
            OperationContext.Current.Channel.Faulted += Disconnect;
            Console.WriteLine("Client {0} is connect", user.Name);
            CallAllNotificationUsers(_storage.UpdateStatus(user.Name, true), user.Name, true);
           // CallAllNotificationUsers(_storage.UpdateStatus(username, true), user.Name, true);
            return user;
        }

        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
            if (_storage.CheckStatus(usernameReceiver))
                _callbacks[usernameReceiver].LoadMessage(usernameSenders, message);
            else
                _storage.SendMessage(usernameSenders, usernameReceiver, message);
        }

        public Friend FindUser(string requiredUsername)
        {
            return _storage.FindUser(requiredUsername);
        }

        public void UploadingUserData(User user)
        {
            CallAllNotificationUsers(_storage.UpdateStatus(user.Name, false), user.Name, false);
            _callbacks[user.Name] = null;
            _storage.Save(user);
        }

        private void CallAllNotificationUsers(List<string> nameList, string nameReciver, bool status)
        {

            foreach (var name in nameList)
            {
                if (_callbacks.ContainsKey(name) && _callbacks[name] != null)
                    _callbacks[name].ContactsStatusUpdate(nameReciver, status);
            }
        }
    }
} 