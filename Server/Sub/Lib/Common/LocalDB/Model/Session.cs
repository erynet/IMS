using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class Session
    {
        public int Idx { get; set; }
        public string Token { get; set; }
        public string MacAddress { get; set; }
        public int UserIdx { get; set; }
        public int EventIdx { get; set; }
        public int WarningIdx { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsClosed { get; set; }
        public DateTime SignIn { get; set; }
        public DateTime SignOut { get; set; }

        public Session()
        {

        }

        public string Enter(string token, string macAddress, int userIdx, bool isAdmin)
        {
            Token = token;
            MacAddress = macAddress;
            UserIdx = userIdx;
            EventIdx = 0;
            WarningIdx = 0;
            IsAdmin = isAdmin;
            IsClosed = false;
            SignIn = DateTime.Now;
            return Token;
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
            Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(512)
                .IsUnicode(true)
                .HasColumnAnnotation("Idx_Token", new IndexAnnotation(new IndexAttribute()))
                .HasColumnOrder(1);
            Property(e => e.MacAddress)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(true)
                .HasColumnOrder(2);
            Property(e => e.UserIdx)
                .IsRequired()
                .HasColumnOrder(3);
            Property(e => e.EventIdx)
                .IsRequired()
                .HasColumnOrder(4);
            Property(e => e.WarningIdx)
                .IsRequired()
                .HasColumnOrder(5);
            Property(e => e.IsAdmin)
                .IsRequired()
                .HasColumnOrder(6);
            Property(e => e.IsClosed)
                .IsRequired()
                .IsConcurrencyToken()
                .HasColumnOrder(7);
            Property(e => e.SignIn)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasColumnOrder(8);
            Property(e => e.SignOut)
                .IsOptional()
                .HasColumnType("datetime2")
                .HasColumnOrder(9);
        }
    }
}