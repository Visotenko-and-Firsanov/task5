using System.Data.Common;
using System.Data.Entity;
using Model;
using MySql.Data.Entity;

namespace MessengerDal
{
    /// <summary>
    /// Создание StorageContext и описание структуры базы данных.
    /// </summary>
    [DbConfigurationType(typeof (MySqlEFConfiguration))]
    internal class StorageContext : DbContext
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="existingConnection">соединение с базой данных</param>
        /// <param name="contextOwnsConnection">подключение к серверу</param>
        public StorageContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// описание структуры базы данных
        /// </summary>
        /// <param name="modelBuilder">таблицы базы данных</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Profile>().ToTable("profiles");
            modelBuilder.Entity<Profile>().HasKey(pr => pr.ProfileId);
            modelBuilder.Entity<Profile>().Property(pr => pr.ProfileId).HasColumnName("ProfileId");
            modelBuilder.Entity<Profile>()
                .Property(pr => pr.ProfileName)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(40);
            modelBuilder.Entity<Profile>().Property(pr => pr.Online).HasColumnName("Online").IsRequired();

            modelBuilder.Entity<Contact>().ToTable("contacts");
            modelBuilder.Entity<Contact>().HasKey(ct => ct.ProfileId);
            modelBuilder.Entity<Contact>().Property(ct => ct.UserId).HasColumnName("UserId").IsRequired();
            modelBuilder.Entity<Contact>().Property(ct => ct.ContactId).HasColumnName("ContactId").IsRequired();

            modelBuilder.Entity<Message>().ToTable("messages");
            modelBuilder.Entity<Message>().HasKey(ms => ms.ProfileId);
            modelBuilder.Entity<Message>().Property(ms => ms.UserId).HasColumnName("UserId").IsRequired();
            modelBuilder.Entity<Message>().Property(ms => ms.SenderId).HasColumnName("SenderId").IsRequired();
            modelBuilder.Entity<Message>()
                .Property(ms => ms.Messages)
                .HasColumnName("Messages")
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}