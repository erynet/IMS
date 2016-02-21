using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Database.LocalDB.Model
{
    public class WarningLog
    {
        public int Idx { get; set; }
        public int Code { get; set; }
        public string Data { get; set; }
        public int DeviceNo { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }

        //public virtual UPS UPS { get; set; }
        //public virtual CDU CDU { get; set; }
    
        public WarningLog(string description, int code)
        {
            this.Description = description;
            this.Code = code;
            this.Priority = 0;  // info
            this.TimeStamp = DateTime.Now;
        }
    }

    public class WarningLogConfiguration : EntityTypeConfiguration<WarningLog>
    {
        public WarningLogConfiguration()
        {
            ToTable("WarningLog");
            HasKey(w => w.Idx);
            Property(w => w.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(w => w.Code)
                .IsRequired()
                .HasColumnOrder(1);
            Property(w => w.Data)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode(true)
                .HasColumnOrder(2);
            Property(w => w.DeviceNo)
                .IsOptional()
                .HasColumnOrder(3);
            Property(w => w.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasColumnOrder(4);
            Property(w => w.Description)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode(true)
                .HasColumnOrder(5);

            //HasOptional(w => w.UPS)
            //    .WithMany(u => u.WarningLogs)
            //    .HasForeignKey(w => w.UpsNo)
            //    .WillCascadeOnDelete(false);
            //HasOptional(w => w.CDU)
            //    .WithMany(u => u.WarningLogs)
            //    .HasForeignKey(w => w.CduNo)
            //    .WillCascadeOnDelete(false);
        }
    }
}