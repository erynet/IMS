using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class Session
    {
        public int Idx { get; set; }
        public string MacAddress { get; set; }
        public int EventIdx { get; set; }
        public int WarningIdx { get; set; }
        
        public Session()
        {

        }

        public string Enter(string macAddress)
        {
            MacAddress = macAddress;
            EventIdx = 0;
            WarningIdx = 0;
            return macAddress;
        }

        public void Exit()
        {
            
        }
    }

    public class SessionConfiguration : EntityTypeConfiguration<Session>
    {
        public SessionConfiguration()
        {
            ToTable("Session");
            HasKey(e => e.Idx);
            Property(e => e.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.MacAddress)
                .IsRequired()
                .HasMaxLength(512)
                .HasColumnAnnotation("Idx_MacAddress", new IndexAnnotation(new IndexAttribute()))
                .HasColumnOrder(1);
            Property(e => e.EventIdx)
                .IsRequired()
                .HasColumnOrder(2);
            Property(e => e.WarningIdx)
                .IsRequired()
                .HasColumnOrder(3);
        }
    }
}