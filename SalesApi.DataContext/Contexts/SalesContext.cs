using Infrastructure.Features.Data;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;

namespace SalesApi.DataContext.Contexts
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

            #region Settings

            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new DistributionGroupConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryVehicleConfiguration());
            modelBuilder.ApplyConfiguration(new SubAreaConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            #endregion

            #region Retail

            modelBuilder.ApplyConfiguration(new ProductForRetailConfiguration());

            #endregion
        }

        #region Settings

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<DistributionGroup> DistributionGroups { get; set; }
        public DbSet<DeliveryVehicle> DeliveryVehicles { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion

        #region Retail

        public DbSet<ProductForRetail> ProductForRetails { get; set; }

        #endregion
    }
}
