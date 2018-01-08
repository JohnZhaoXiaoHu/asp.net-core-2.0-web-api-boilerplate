using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription
{
    public class SubscriptionPromotionSeriesBonus : EntityBase
    {
        public int SubscriptionPromotionSeriesId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int BonusCount { get; set; }

        public SubscriptionPromotionSeries SubscriptionPromotionSeries { get; set; }
        public ProductForSubscription ProductForSubscription { get; set; }
    }

    public class SubscriptionPromotionSeriesBonusConfiguration : EntityBaseConfiguration<SubscriptionPromotionSeriesBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotionSeriesBonus> b)
        {
            b.HasOne(x => x.SubscriptionPromotionSeries).WithMany(x => x.SubscriptionPromotionSeriesBonuses)
                .HasForeignKey(x => x.SubscriptionPromotionSeriesId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForSubscription).WithMany(x => x.SubscriptionPromotionSeriesBonuses)
                .HasForeignKey(x => x.ProductForSubscriptionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
