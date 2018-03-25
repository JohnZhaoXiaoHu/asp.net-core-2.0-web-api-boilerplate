using Microsoft.EntityFrameworkCore;
using SalesApi.Core.Abstractions.Data;
using SalesApi.Core.DomainModels;

namespace SalesApi.Core.Contexts
{
    public class SalesContext : DbContextBase
    {
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public DbSet<Product> Products { get; set; }
    }
}
