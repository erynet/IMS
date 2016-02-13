using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class EventLog
    {
        public int Idx { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public string Data { get; set; }
        public DateTime TimeStamp { get; set; }

        public EventLog(string description, int code)
        {
            this.Description = description;
            this.Code = code;
            this.TimeStamp = DateTime.Now;
        }
    }

    public class EventLogConfiguration : EntityTypeConfiguration<EventLog>
    {
        public EventLogConfiguration()
        {
            ToTable("EventLog");
            HasKey(e => e.Idx);
            Property(e => e.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Description)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode(true)
                .HasColumnOrder(1);
            Property(e => e.Code)
                .IsRequired()
                .HasColumnOrder(2);
            Property(e => e.Data)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode(true)
                .HasColumnOrder(3);
            Property(e => e.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasColumnOrder(4);
        }
    }
}