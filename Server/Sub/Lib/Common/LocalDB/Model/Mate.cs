using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class Mate
    {
        public int Idx { get; set; }
        public int GroupIdx { get; set; }
        public string Rule { get; set; }

        public virtual Group Group { get; set; }

        public Mate()
        {
            
        }
        
    }

    public class MateConfiguration : EntityTypeConfiguration<Mate>
    {
        public MateConfiguration()
        {
            ToTable("Mate");
            HasKey(m => m.Idx);
            Property(m => m.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.GroupIdx)
                .IsRequired()
                .HasColumnOrder(1);
            Property(m => m.Rule)
                .IsRequired()
                .HasMaxLength(512)
                .HasColumnType("varchar")
                .HasColumnOrder(2);

            HasRequired(m => m.Group)
                .WithMany(g => g.Mates)
                .HasForeignKey(m => m.GroupIdx)
                .WillCascadeOnDelete(true);
        }
    }
}