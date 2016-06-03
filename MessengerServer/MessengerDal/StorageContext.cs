using System.Data.Common;
using System.Data.Entity;
using Model;
using MySql.Data.Entity;

namespace MessengerDal
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class StorageContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }

        public StorageContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Profile>().ToTable("profiles");
            modelBuilder.Entity<Profile>().HasKey(pr => pr.ProfileId);
            modelBuilder.Entity<Profile>().Property(pr => pr.ProfileId).HasColumnName("ProfileId");
            modelBuilder.Entity<Profile>().Property(pr => pr.ProfileName).HasColumnName("Name").IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Profile>().Property(pr => pr.Online).HasColumnName("Online").IsRequired();
            
            modelBuilder.Entity<Contact>().ToTable("contacts");
            modelBuilder.Entity<Contact>().HasKey(ct => ct.ProfileId);
            modelBuilder.Entity<Contact>().Property(ct => ct.UserId).HasColumnName("UserId").IsRequired();
            modelBuilder.Entity<Contact>().Property(ct => ct.ContactId).HasColumnName("ContactId").IsRequired();
            
            modelBuilder.Entity<Message>().ToTable("messages");
            modelBuilder.Entity<Message>().HasKey(ms => ms.ProfileId);
            modelBuilder.Entity<Message>().Property(ms => ms.UserId).HasColumnName("UserId").IsRequired();
            modelBuilder.Entity<Message>().Property(ms => ms.SenderId).HasColumnName("SenderId").IsRequired();
            modelBuilder.Entity<Message>().Property(ms => ms.Messages).HasColumnName("Messages").IsRequired().HasMaxLength(100);

        }
    }
}