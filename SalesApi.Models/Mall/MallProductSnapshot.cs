using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Mall
{
    public class MallProductSnapshot : EntityBase
    {
        public int MallDayId { get; set; }
        public int ProductForMallId { get; set; }
        
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

        public MallDay MallDay { get; set; }
        public ProductForMall ProductForMall { get; set; }
    }

    public class MallProductSnapshotConfiguration : EntityBaseConfiguration<MallProductSnapshot>
    {
        public override void ConfigureDerived(EntityTypeBuilder<MallProductSnapshot> b)
        {
            b.Property(x => x.LegacyProductId).HasMaxLength(5);
            b.Property(x => x.Name).IsRequired().HasMaxLength(10);
            b.Property(x => x.FullName).IsRequired().HasMaxLength(50);
            b.Property(x => x.Specification).IsRequired().HasMaxLength(50);
            b.Property(x => x.EquivalentTon).HasColumnType("decimal(7, 6)");
            b.Property(x => x.Barcode).HasMaxLength(20);
            b.Property(x => x.TaxRate).HasColumnType("decimal(7, 6)");

            b.HasOne(x => x.MallDay).WithMany(x => x.MallProductSnapshots).HasForeignKey(x => x.MallDayId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForMall).WithMany(x => x.MallProductSnapshots)
                .HasForeignKey(x => x.ProductForMallId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
