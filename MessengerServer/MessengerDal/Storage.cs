using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using log4net;
using Model;
using MySql.Data.MySqlClient;

namespace MessengerDal
{
    public class Storage : IStorage
    {
        private readonly string _connectionString;


        private readonly List<KeyValuePair<string, long>> _idsDictionary = new List<KeyValuePair<string, long>>();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="nameConnectionString">Строка подключения</param>
        public Storage(string nameConnectionString = "LocalDatabase")
        {
            _connectionString = ConfigurationManager.ConnectionStrings[nameConnectionString].ConnectionString;
        }

        private static ILog Log
        {
            get { return LogManager.GetLogger(typeof (Storage)); }
        }

        /// <summary>
        /// Сохраняет пользователя в бд
        /// </summary>
        /// <param name="user"></param>
        public void Save(User user)
        {
            var userId = SimplyGetIdFromName(user.Name);
            var listFreinds =
                user.Contacts.Select(
                    friend => new Contact {ContactId = SimplyGetIdFromName(friend.Name), ProfileId = 1, UserId = userId})
                    .ToList();
            SaveContacts(listFreinds);
        }

        /// <summary>
        /// сохраняет контакты в бд
        /// </summary>
        /// <param name="contacts"></param>
        private void SaveContacts(List<Contact> contacts)
        {
            if (contacts.Count == 0)
                return;
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);
                        var usId = contacts[0].UserId;
                        var dbCont = from contac in context.Contacts
                            where contac.UserId == usId
                            select contac;
                        context.Contacts.RemoveRange(dbCont);
                        context.Contacts.AddRange(contacts);
                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке сохранения контактов", exception);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Обновляет статус пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<string> UpdateStatus(string userName, bool status)
        {
            var userId = SimplyGetIdFromName(userName);
            var listIdUsers = UpdateProfile(new Profile {Online = status, ProfileId = userId, ProfileName = userName});
            return listIdUsers.Select(idUser => (SimplyGetNameFromId(idUser))).ToList();
        }

        /// <summary>
        /// получает id пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private long SimplyGetIdFromName(string userName)
        {
            long userId;
            if (_idsDictionary.Any(p => p.Key == userName))
                userId = _idsDictionary.Find(p => p.Key == userName).Value;
            else
            {
                userId = FindIdProfileByHe(userName);
                _idsDictionary.Add(new KeyValuePair<string, long>(userName, userId));
            }
            return userId;
        }

        /// <summary>
        /// Получает имя пользователя
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        private string SimplyGetNameFromId(long idUser)
        {
            return (_idsDictionary.Any(p => p.Value == idUser))
                ? _idsDictionary.Find(p => p.Value == idUser).Key
                : FindNameProfileByHe(idUser);
        }

        /// <summary>
        /// отправляет сообщение в бд,которое было написано,когда пользователь был ofline
        /// </summary>
        /// <param name="usernameSenders">отправитель</param>
        /// <param name="usernameReceiver">получатель</param>
        /// <param name="message">сообщение</param>
        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
            var idSender = SimplyGetIdFromName(usernameSenders);
            var idReciver = SimplyGetIdFromName(usernameReceiver);
            SendMessage(idSender, idReciver, message);
        }

        /// <summary>
        /// Загружает пользователя из бд
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User Load(string userName)
        {
            var userId = SimplyGetIdFromName(userName);
            if (userId == 0)
            {
                _idsDictionary.Remove(new KeyValuePair<string, long>(userName, 0));
                SaveProfile(new Profile {ProfileId = 1, Online = true, ProfileName = userName});
                _idsDictionary.Add(new KeyValuePair<string, long>(userName, FindIdProfileByHe(userName)));
                return new User
                {
                    Name = userName,
                    Contacts = new List<Friend>(),
                    MessageBySender = new List<KeyValuePair<string, string>>()
                };
            }
            var listMessage = LoadMessages(userId);
            var listMessageBySender =
                listMessage.Select(
                    message =>
                        new KeyValuePair<string, string>(message.Messages,
                            SimplyGetNameFromId(message.SenderId))).ToList();


            var listIdContacts = LoadContacts(userId);

            foreach (var message in listMessage)
            {
                if (!listIdContacts.Contains(message.SenderId))
                    listIdContacts.Add(message.SenderId);
            }

            var friends = listIdContacts.Select(idContact => new Friend
            {
                Name = SimplyGetNameFromId(idContact),
                Online = CheckStatus(idContact)
            }).ToList();

            return new User {Contacts = friends, MessageBySender = listMessageBySender, Name = userName};
        }

