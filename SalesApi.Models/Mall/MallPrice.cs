using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Mall
{
    public class MallPrice: EntityBase
    {
        public int MallCustomerId { get; set; }
        public int ProductForMallId { get; set; }
        public decimal Price { get; set; }

        public MallCustomer MallCustomer { get; set; }
        public ProductForMall ProductForMall { get; set; }
    }

    public class MallPriceConfiguration : EntityBaseConfiguration<MallPrice>
    {
        public override void ConfigureDerived(EntityTypeBuilder<MallPrice> b)
        {
            b.HasOne(x => x.MallCustomer).WithMany(x => x.MallPrices)
                .HasForeignKey(x => x.MallCustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForMall).WithMany(x => x.MallPrices)
                .HasForeignKey(x => x.ProductForMallId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new {x.MallCustomerId, x.ProductForMallId}).IsUnique();
        }
    }
}
