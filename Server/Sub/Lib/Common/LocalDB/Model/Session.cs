using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class Session
    {
        public int Idx { get; set; }
        public string Guid { get; set; }
        public string MacAddress { get; set; }
        public int EventIdx { get; set; }
        public int WarningIdx { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsClosed { get; set; }
        public DateTime SignIn { get; set; }
        public DateTime SignOut { get; set; }

        public Session()
        {

        }

        public string Enter(string macAddress, bool isAdmin)
        {
            Guid = System.Guid.NewGuid().ToString();
            MacAddress = macAddress;
            EventIdx = 0;
            WarningIdx = 0;
            IsAdmin = isAdmin;
            IsClosed = false;
            SignIn = DateTime.Now;
            return Guid;
        }

        public void Exit()
        {
            if (!IsClosed)
            {
                IsClosed = true;
                SignOut = DateTime.Now;
            }
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
            Property(e => e.Guid)
                .IsRequired()
                .HasColumnAnnotation("Idx_Guid", new IndexAnnotation(new IndexAttribute()))
                .HasColumnOrder(1);
            Property(e => e.MacAddress)
                .IsRequired()
                .HasColumnOrder(2);
            Property(e => e.EventIdx)
                .IsRequired()
                .HasColumnOrder(3);
            Property(e => e.WarningIdx)
                .IsRequired()
                .HasColumnOrder(4);
            Property(e => e.IsAdmin)
                .IsRequired()
                .HasColumnOrder(5);
            Property(e => e.IsClosed)
                .IsRequired()
                .IsConcurrencyToken()
                .HasColumnOrder(6);
            Property(e => e.SignIn)
                .IsRequired()
                .HasColumnOrder(7);
            Property(e => e.SignOut)
                .IsOptional()
                .HasColumnOrder(7);
        }
    }
}