        /// <summary>
        /// поиск пользователя в списке друзей
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Friend> FindUser(string userName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                var friends = new List<Friend>();

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);
                        var profiles = from profile in context.Profiles
                            where profile.ProfileName.Contains(userName)
                            select profile;

                        foreach (var prof in profiles)
                        {
                            friends.Add(new Friend {Name = prof.ProfileName, Online = prof.Online});
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при найти друзей имени", exception);
                    transaction.Rollback();
                    throw;
                }

                return friends;
            }
        }
        /// <summary>
        /// Смотрит статус пользователя по имени
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckStatus(string userName)
        {
            return CheckStatus(SimplyGetIdFromName(userName));
        }
        public void Dispose()
        {
        }

        /// <summary>
        /// ищет id профиля по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private long FindIdProfileByHe(string name)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                long profileId;

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);
                        var id = from profile in context.Profiles
                            where profile.ProfileName == name
                            select profile.ProfileId;
                        profileId = id.ToList().FirstOrDefault();
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке поиска id профиля по его имени", exception);
                    transaction.Rollback();
                    throw;
                }

                return profileId;
            }
        }

        /// <summary>
        /// Ищет имя пользователя по id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string FindNameProfileByHe(long id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                string profileName;

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);
                        var names = from profile in context.Profiles
                            where profile.ProfileId == id
                            select profile.ProfileName;
                        profileName = names.ToList().FirstOrDefault();
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке поиска имени профиля по его id", exception);
                    transaction.Rollback();
                    throw;
                }

                return profileName;
            }
        }

        /// <summary>
        /// смотрит статус пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CheckStatus(long id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                bool status;

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);
                        status = context.Profiles.Find(id).Online;
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке проверить статус контакта", exception);
                    transaction.Rollback();
                    throw;
                }

                return status;
            }
        }

        /// <summary>
        /// реализация отправки сообщения
        /// </summary>
        /// <param name="sendersId">id отправителя</param>
        /// <param name="receiverId">id получателя</param>
        /// <param name="message">сообщение</param>
        private void SendMessage(long sendersId, long receiverId, string message)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();
                var mess = new Message {Messages = message, UserId = receiverId, SenderId = sendersId};

                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);

                        context.Messages.Add(mess);

                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке отправить сообщение", exception);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// загрузка списка контактов из бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<long> LoadContacts(long id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                List<long> contacts;

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);

                        var con = from contact in context.Contacts
                            where contact.UserId == id
                            select contact.ContactId;
                        contacts = con.ToList();
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке загрузить контактов", exception);
                    transaction.Rollback();
                    throw;
                }

                return contacts;
            }
        }

        /// <summary>
        /// удаляет прочитанное сообщение из бд
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="context"></param>
        private void RemoveReadeadMessage(List<Message> messages, StorageContext context)
        {
            foreach (var message in messages)
            {
                context.Messages.Remove(message);
            }
        }

        /// <summary>
        /// загрузка сообщений из бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<Message> LoadMessages(long id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                List<Message> messages;

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);

                        var mes = from message in context.Messages
                            where message.UserId == id
                            select message;
                        messages = mes.ToList();
                        RemoveReadeadMessage(messages, context);
                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке загрузить сообщения", exception);
                    transaction.Rollback();
                    throw;
                }

                return messages;
            }
        }

        /// <summary>
        /// сбрасывает бд
        /// </summary>
        public void DropDb()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);
                        context.Database.Delete();
                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке удалении базы данных", exception);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// сохраняет пользователя в бд
        /// </summary>
        /// <param name="user"></param>
        private void SaveProfile(Profile user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);

                        context.Profiles.Add(user);

                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке сохранения профиля", exception);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// обновляет профиль
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private List<long> UpdateProfile(Profile profile)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();
                List<long> result;

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);

                        context.Entry(profile).State = EntityState.Modified;

                        result = GetNotificationsList(context, profile.ProfileId);

                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error("Ошибка при попытке обновить статус профиля", exception);
                    transaction.Rollback();
                    throw;
                }
                return result;
            }
        }

        /// <summary>
        /// получает список уведомлений из бд
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<long> GetNotificationsList(StorageContext context, long id)
        {
            var mes = from contact in context.Contacts
                where contact.UserId == id
                select contact.ContactId;
            return mes.ToList();
        }

        /// <summary>
        /// сохраняет изменения в бд
        /// </summary>
        /// <param name="context"></param>
        private void SaveChanges(StorageContext context)
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.Single().Reload();
                }
            } while (saveFailed);
        }
    }
}