using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription
{
    public class SubscriptionPromotionEventBonus: EntityBase
    {
        public int SubscriptionPromotionEventId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int BonusCount { get; set; }

        public SubscriptionPromotionEvent SubscriptionPromotionEvent { get; set; }
        public ProductForSubscription ProductForSubscription { get; set; }
    }

    public class SubscriptionPromotionEventBonusConfiguration : EntityBaseConfiguration<SubscriptionPromotionEventBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotionEventBonus> b)
        {
            b.HasOne(x => x.SubscriptionPromotionEvent).WithMany(x => x.SubscriptionPromotionEventBonuses)
                .HasForeignKey(x => x.SubscriptionPromotionEventId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForSubscription).WithMany(x => x.SubscriptionPromotionEventBonuses)
                .HasForeignKey(x => x.ProductForSubscriptionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
