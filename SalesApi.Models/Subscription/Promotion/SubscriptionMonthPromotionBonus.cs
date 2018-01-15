using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription.Promotion
{
    public class SubscriptionMonthPromotionBonus: EntityBase
    {
        public SubscriptionMonthPromotionBonus()
        {
            SubscriptionMonthPromotionBonusDates = new List<SubscriptionMonthPromotionBonusDate>();
        }

        public int SubscriptionMonthPromotionId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int PresetDayBonusCount { get; set; }

        public SubscriptionMonthPromotion SubscriptionMonthPromotion { get; set; }
        public ICollection<SubscriptionMonthPromotionBonusDate> SubscriptionMonthPromotionBonusDates { get; set; }
    }

    public class SubscriptionMonthPromotionBonusConfiguration : EntityBaseConfiguration<SubscriptionMonthPromotionBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionMonthPromotionBonus> b)
        {
            b.HasOne(x => x.SubscriptionMonthPromotion).WithMany(x => x.SubscriptionMonthPromotionBonuses)
                .HasForeignKey(x => x.SubscriptionMonthPromotionId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.SubscriptionMonthPromotionId, x.ProductForSubscriptionId, x.PresetDayBonusCount }).IsUnique();
        }
    }
}
