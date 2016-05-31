using System.Data.Entity.ModelConfiguration;

namespace MessengerServer
{
    class DbContactMap : EntityTypeConfiguration<Contact>
    {

        public DbContactMap()
        {
            ToTable("Contact").HasKey(r => r.Id);
            Property(r => r.Name).IsRequired().HasColumnName("Name");
            Property(r => r.Friends).IsRequired().HasColumnName("Friends");
        }
    }
}
