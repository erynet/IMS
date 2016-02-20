using System.Data.Entity;
using IMS.Database.LocalDB.Model;

namespace IMS.Database.LocalDB
{
    public class LocalDB : DbContext
    {
        public LocalDB() : base("LocalDB")
        {
            //Database.SetInitializer<LabelInspectorDb>(new CreateDatabaseIfNotExists<LabelInspectorDb>());
            //Database.SetInitializer<LabelInspectorDb>(new DropCreateDatabaseAlways<LabelInspectorDb>());
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LocalDB>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new SettingConfiguration());
            //modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new EventLogConfiguration());
            modelBuilder.Configurations.Add(new WarningLogConfiguration());
            modelBuilder.Configurations.Add(new GeneralLogConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new UpsConfiguration());
            modelBuilder.Configurations.Add(new CduConfiguration());
            modelBuilder.Configurations.Add(new UpsEventConfiguration());
            modelBuilder.Configurations.Add(new CduEventConfiguration());
            modelBuilder.Configurations.Add(new CduSocketConfiguration());
        }

        public DbSet<Setting> Setting { get; set; }
        //public DbSet<Session> Session { get; set; }
        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<WarningLog> WarningLog { get; set; }
        public DbSet<GeneralLog> GeneralLog { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<UPS> Ups { get; set; }
        public DbSet<CDU> Cdu { get; set; }
        public DbSet<UpsEvent> UpsEvent { get; set; }
        public DbSet<CduEvent> CduEvent { get; set; }
        public DbSet<CduSocket> CduSocket { get; set; }
    }
}