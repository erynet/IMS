using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class Group
    {
        public int Idx { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Devices> Devices { get; set; }

        public Group()
        {
            Devices = new List<Devices>();
            CreateAt = DateTime.Now;
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
            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(true)
                .HasColumnType("varchar")
                .HasColumnOrder(1);
            Property(g => g.Description)
                .IsRequired()
                .HasMaxLength(4096)
                .IsUnicode(true)
                .HasColumnType("text")
                .HasColumnOrder(2);
            Property(g => g.CreateAt)
                .IsOptional()
                .HasColumnType("datetime2")
                .HasColumnOrder(5);


        }
    }

}