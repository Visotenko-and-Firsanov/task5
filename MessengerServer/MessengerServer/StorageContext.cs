using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MySql.Data.Entity;

namespace MessengerServer
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class StorageContext : DbContext
    {
        public StorageContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Profile> Contacts { get; set; }
        public DbSet<Profile> Messages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>();
            modelBuilder.Entity<Contacts>();
            modelBuilder.Entity<Message>();
        }
    }

    [Table("Profiles")]
    public class Profile
    {
        [Key]
        public int ProifileId { get; set; }

        [Required]
        public bool Online { get; set; }
        //[Required]
        //public ICollection<Contact> Contacts { get; set; }
        [Required]
        public string Name { get; set; }
    }

    [Table("Contacts")]
    public class Contacts
    {
        [Key, ForeignKey("Profile")]
        public int ProifileId { get; set; }
        [Required]
        public List<int> ContactsId { get; set; } 
    }

    [Table("Messages")]
    public class Message
    {
        [Key, ForeignKey("Profile")]
        public int ProifileId { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public List<string> MessagesList { get; set; }  
    }
}
