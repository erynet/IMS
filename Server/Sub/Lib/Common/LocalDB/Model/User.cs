using System;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace IMS.Server.Sub.Lib.LocalDB.Model
{
    public class User
    {
        public int Idx { get; set; }
        public string Id { get; set; }
        public string Passwd { get; set; }
        public bool IsAdmin { get; set; }

        public User()
        {
            
        }

        public bool Authenticate(string id, string passwd)
        {
            if (Id != id)
                return false;
            var hash = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(passwd));
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            var hashedPasswd = sb.ToString();
            return Passwd == hashedPasswd;
        }

        public void CreateUser(string id, string passwd, bool isAdmin = false)
        {
            Id = id;
            var hash = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(passwd));
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            Passwd = sb.ToString();
            IsAdmin = isAdmin;
        }
    }

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.Id);
            Property(u => u.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.Id)
                .IsRequired()
                .HasColumnAnnotation("Idx_Id", new IndexAnnotation(new IndexAttribute()))
                .HasColumnOrder(1);
            Property(s => s.Passwd)
                .IsRequired()
                .HasColumnOrder(2);
            Property(s => s.IsAdmin)
                .IsRequired()
                .HasColumnOrder(3);
        }
    }
}