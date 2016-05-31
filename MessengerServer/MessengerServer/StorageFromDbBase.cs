using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MessengerServer
{
    public class StorageFromDbBase : IDisposable
    {
        private readonly StorageContext _context;

        public StorageFromDbBase(DbConnection existingConnection, bool contextOwnsConnection)
        {
            _context = new StorageContext(existingConnection, contextOwnsConnection);
            _context.Database.ExecuteSqlCommand("DROP DATABASE IF EXISTS messeng;CREATE DATABASE messeng; USE messeng; ");
        }

        public void Save(List<Contact> contacts)
        {
            foreach (var contact in contacts)
                _context.Contacts.Add(contact);
        }

        public void Delete(List<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                var st = _context.Contacts.First(stu => stu.Id == contact.Id);
                _context.Contacts.Remove(st);
            }

        }

        public List<Contact> Load()
        {
            var result = _context.Contacts.Local;
            return result.ToList();
        }

        public void ClearDb()
        {
            _context.Database.ExecuteSqlCommand("DROP TABLE Contact");
        }

        public void Dispose()
        {

        }
    }
}
