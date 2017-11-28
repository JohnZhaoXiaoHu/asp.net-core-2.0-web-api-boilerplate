using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Retail;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Settings
{
    public class Product : EntityBase
    {
        public string LegacyProductId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Specification { get; set; }
        public ProductUnit ProductUnit { get; set; }
        public int ShelfLife { get; set; }
        public decimal EquivalentTon { get; set; }
        public string Barcode { get; set; }
        public decimal TaxRate { get; set; }

        public ProductForRetail ProductForRetail { get; set; }
    }

    public class ProductConfiguration : EntityBaseConfiguration<Product>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Product> b)
        {
            b.Property(x => x.LegacyProductId).HasMaxLength(5);
            b.Property(x => x.Name).IsRequired().HasMaxLength(10);
            b.Property(x => x.FullName).IsRequired().HasMaxLength(50);
            b.Property(x => x.Specification).IsRequired().HasMaxLength(50);
            b.Property(x => x.EquivalentTon).HasColumnType("decimal(7, 6)");
            b.Property(x => x.Barcode).HasMaxLength(20);
            b.Property(x => x.TaxRate).HasColumnType("decimal(7, 6)");
        }
    }
}
