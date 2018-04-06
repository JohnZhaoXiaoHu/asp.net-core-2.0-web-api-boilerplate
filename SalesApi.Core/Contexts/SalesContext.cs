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
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
