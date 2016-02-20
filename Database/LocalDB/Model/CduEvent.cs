using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Database.LocalDB.Model
{
    public class CduEvent
    {
        public int Idx { get; set; }
        public int CduIdx { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Priority { get; set; }
        public DateTime TimeStamp { get; set; }

        //public CDU CDU { get; set; }

        public CduEvent()
        {
            Priority = 1;   // 0: Debug, 1: Info, 2: Waring, 3: Critical
            TimeStamp = DateTime.Now;
        }
    }

    public class CduEventConfiguration : EntityTypeConfiguration<CduEvent>
    {
        public CduEventConfiguration()
        {
            ToTable("CduEvent");
            HasKey(c => c.Idx);
            Property(c => c.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.CduIdx)
                .IsOptional()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("Idx_cdu_event_timestamp_cduidx", 2)))
                .HasColumnOrder(1);
            Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasColumnOrder(2);
            Property(c => c.Body)
                .IsOptional()
                .HasMaxLength(4096)
                .IsUnicode()
                .HasColumnType("text")
                .HasColumnOrder(3);
            Property(c => c.Priority)
                .IsRequired()
                .HasColumnOrder(4);
            // http://gavindraper.com/2014/06/26/entity-framework-fluent-api-and-indexing/
            Property(c => c.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("Idx_cdu_event_timestamp_cduidx", 1)))
                .HasColumnOrder(5);
            //HasOptional(c => c.CDU)
            //    .WithMany(c => c.CduEvents)
            //    .HasForeignKey(c => c.CduIdx)
            //    .WillCascadeOnDelete(false);
        }
    }
}