using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Collective
{
    public class ProductForCollective : EntityBase
    {
        public ProductForCollective()
        {
            SalesType = SalesType.集体户;
            CollectiveProductSnapshots = new List<CollectiveProductSnapshot>();
        }

        public int ProductId { get; set; }
        public SalesType SalesType { get; set; }
        public int EquivalentBox { get; set; }
        public decimal Price { get; set; }
        public decimal InternalPrice { get; set; }
        public decimal BoxPrice { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }

        public Product Product { get; set; }
        public ICollection<CollectiveProductSnapshot> CollectiveProductSnapshots { get; set; }
    }

    public class ProductForCollectiveConfiguration : EntityBaseConfiguration<ProductForCollective>
    {
        public override void ConfigureDerived(EntityTypeBuilder<ProductForCollective> b)
        {
            b.Property(x => x.Price).HasColumnType("decimal(10, 6)");
            b.Property(x => x.InternalPrice).HasColumnType("decimal(10, 6)");
            b.Property(x => x.BoxPrice).HasColumnType("decimal(10, 2)");
            b.HasOne(x => x.Product).WithOne(x => x.ProductForCollective).HasForeignKey<ProductForRetail>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
