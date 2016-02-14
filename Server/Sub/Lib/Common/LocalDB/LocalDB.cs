using System.Data.Entity;
using IMS.Server.Sub.Lib.LocalDB.Model;

namespace IMS.Server.Sub.Lib.LocalDB
{
    public class LocalDB : DbContext
    {
        public LocalDB() : base("LocalDB")
        {
            //Database.SetInitializer<LabelInspectorDb>(new CreateDatabaseIfNotExists<LabelInspectorDb>());
            //Database.SetInitializer<LabelInspectorDb>(new DropCreateDatabaseAlways<LabelInspectorDb>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LocalDB>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new SettingConfiguration());
            modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new EventLogConfiguration());
            modelBuilder.Configurations.Add(new GeneralLogConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new DevicesConfiguration());
        }

        public DbSet<Setting> Setting { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<GeneralLog> GeneralLog { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Devices> Devices { get; set; }
    }
}