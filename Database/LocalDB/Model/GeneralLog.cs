using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Database.LocalDB.Model
{
    public class GeneralLog
    {
        public int Idx { get; set; }
        public string Contents { get; set; }
        public int Type { get; set; }
        public DateTime TimeStamp { get; set; }

        public GeneralLog(string contents, int type = 0)
        {
            this.Contents = contents;
            this.Type = type;
            this.TimeStamp = DateTime.Now;
        }
    }

    public class GeneralLogConfiguration : EntityTypeConfiguration<GeneralLog>
    {
        public GeneralLogConfiguration()
        {
            ToTable("GeneralLog");
            HasKey(g => g.Idx);
            Property(g => g.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(g => g.Contents)
                .IsRequired()
                .HasMaxLength(4096)
                .IsUnicode(true)
                .HasColumnOrder(1);
            Property(g => g.Type)
                .IsRequired()
                .HasColumnOrder(2);
            Property(g => g.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasColumnOrder(3);
        }
    }
}