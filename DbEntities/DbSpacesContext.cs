namespace Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities
{
    using System.Data.Entity;

    public partial class DbSpacesContext : DbContext
    {
        public DbSpacesContext()
            : base("name=DbSpacesModel")
        {
        }

        public DbSpacesContext(string configConnectionString) 
            : base($"name={configConnectionString}")
        {
        }

        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<BackupFoldersDetail> BackupFoldersDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Case>()
                .Property(e => e.ClosingDate)
                .HasPrecision(0);

            modelBuilder.Entity<Case>()
                .Property(e => e.SyncDateTime)
                .HasPrecision(0);

            modelBuilder.Entity<BackupFoldersDetail>()
               .Property(e => e.SyncDateTime)
               .HasPrecision(0);
        }
    }
}
