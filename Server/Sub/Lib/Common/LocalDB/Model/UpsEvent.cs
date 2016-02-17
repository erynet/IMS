using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class UpsEvent
    {
        public int Idx { get; set; }
        public int UpsIdx { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Priority { get; set; }
        public DateTime TimeStamp { get; set; }

        public UPS UPS { get; set; }

        public UpsEvent()
        {
            Priority = 1;   // 0: Debug, 1: Info, 2: Waring, 3: Critical
            TimeStamp = DateTime.Now;
        }
    }

    public class UpsEventConfiguration : EntityTypeConfiguration<UpsEvent>
    {
        public UpsEventConfiguration()
        {
            ToTable("UpsEvent");
            HasKey(u => u.Idx);
            Property(u => u.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.UpsIdx)
                .IsOptional()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("Idx_ups_event_timestamp_upsidx", 2)))
                .HasColumnOrder(1);
            Property(u => u.Title)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(2);
            Property(u => u.Body)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode()
                .HasColumnType("text")
                .HasColumnOrder(3);
            Property(u => u.Priority)
                .IsRequired()
                .HasColumnOrder(4);
            // http://gavindraper.com/2014/06/26/entity-framework-fluent-api-and-indexing/
            Property(u => u.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, 
                    new IndexAnnotation(
                        new IndexAttribute("Idx_ups_event_timestamp_upsidx", 1)))
                .HasColumnOrder(5);
            HasOptional(u => u.UPS)
                .WithMany(u => u.UpsEvents)
                .HasForeignKey(u => u.UpsIdx)
                .WillCascadeOnDelete(false);
        }
    }
}