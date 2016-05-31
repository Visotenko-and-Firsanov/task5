using System.Data.Common;
using System.Data.Entity;
using MySql.Data.Entity;

namespace MessengerServer
{
    [DbConfigurationType(typeof (MySqlEFConfiguration))]
    public class StorageContext : DbContext
    {
        public StorageContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DbContactMap());
        }

        public IDbSet<Contact> Contacts => Set<Contact>();
    }
}
