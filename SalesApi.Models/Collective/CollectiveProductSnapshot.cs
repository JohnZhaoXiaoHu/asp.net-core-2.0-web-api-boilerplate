using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Collective
{
    public class CollectiveProductSnapshot : EntityBase
    {
        public CollectiveProductSnapshot()
        {
            SalesType = SalesType.集体户;
        }

        public int CollectiveDayId { get; set; }
        public int ProductForCollectiveId { get; set; }
        
        public SalesType SalesType { get; set; }
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

        public CollectiveDay CollectiveDay { get; set; }
        public ProductForCollective ProductForCollective { get; set; }
    }

    public class CollectiveProductSnapshotConfiguration : EntityBaseConfiguration<CollectiveProductSnapshot>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CollectiveProductSnapshot> b)
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

            b.HasOne(x => x.CollectiveDay).WithMany(x => x.CollectiveProductSnapshots).HasForeignKey(x => x.CollectiveDayId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForCollective).WithMany(x => x.CollectiveProductSnapshots)
                .HasForeignKey(x => x.ProductForCollectiveId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
