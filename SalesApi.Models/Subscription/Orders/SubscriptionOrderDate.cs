using System;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription.Orders
{
    public class SubscriptionOrderDate: EntityBase
    {
        public int SubscriptionOrderId { get; set; }
        public DateTime Date { get; set; }

        public SubscriptionOrder SubscriptionOrder { get; set; }
    }

    public class SubscriptionOrderDateConfiguration : EntityBaseConfiguration<SubscriptionOrderDate>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionOrderDate> b)
        {
            b.HasIndex(x => new {x.SubscriptionOrderId, x.Date}).IsUnique();
            b.HasOne(x => x.SubscriptionOrder).WithMany(x => x.SubscriptionOrderDates)
                .HasForeignKey(x => x.SubscriptionOrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
