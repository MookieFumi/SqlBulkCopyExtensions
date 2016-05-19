using System.Data.Entity;
using SqlBulkCopyExtensions.Test.Model.Configurations;

namespace SqlBulkCopyExtensions.Test.Model
{
    public class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<AccessLog> AccessLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccessLogConfiguration());
        }
    }
}
