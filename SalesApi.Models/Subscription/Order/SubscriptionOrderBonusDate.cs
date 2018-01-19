using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Models.Subscription.Order
{
    public class SubscriptionOrderBonusDate : EntityBase
    {
        public int SubscriptionOrderId { get; set; }
        public int SubscriptionMonthPromotionBonusDateId { get; set; }

        public SubscriptionOrder SubscriptionOrder { get; set; }
        public SubscriptionMonthPromotionBonusDate SubscriptionMonthPromotionBonusDate { get; set; }
    }

    public class SubscriptionOrderBonusConfiguration : EntityBaseConfiguration<SubscriptionOrderBonusDate>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionOrderBonusDate> b)
        {
            b.HasIndex(x => new { x.SubscriptionOrderId, x.SubscriptionMonthPromotionBonusDateId }).IsUnique();
            b.HasOne(x => x.SubscriptionOrder).WithMany(x => x.SubscriptionOrderBonusDates)
                .HasForeignKey(x => x.SubscriptionOrderId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SubscriptionMonthPromotionBonusDate).WithMany()
                .HasForeignKey(x => x.SubscriptionMonthPromotionBonusDateId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
