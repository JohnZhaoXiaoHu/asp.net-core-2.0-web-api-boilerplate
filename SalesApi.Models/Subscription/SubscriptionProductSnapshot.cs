using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Subscription
{
    public class SubscriptionProductSnapshot : EntityBase
    {
        public int SubscriptionDayId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        
        public int EquivalentBox { get; set; }
        public decimal Price { get; set; }
        public decimal InternalPrice { get; set; }
        public decimal BoxPrice { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }
        
        public string LegacyProductId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Specification { get; set; }
        public ProductUnit ProductUnit { get; set; }
        public int ShelfLife { get; set; }
        public decimal EquivalentTon { get; set; }
        public string Barcode { get; set; }
        public decimal TaxRate { get; set; }

        public SubscriptionDay SubscriptionDay { get; set; }
        public ProductForSubscription ProductForSubscription { get; set; }
    }

    public class SubscriptionProductSnapshotConfiguration : EntityBaseConfiguration<SubscriptionProductSnapshot>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionProductSnapshot> b)
        {
            b.Property(x => x.Price).HasColumnType("decimal(10, 6)");
            b.Property(x => x.InternalPrice).HasColumnType("decimal(10, 6)");
            b.Property(x => x.BoxPrice).HasColumnType("decimal(10, 2)");

            b.Property(x => x.LegacyProductId).HasMaxLength(5);
            b.Property(x => x.Name).IsRequired().HasMaxLength(10);
            b.Property(x => x.FullName).IsRequired().HasMaxLength(50);
            b.Property(x => x.Specification).IsRequired().HasMaxLength(50);
            b.Property(x => x.EquivalentTon).HasColumnType("decimal(7, 6)");
            b.Property(x => x.Barcode).HasMaxLength(20);
            b.Property(x => x.TaxRate).HasColumnType("decimal(7, 6)");

            b.HasOne(x => x.SubscriptionDay).WithMany(x => x.SubscriptionProductSnapshots).HasForeignKey(x => x.SubscriptionDayId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForSubscription).WithMany(x => x.SubscriptionProductSnapshots)
                .HasForeignKey(x => x.ProductForSubscriptionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
