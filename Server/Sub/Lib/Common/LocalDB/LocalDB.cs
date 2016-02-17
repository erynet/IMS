﻿using System.Data.Entity;
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
            //modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            //modelBuilder.Configurations.Add(new DevicesConfiguration());
            modelBuilder.Configurations.Add(new UpsConfiguration());
            modelBuilder.Configurations.Add(new CduConfiguration());
            modelBuilder.Configurations.Add(new UpsEventConfiguration());
            modelBuilder.Configurations.Add(new CduEventConfiguration());
            modelBuilder.Configurations.Add(new CduSocketConfiguration());
        }

        public DbSet<Setting> Setting { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<GeneralLog> GeneralLog { get; set; }
        //public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        //public DbSet<Devices> Devices { get; set; }
        public DbSet<UPS> Ups { get; set; }
        public DbSet<CDU> Cdu { get; set; }
        public DbSet<UpsEvent> UpsEvent { get; set; }
        public DbSet<CduEvent> CduEvent { get; set; }
        public DbSet<CduSocket> CduContract { get; set; }
    }
}