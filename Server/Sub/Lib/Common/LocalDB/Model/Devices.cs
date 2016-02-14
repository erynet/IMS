using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class Devices
    {
        public int Idx { get; set; }
        public int GroupIdx { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeCode { get; set; }
        public string Type { get; set; }
        public string IpAddress { get; set; }

        public virtual Group Group { get; set; }

        public Devices()
        {
            
        }
    }

    public class DevicesConfiguration : EntityTypeConfiguration<Devices>
    {
        public DevicesConfiguration()
        {
            ToTable("Devices");
            HasKey(d => d.Idx);
            Property(d => d.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(d => d.GroupIdx)
                .IsRequired()
                .HasColumnOrder(1);
            Property(d => d.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(256)
                .HasColumnType("varchar")
                .HasColumnOrder(2);
            Property(d => d.Description)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(4096)
                .HasColumnType("varchar")
                .HasColumnOrder(3);
            Property(d => d.TypeCode)
                .IsRequired()
                .HasColumnOrder(4);
            Property(d => d.Type)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(256)
                .HasColumnType("varchar")
                .HasColumnOrder(5);
            Property(d => d.IpAddress)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnType("varchar")
                .HasColumnOrder(6);

            HasRequired(d => d.Group)
                .WithMany(g => g.Devices)
                .HasForeignKey(d => d.GroupIdx)
                .WillCascadeOnDelete(true);
        }
    }
}