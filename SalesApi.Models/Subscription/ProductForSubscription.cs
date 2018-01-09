using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Settings;

namespace SalesApi.Models.Subscription
{
    public class ProductForSubscription : EntityBase
    {
        public ProductForSubscription()
        {
            SubscriptionProductSnapshots = new List<SubscriptionProductSnapshot>();
        }

        public int ProductId { get; set; }
        public int EquivalentBox { get; set; }
        public decimal Price { get; set; }
        public decimal InternalPrice { get; set; }
        public decimal BoxPrice { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }

        public Product Product { get; set; }
        public ICollection<SubscriptionProductSnapshot> SubscriptionProductSnapshots { get; set; }
    }

    public class ProductForSubscriptionConfiguration : EntityBaseConfiguration<ProductForSubscription>
    {
        public override void ConfigureDerived(EntityTypeBuilder<ProductForSubscription> b)
        {
            b.HasOne(x => x.Product).WithOne(x => x.ProductForSubscription).HasForeignKey<ProductForSubscription>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
