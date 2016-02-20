using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Database.LocalDB.Model
{
    public class UPS
    {
        public int Idx { get; set; }
        public int GroupNo { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public string MateList { get; set; }
        public int CduNo { get; set; }
        public string Specification { get; set; }
        public string Capacity { get; set; }
        public string IpAddress { get; set; }
        public int Status { get; set; }
        public bool Enabled { get; set; }
        public string InstallAt { get; set; }
        public string Description { get; set; }

        //public virtual Group Group { get; set; }
        //public virtual ICollection<WarningLog> WarningLogs { get; set; }
        //public virtual ICollection<UpsEvent> UpsEvents { get; set; }

        public UPS()
        {
            Status = 0; // normal state
            Enabled = true;

            //WarningLogs = new List<WarningLog>();
            //UpsEvents = new List<UpsEvent>();
        }
    }

    public class UpsConfiguration : EntityTypeConfiguration<UPS>
    {
        public UpsConfiguration()
        {
            ToTable("Ups");
            HasKey(u => u.Idx);
            Property(u => u.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.GroupNo)
                .IsOptional()
                .HasColumnOrder(1);
            Property(u => u.No)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                        new IndexAnnotation(
                            new IndexAttribute("Idx_unique_UpsNo", 1) { IsUnique = true }))
                .HasColumnOrder(2);
            Property(u => u.Name)
                .IsOptional()
                .HasMaxLength(128)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(3);
            Property(u => u.MateList)
                .IsOptional()
                .HasMaxLength(512)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(4);
            Property(u => u.CduNo)
                .IsOptional()
                .HasColumnOrder(5);
            Property(u => u.Specification)
                .IsOptional()
                .HasMaxLength(256)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(6);
            Property(u => u.Capacity)
                .IsOptional()
                .HasMaxLength(128)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(7);
            Property(u => u.IpAddress)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(8);
            Property(u => u.Status)
                .IsRequired()
                .HasColumnOrder(9);
            Property(u => u.Enabled)
                .IsRequired()
                .HasColumnOrder(10);
            Property(u => u.InstallAt)
                .IsOptional()
                .HasMaxLength(128)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(11);
            Property(u => u.Description)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode()
                .HasColumnType("text")
                .HasColumnOrder(12);

            //HasOptional(u => u.Group)
            //    .WithMany(g => g.Ups)
            //    .HasForeignKey(u => u.GroupNo)
            //    .WillCascadeOnDelete(true);


        }
    }
    
}