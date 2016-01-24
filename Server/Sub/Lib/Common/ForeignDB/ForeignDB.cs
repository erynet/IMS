using System.Data.Entity;

//using IMS.Server.Sub.Lib.ExternalDB.Model;

namespace IMS.Server.Sub.Lib.ForeignDB
{
    public class ForeignDB : DbContext
    {
        public ForeignDB() : base("ForeignDB")
        {
            //Database.SetInitializer<LabelInspectorDb>(new CreateDatabaseIfNotExists<LabelInspectorDb>());
            //Database.SetInitializer<LabelInspectorDb>(new DropCreateDatabaseAlways<LabelInspectorDb>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ForeignDB>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Configurations.Add(new LaunchEntryConfiguration());
            //modelBuilder.Configurations.Add(new SessionEntityConfiguration());
            //modelBuilder.Configurations.Add(new ImageConfiguration());
            //modelBuilder.Configurations.Add(new ImageSliceConfiguration());
            //modelBuilder.Configurations.Add(new ReferenceConfiguration());
            //modelBuilder.Configurations.Add(new MessageConfiguration());
            //modelBuilder.Configurations.Add(new DefectConfiguration());
            //modelBuilder.Configurations.Add(new LogConfiguration());
            //modelBuilder.Configurations.Add(new ClientInfoDictionaryConfiguration());

        }

        //public DbSet<LaunchEntry> LaunchEntry { get; set; }
        //public DbSet<Session> Session { get; set; }
        //public DbSet<MediaTypeNames.Image> Image { get; set; }
        //public DbSet<ImageSlice> ImageSlice { get; set; }
        //public DbSet<Reference> Reference { get; set; }
        //public DbSet<Message> Message { get; set; }
        //public DbSet<Defect> Defect { get; set; }
        //public DbSet<Log> Log { get; set; }
        //public DbSet<ClientInfoDictionary> ClientInfoDictionary { get; set; }
    }
}