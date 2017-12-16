using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Collective
{
    public class CollectivePrice: EntityBase
    {
        public int CollectiveCustomerId { get; set; }
        public int ProductForCollectiveId { get; set; }
        public decimal Price { get; set; }

        public CollectiveCustomer CollectiveCustomer { get; set; }
        public ProductForCollective ProductForCollective { get; set; }
    }

    public class CollectivePriceConfiguration : EntityBaseConfiguration<CollectivePrice>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CollectivePrice> b)
        {
            b.HasOne(x => x.CollectiveCustomer).WithMany(x => x.CollectivePrices)
                .HasForeignKey(x => x.CollectiveCustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForCollective).WithMany(x => x.CollectivePrices)
                .HasForeignKey(x => x.ProductForCollectiveId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new {x.CollectiveCustomerId, x.ProductForCollectiveId}).IsUnique();
        }
    }
}
