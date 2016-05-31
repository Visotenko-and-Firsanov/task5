using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MessengerServer
{
    public class StorageFromDbBase : IStorage
    {
        private readonly StorageContext _context;

        public StorageFromDbBase(DbConnection existingConnection, bool contextOwnsConnection)
        {
            _context = new StorageContext(existingConnection, contextOwnsConnection);
            _context.Database.CreateIfNotExists();
            //_context.
        }

        public void Save(Profile contact)
        {
            _context.Profile.Add(contact);
        }

        public void Save(List<Profile> contacts)
        {
            foreach (var contact in contacts)
                _context.Profile.Add(contact);
        }

        public void Delete(List<Profile> contacts)
        {
            foreach (var contact in contacts)
            {
                var st = _context.Profile.First(stu => stu.Name == contact.Name);
                _context.Profile.Remove(st);
            }

        }
		
		public void UpdateStatus(string userName, bool status)
		{
			LoadOneProfile(userName).Online = status;
		}

        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
            //var contacts = LoadOneProfile(usernameReceiver).Contacts;
            //bool haveContact = false;
            //foreach (var contact in contacts)
            //{
            //    if (contact.Name == usernameSenders)
            //    {
            //        haveContact = true;
            //        contact.Message = message;
            //    }
            //}
            //if (!haveContact)
            //{
            //    var newMember = new Contact {Name = usernameSenders, Online = true, Message = message};
            //    contacts.Add(newMember);
            //}
            
        }

        public void Dispose()
        {
            
        }
        public List<string> GetOnlineMembers(string userName)
        {
            return null;
            //var contacts = LoadOneProfile(userName).Contacts;
            //return (from contact in contacts where contact.Online select contact.Name).ToList();
        }

        public bool CheckStatus(string userName)
        {
            return LoadOneProfile(userName).Online;
        }

        public Profile LoadOneProfile(string userName)
        {
            return _context.Profile.Find(userName);

        }

        public List<Profile> Load(string userName)
        {
            var result = from contact in _context.Profile
                         where contact.Name == userName
                         select contact;
            return result.ToList();
        }

        public void ClearDb()
        {
            _context.Database.ExecuteSqlCommand("DROP TABLE Profile");
        }
    }
}