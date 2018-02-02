using System;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription.Order
{
    public class SubscriptionOrderModifiedBonusDate : EntityBase
    {
        public int SubscriptionOrderId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public DateTime Date { get; set; }
        public int DayCount { get; set; }

        public SubscriptionOrder SubscriptionOrder { get; set; }
        public SubscriptionProductSnapshot SubscriptionProductSnapshot { get; set; }
    }

    public class SubscriptionOrderModifiedBonusDateConfiguration : EntityBaseConfiguration<SubscriptionOrderModifiedBonusDate>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionOrderModifiedBonusDate> b)
        {
            b.HasOne(x => x.SubscriptionOrder).WithMany(x => x.SubscriptionOrderModifiedBonusDates)
                .HasForeignKey(x => x.SubscriptionOrderId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SubscriptionProductSnapshot).WithMany().HasForeignKey(x => x.SubscriptionProductSnapshotId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.SubscriptionOrderId, x.SubscriptionProductSnapshotId, x.Date }).IsUnique();
        }
    }
}
