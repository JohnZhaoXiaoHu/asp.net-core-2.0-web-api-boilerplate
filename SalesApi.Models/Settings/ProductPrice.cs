using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedSettings.Enums;

namespace SalesApi.Models.Settings
{
    public class ProductPrice : EntityBase
    {
        public int ProductId { get; set; }
        public SalesType SalesType { get; set; }
        public int EquivalentBox { get; set; }
        public decimal Price { get; set; }
        public decimal InternalPrice { get; set; }
        public decimal BoxPrice { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }
        public bool IsOutOfStock { get; set; }

        public Product Product { get; set; }
    }

    public class ProductPriceConfiguration : EntityBaseConfiguration<ProductPrice>
    {
        public override void ConfigureDerived(EntityTypeBuilder<ProductPrice> b)
        {
            b.Property(x => x.Price).HasColumnType("decimal(10, 6)");
            b.Property(x => x.InternalPrice).HasColumnType("decimal(10, 6)");
            b.Property(x => x.BoxPrice).HasColumnType("decimal(10, 2)");
            b.HasOne(x => x.Product).WithMany(x => x.ProductPrices).HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new {x.ProductId, x.SalesType}).IsUnique();
        }
    }
}
