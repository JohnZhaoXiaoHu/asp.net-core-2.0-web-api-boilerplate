using Infrastructure.Features.Data;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Collective;
using SalesApi.Models.County;
using SalesApi.Models.Mall;
using SalesApi.Models.Overall;
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

            #region Overall

            modelBuilder.ApplyConfiguration(new SalesDayConfiguration());

            #endregion

            #region Retail

            modelBuilder.ApplyConfiguration(new ProductForRetailConfiguration());
            modelBuilder.ApplyConfiguration(new RetailerConfiguration());
            modelBuilder.ApplyConfiguration(new RetailPromotionSeriesConfiguration());
            modelBuilder.ApplyConfiguration(new RetailPromotionSeriesBonusConfiguration());
            modelBuilder.ApplyConfiguration(new RetailPromotionEventConfiguration());
            modelBuilder.ApplyConfiguration(new RetailPromotionEventBonusConfiguration());
            modelBuilder.ApplyConfiguration(new RetailDayConfiguration());
            modelBuilder.ApplyConfiguration(new RetailProductSnapshotConfiguration());
            modelBuilder.ApplyConfiguration(new RetailOrderConfiguration());
            modelBuilder.ApplyConfiguration(new RetailPromotionGiftOrderConfiguration());

            #endregion

            #region Collective

            modelBuilder.ApplyConfiguration(new CollectiveCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductForCollectiveConfiguration());
            modelBuilder.ApplyConfiguration(new CollectiveDayConfiguration());
            modelBuilder.ApplyConfiguration(new CollectiveProductSnapshotConfiguration());
            modelBuilder.ApplyConfiguration(new CollectiveOrderConfiguration());
            modelBuilder.ApplyConfiguration(new CollectivePriceConfiguration());

            #endregion

            #region County

            modelBuilder.ApplyConfiguration(new ProductForCountyConfiguration());
            modelBuilder.ApplyConfiguration(new CountyAgentConfiguration());
            modelBuilder.ApplyConfiguration(new CountyAgentPriceConfiguration());
            modelBuilder.ApplyConfiguration(new CountyDayConfiguration());
            modelBuilder.ApplyConfiguration(new CountyProductSnapshotConfiguration());
            modelBuilder.ApplyConfiguration(new CountyPromotionSeriesConfiguration());
            modelBuilder.ApplyConfiguration(new CountyPromotionSeriesBonusConfiguration());
            modelBuilder.ApplyConfiguration(new CountyPromotionEventConfiguration());
            modelBuilder.ApplyConfiguration(new CountyPromotionEventBonusConfiguration());
            modelBuilder.ApplyConfiguration(new CountyOrderConfiguration());
            modelBuilder.ApplyConfiguration(new CountyPromotionGiftOrderConfiguration());

            #endregion

            #region Mall

            modelBuilder.ApplyConfiguration(new ProductForMallConfiguration());
            modelBuilder.ApplyConfiguration(new MallGroupConfiguration());
            modelBuilder.ApplyConfiguration(new MallCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new MallPriceConfiguration());
            modelBuilder.ApplyConfiguration(new MallDayConfiguration());
            modelBuilder.ApplyConfiguration(new MallProductSnapshotConfiguration());
            modelBuilder.ApplyConfiguration(new MallOrderConfiguration());

            #endregion
        }

        #region Settings

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<DistributionGroup> DistributionGroups { get; set; }
        public DbSet<DeliveryVehicle> DeliveryVehicles { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion

        #region Overall

        public DbSet<SalesDay> SalesDays { get; set; }

        #endregion

        #region Retail

        public DbSet<ProductForRetail> ProductForRetails { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<RetailPromotionSeries> RetailPromotionSeries { get; set; }
        public DbSet<RetailPromotionSeriesBonus> RetailPromotionSeriesBonuses { get; set; }
        public DbSet<RetailPromotionEvent> RetailPromotionEvents { get; set; }
        public DbSet<RetailPromotionEventBonus> RetailPromotionEventBonuses { get; set; }
        public DbSet<RetailDay> RetailDays { get; set; }
        public DbSet<RetailProductSnapshot> RetailProductSnapshots { get; set; }
        public DbSet<RetailOrder> RetailOrders { get; set; }

        #endregion

        #region Collective

        public DbSet<CollectiveCustomer> CollectiveCustomers { get; set; }
        public DbSet<ProductForCollective> ProductForCollectives { get; set; }
        public DbSet<CollectiveProductSnapshot> CollectiveProductSnapshots { get; set; }
        public DbSet<CollectiveDay> CollectiveDays { get; set; }
        public DbSet<CollectiveOrder> CollectiveOrders { get; set; }
        public DbSet<CollectivePrice> CollectivePrices { get; set; }

        #endregion

        #region County

        public DbSet<ProductForCounty> ProductForCounties { get; set; }
        public DbSet<CountyAgent> CountyAgents { get; set; }
        public DbSet<CountyAgentPrice> CountyAgentPrices { get; set; }
        public DbSet<CountyDay> CountyDays { get; set; }
        public DbSet<CountyProductSnapshot> CountyProductSnapshots { get; set; }
        public DbSet<CountyPromotionSeries> CountyPromotionSeries { get; set; }
        public DbSet<CountyPromotionSeriesBonus> CountyPromotionSeriesBonuses { get; set; }
        public DbSet<CountyPromotionEvent> CountyPromotionEvents { get; set; }
        public DbSet<CountyPromotionEventBonus> CountyPromotionEventBonuses { get; set; }
        public DbSet<CountyOrder> CountyOrders { get; set; }
        public DbSet<CountyPromotionGiftOrder> CountyPromotionGiftOrders { get; set; }
        public DbSet<CountyPromotionGiftOrder> PromotionGiftOrders { get; set; }

        #endregion

        #region Mall

        public DbSet<ProductForMall> ProductForMalls { get; set; }
        public DbSet<MallGroup> MallGroups { get; set; }
        public DbSet<MallCustomer> MallCustomers { get; set; }
        public DbSet<MallPrice> MallPrices { get; set; }
        public DbSet<MallDay> MallDays { get; set; }
        public DbSet<MallProductSnapshot> MallProductSnapshots { get; set; }
        public DbSet<MallOrder> MallOrders { get; set; }

        #endregion
    }
}
