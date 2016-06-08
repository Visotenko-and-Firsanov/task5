using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using log4net;
using log4net.Config;
using MessengerDal;

[assembly: XmlConfigurator(Watch = true)]

namespace MessengerServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MessengerServerService : IMessengerServerService
    {
        private readonly Dictionary<string, IMessengerServerServiceCallback> _callbacks =
            new Dictionary<string, IMessengerServerServiceCallback>();

        private readonly Dictionary<string, string> _clientSession =
            new Dictionary<string, string>();

        private readonly IStorage _storage;
        private readonly ReaderWriterLockSlim _storageLock = new ReaderWriterLockSlim();

        /// <summary>
        /// конструктор поумолчанию
        /// </summary>
        public MessengerServerService()
        {
            _storage = new Storage();
        }

        /// <summary>
        /// конструктор с параметром
        /// </summary>
        /// <param name="storage"></param>
        public MessengerServerService(IStorage storage)
        {
            _storage = storage;
        }

        private static ILog Log
        {
            get { return LogManager.GetLogger(typeof (MessengerServerService)); }
        }

        /// <summary>
        /// Загрузка данных пользователя в бд
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User UploadUserData(string username)
        {
            User user;
            _storageLock.EnterReadLock();
            try
            {
                user = _storage.Load(username);
            }
            catch (Exception exception)
            {
                Log.Error("Невозможно загрузить данные", exception);
                throw new Exception("Невозможно загрузить данные");
            }
            finally
            {
                _storageLock.ExitReadLock();
            }

            _storageLock.EnterWriteLock();
            try
            {
                _clientSession[OperationContext.Current.Channel.SessionId] = username;
                _callbacks[user.Name] = OperationContext.Current.GetCallbackChannel<IMessengerServerServiceCallback>();
            }
            catch (Exception exception)
            {
                Log.Error("Невозможно получить коллбэк", exception);
                throw new Exception("Невозможно получить коллбэк");
            }
            finally
            {
                _storageLock.ExitWriteLock();
            }

            OperationContext.Current.Channel.Faulted += Disconnect;

            var clientStatusInfo = "Client " + user.Name + " is connect";
            Log.Info(clientStatusInfo);
            Console.WriteLine(clientStatusInfo);

            List<string> contList;
            _storageLock.EnterReadLock();
            try
            {
                contList = _storage.UpdateStatus(user.Name, true);
            }
            finally
            {
                _storageLock.ExitReadLock();
            }
            CallAllNotificationUsers(contList, user.Name, true);
            return user;
        }

        /// <summary>
        /// отправка сообщения
        /// </summary>
        /// <param name="usernameSenders">отправитель</param>
        /// <param name="usernameReceiver">получатель</param>
        /// <param name="message">сообщение</param>
        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
            bool statusUser;
            _storageLock.EnterReadLock();
            try
            {
                statusUser = _storage.CheckStatus(usernameReceiver);
            }
            finally
            {
                _storageLock.ExitReadLock();
            }


            if (statusUser)
            {
                _storageLock.EnterReadLock();
                try
                {
                    _callbacks[usernameReceiver].LoadMessage(usernameSenders, message);
                }
                finally
                {
                    _storageLock.ExitReadLock();
                }
            }
            else
            {
                _storageLock.EnterWriteLock();
                try
                {
                    _storage.SendMessage(usernameSenders, usernameReceiver, message);
                }
                finally
                {
                    _storageLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// поиск пользователей в спсиске друзей
        /// </summary>
        /// <param name="requiredUsername"></param>
        /// <returns></returns>
        public List<Friend> FindUser(string requiredUsername)
        {
            List<Friend> frend;
            _storageLock.EnterReadLock();
            try
            {
                frend = _storage.FindUser(requiredUsername);
            }
            finally
            {
                _storageLock.ExitReadLock();
            }
            return frend;
        }

        /// <summary>
        /// выгружаем из базы данных
        /// </summary>
        /// <param name="user"></param>
        public void UploadingUserData(User user)
        {
            List<string> contList;

            _storageLock.EnterReadLock();
            try
            {
                contList = _storage.UpdateStatus(user.Name, true);
            }
            finally
            {
                _storageLock.ExitReadLock();
            }
            CallAllNotificationUsers(contList, user.Name, false);
            _storageLock.EnterWriteLock();
            try
            {
                _callbacks[user.Name] = null;
                _storage.Save(user);
            }
            finally
            {
                _storageLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// рарыв соединения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnect(object sender, EventArgs e)
        {
            string userName;

            _storageLock.EnterReadLock();
            try
            {
                userName = _clientSession[((IClientChannel) sender).SessionId];
            }
            finally
            {
                _storageLock.ExitReadLock();
            }

            var clientStatusInfo = "Client " + userName + " is disconnect";
            Log.Info(clientStatusInfo);
            Console.WriteLine(clientStatusInfo);

            _storageLock.EnterWriteLock();
            try
            {
                _callbacks[userName] = null;
                _clientSession[userName] = null;
            }
            finally
            {
                _storageLock.ExitWriteLock();
            }

            List<string> contList;
            _storageLock.EnterReadLock();
            try
            {
                contList = _storage.UpdateStatus(userName, false);
            }
            finally
            {
                _storageLock.ExitReadLock();
            }
            CallAllNotificationUsers(contList, userName, false);
        }

        /// <summary>
        /// вызывает все уведомления пользователей
        /// </summary>
        /// <param name="nameList"></param>
        /// <param name="nameReciver"></param>
        /// <param name="status"></param>
        private void CallAllNotificationUsers(List<string> nameList, string nameReciver, bool status)
        {
            foreach (var name in nameList.Where(name => _callbacks.ContainsKey(name) && _callbacks[name] != null))
            {
                _storageLock.EnterReadLock();
                try
                {
                    _callbacks[name].ContactsStatusUpdate(nameReciver, status);
                }
                finally
                {
                    _storageLock.ExitReadLock();
                }
            }
        }
    }
}