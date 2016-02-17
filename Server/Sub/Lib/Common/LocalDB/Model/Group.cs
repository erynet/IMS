using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Security.Policy;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class Group
    {
        public int Idx { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public bool Display { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public string UpsList { get; set; }
        public string CduList { get; set; }
        public int Status { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<UPS> Ups { get; set; }
        public virtual ICollection<CDU> Cdu { get; set; }

        public Group()
        {
            Display = true;
            CoordX = 0;
            CoordY = 0;
            Status = 0;
            Enabled = true;

            Ups = new List<UPS>();
            Cdu = new List<CDU>();
        }
    }

    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            ToTable("Group");
            HasKey(g => g.Idx);
            Property(g => g.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(g => g.No)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                        new IndexAnnotation(
                            new IndexAttribute("Idx_unique_GroupNo", 1) { IsUnique = true }))
                .HasColumnOrder(1);
            Property(g => g.Name)
                .IsOptional()
                .HasMaxLength(128)
                .IsUnicode(true)
                .HasColumnType("varchar")
                .HasColumnOrder(2);
            Property(g => g.Display)
                .IsRequired()
                .HasColumnOrder(3);
            Property(g => g.CoordX)
                .IsRequired()
                .HasColumnOrder(4);
            Property(g => g.CoordY)
                .IsRequired()
                .HasColumnOrder(5);
            Property(g => g.UpsList)
                .IsOptional()
                .HasMaxLength(512)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(6);
            Property(g => g.CduList)
                .IsOptional()
                .HasMaxLength(512)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(7);
            Property(g => g.Status)
                .IsRequired()
                .HasColumnOrder(8);
            Property(g => g.Enabled)
                .IsRequired()
                .HasColumnOrder(9);
            Property(g => g.Description)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode(true)
                .HasColumnType("text")
                .HasColumnOrder(10);
        }
    }

}