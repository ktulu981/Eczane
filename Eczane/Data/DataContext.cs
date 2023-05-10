using Eczane.Core.Entities;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

namespace Eczane.Data
{
    public class DataContext: DbContext
    {

        public DataContext():base(
            new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = "./rxsample.db", ForeignKeys = true }.ConnectionString
            }, true)
        {
            Database.SetInitializer<DataContext>(null);
        }
        public DbSet<ETKIN_MADDELER> ETKIN_MADDELER { get; set; }
        public DbSet<ILAC_AMBALAJ> ILAC_AMBALAJ { get; set; }
        public DbSet<ILAC_ETKIN_MADDELER> ILAC_ETKIN_MADDELER { get; set; }
        public DbSet<ILAC_FORM> ILAC_FORM { get; set; }
        public DbSet<ILACLAR> ILACLAR { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }


    public class SQLiteConfiguration : DbConfiguration
    {
        public SQLiteConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}
