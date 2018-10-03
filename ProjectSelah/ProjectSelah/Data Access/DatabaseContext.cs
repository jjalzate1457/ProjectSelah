using ProjectSelah.Models;
using SQLite.CodeFirst;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace ProjectSelah.Data_Access
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = "data\\selah.db", ForeignKeys = true }.ConnectionString
            }, true)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder, true);
            Database.SetInitializer(sqliteConnectionInitializer);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Lyrics> Lyrics { get; set; }
    }
}
