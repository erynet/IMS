using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using IMS.Database.LocalDB.Model;

namespace IMS.Database.LocalDB.Model
{
    public class CDU
    {
        public int Idx { get; set; }
        public int GroupIdx { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        //public string UpsList { get; set; }
        public bool Extendable { get; set; }
        public int ContractCount { get; set; }
        public string IpAddress { get; set; }
        public int Status { get; set; }
        public bool Enabled { get; set; }
        public string InstallAt { get; set; }
        public string Description { get; set; }

        //public virtual Group Group { get; set; }
        //public virtual ICollection<WarningLog> WarningLogs { get; set; }
        //public virtual ICollection<CduEvent> CduEvents { get; set; }
        //public virtual ICollection<CduSocket> CduSockets { get; set; }

        public CDU()
        {
            Extendable = false;
            ContractCount = 0;
            Status = 0; // normal state
            Enabled = true;

            //WarningLogs = new List<WarningLog>();
            //CduEvents = new List<CduEvent>();
            //CduSockets = new List<CduSocket>();
        }
    }

    public class CduConfiguration : EntityTypeConfiguration<CDU>
    {
        public CduConfiguration()
        {
            ToTable("Cdu");
            HasKey(c => c.Idx);
            Property(c => c.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.GroupIdx)
                .IsOptional()
                .HasColumnOrder(1);
            Property(c => c.No)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                        new IndexAnnotation(
                            new IndexAttribute("Idx_unique_CduNo", 1) { IsUnique = true }))
                .IsConcurrencyToken()
                .HasColumnOrder(2);
            Property(c => c.Name)
                .IsOptional()
                .HasMaxLength(128)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(3);
            //Property(c => c.UpsList)
            //    .IsOptional()
            //    .HasMaxLength(512)
            //    .IsUnicode()
            //    .HasColumnType("varchar")
            //    .HasColumnOrder(4);
            Property(c => c.Extendable)
                .IsRequired()
                .HasColumnOrder(4);
            Property(c => c.ContractCount)
                .IsRequired()
                .HasColumnOrder(5);
            Property(c => c.IpAddress)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(6);
            Property(c => c.Status)
                .IsRequired()
                .HasColumnOrder(7);
            Property(c => c.Enabled)
                .IsRequired()
                .HasColumnOrder(8);
            Property(c => c.InstallAt)
                .IsOptional()
                .HasMaxLength(128)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(9);
            Property(c => c.Description)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode()
                .HasColumnType("text")
                .HasColumnOrder(10);

            //HasOptional(c => c.Group)
            //    .WithMany(g => g.Cdu)
            //    .HasForeignKey(c => c.GroupIdx)
            //    .WillCascadeOnDelete(true);
        }
    }
}