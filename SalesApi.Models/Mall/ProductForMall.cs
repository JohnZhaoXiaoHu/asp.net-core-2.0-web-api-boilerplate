using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Settings;

namespace SalesApi.Models.Mall
{
    public class ProductForMall : EntityBase
    {
        public ProductForMall()
        {
            MallProductSnapshots = new List<MallProductSnapshot>();
            MallPrices = new List<MallPrice>();
        }

        public int ProductId { get; set; }
        public int EquivalentBox { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }

        public Product Product { get; set; }
        public ICollection<MallProductSnapshot> MallProductSnapshots { get; set; }
        public ICollection<MallPrice> MallPrices { get; set; }
    }

    public class ProductForMallConfiguration : EntityBaseConfiguration<ProductForMall>
    {
        public override void ConfigureDerived(EntityTypeBuilder<ProductForMall> b)
        {
            b.HasOne(x => x.Product).WithOne(x => x.ProductForMall).HasForeignKey<ProductForMall>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
