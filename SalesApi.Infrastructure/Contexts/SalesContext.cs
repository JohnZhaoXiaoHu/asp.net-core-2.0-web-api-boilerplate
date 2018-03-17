using Microsoft.EntityFrameworkCore;
using SalesApi.Infrastructure.Abstractions.Data;
using SalesApi.Infrastructure.DomainModels;

namespace SalesApi.Infrastructure.Contexts
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
