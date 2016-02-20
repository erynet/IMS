using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Database.LocalDB.Model
{
    public class CduSocket
    {
        public int Idx { get; set; }
        public int CduIdx { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        //public virtual CDU CDU { get; set; }

        public CduSocket()
        {
            Enabled = false;
        }
    }

    public class CduSocketConfiguration : EntityTypeConfiguration<CduSocket>
    {
        public CduSocketConfiguration()
        {
            ToTable("CduSocket");
            HasKey(c => c.Idx);
            Property(c => c.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.CduIdx)
                .IsRequired()
                .HasColumnOrder(1);
            Property(c => c.No)
                .IsRequired()
                .HasColumnOrder(2);
            Property(c => c.Name)
                .IsOptional()
                .HasMaxLength(256)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(3);
            Property(c => c.Enabled)
                .IsRequired()
                .HasColumnOrder(4);

            //HasRequired(c => c.CDU)
            //    .WithMany(c => c.CduSockets)
            //    .HasForeignKey(c => c.CduIdx)
            //    .WillCascadeOnDelete(true);
        }
    }
}