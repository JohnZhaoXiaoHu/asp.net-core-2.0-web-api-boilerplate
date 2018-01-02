using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.County
{
    public class CountyProductSnapshot : EntityBase
    {
        public int CountyDayId { get; set; }
        public int ProductForCountyId { get; set; }
        
        public int EquivalentBox { get; set; }
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

        public CountyDay CountyDay { get; set; }
        public ProductForCounty ProductForCounty { get; set; }
    }

    public class CountyProductSnapshotConfiguration : EntityBaseConfiguration<CountyProductSnapshot>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyProductSnapshot> b)
        {
            b.Property(x => x.LegacyProductId).HasMaxLength(5);
            b.Property(x => x.Name).IsRequired().HasMaxLength(10);
            b.Property(x => x.FullName).IsRequired().HasMaxLength(50);
            b.Property(x => x.Specification).IsRequired().HasMaxLength(50);
            b.Property(x => x.EquivalentTon).HasColumnType("decimal(7, 6)");
            b.Property(x => x.Barcode).HasMaxLength(20);
            b.Property(x => x.TaxRate).HasColumnType("decimal(7, 6)");

            b.HasOne(x => x.CountyDay).WithMany(x => x.CountyProductSnapshots).HasForeignKey(x => x.CountyDayId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForCounty).WithMany(x => x.CountyProductSnapshots)
                .HasForeignKey(x => x.ProductForCountyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
