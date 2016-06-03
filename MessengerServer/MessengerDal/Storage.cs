using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Model;
using MySql.Data.MySqlClient;

namespace MessengerDal
{
    public class Storage
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["LocalDatabase"].ConnectionString;

        private readonly List<KeyValuePair<string, long>> _idsDictionary = new List<KeyValuePair<string, long>>();

        public void Save(User user)
        {
            long userId = SimplyGetIdFromName(user.Name);

            foreach (var friend in user.Contacts)
            {
                var contId = SimplyGetIdFromName(friend.Name);
                SaveContact(new Contact {ContactId = contId, ProfileId = 1, UserId = userId});
            }
        }

        public List<string> UpdateStatus(string userName, bool status)
        {
            long userId = SimplyGetIdFromName(userName);
            var listIdUsers = UpdateProfile(new Profile {Online = status, ProfileId = userId, ProfileName = userName});
            return listIdUsers.Select(idUser => (SimplyGetNameFromId(idUser))).ToList();
        }

        private long SimplyGetIdFromName(string userName)
        {
            long userId = 0;
            if (_idsDictionary.Any(p => p.Key == userName))
                userId = _idsDictionary.Find(p => p.Key == userName).Value;
            else
            {
                userId = FindIdProfileByHe(userName);
                _idsDictionary.Add(new KeyValuePair<string, long>(userName, userId));
            }
            return userId;
        }

        private string SimplyGetNameFromId(long idUser)
        {
            return (_idsDictionary.Any(p => p.Value == idUser)) ? _idsDictionary.Find(p => p.Value == idUser).Key : FindNameProfileByHe(idUser);
        }
        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
            var idSender = SimplyGetIdFromName(usernameSenders);
            var idReciver= SimplyGetIdFromName(usernameReceiver);
            SendMessage(idSender,idReciver,message);
        }
        public User Load(string userName)
        {
            long userId = SimplyGetIdFromName(userName);
            if (userId == 0)
            {
                SaveProfile(new Profile{ProfileId = 1, Online = true, ProfileName = userName});
                return new User{Name = userName, Contacts = new List<Friend>(), MessageBySender = new List<KeyValuePair<string, string>>() };
            }
            var listIdContacts = LoadContacts(userId);
            var friends = listIdContacts.Select(idContact => new Friend
            {
                Name = SimplyGetNameFromId(idContact),
                Online = CheckStatus(idContact)
            }).ToList();

            var listMessage = LoadMessages(userId);

            var listMessageBySender =
                listMessage.Select(
                    message =>
                        new KeyValuePair<string, string>(message.Messages,
                            SimplyGetNameFromId(message.SenderId))).ToList();/*_idsDictionary.Any(p => p.Value == message.SenderId))
                                ? _idsDictionary.Find(p => p.Value == message.SenderId).Key
                                : FindNameProfileByHe(message.SenderId))*///).ToList();

            return new User {Contacts = friends, MessageBySender = listMessageBySender, Name = userName};
        }

        public Friend FindUser(string userName)
        {
            var userId = SimplyGetIdFromName(userName);
            return userId == 0 ? null : new Friend {Name = userName, Online = CheckStatus(userId)};
        }

        public bool CheckStatus(string userName)
        {
            return CheckStatus(SimplyGetIdFromName(userName));
        }

        public void Dispose()
        {
        }

        public long FindIdProfileByHe(string name)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return profileId;
            }
        }

        public string FindNameProfileByHe(long id)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return profileName;
            }
        }

        public bool CheckStatus(long id)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return status;
            }
        }

        public void SendMessage(long sendersId, long receiverId, string message)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<long> LoadContacts(long id)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return contacts;
            }
        }

        private void RemoveReadeadMessage(List<Message> messages, StorageContext context)
        {
            foreach (var message in messages)
            {
                context.Messages.Remove(message);
            }
        }

        public void SaveContact(Contact contact)
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

                        var cont = from contac in context.Contacts
                            where contac.UserId == contact.UserId && contac.ContactId == contact.ContactId
                            select contac;

                        if (!cont.Any())
                        {
                            context.Contacts.Add(contact);
                            SaveChanges(context);
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Message> LoadMessages(long id)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return messages;
            }
        }

        public void SaveMessage(Message message)
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

                        context.Messages.Add(message);

                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void SaveProfile(Profile user)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<long> UpdateProfile(Profile profile)
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
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                return result;
            }
        }

        public Profile LoadProfile(long id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new StorageContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                Profile user;

                try
                {
                    using (var context = new StorageContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);

                        user = context.Profiles.SingleOrDefault(u => u.ProfileId == id);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return user;
            }
        }

        private List<long> GetNotificationsList(StorageContext context, long id)
        {
            var mes = from contact in context.Contacts
                where contact.UserId == id
                select contact.ContactId;
            return mes.ToList();
        }

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