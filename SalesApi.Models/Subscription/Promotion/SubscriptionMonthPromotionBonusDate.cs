using System;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription.Promotion
{
    public class SubscriptionMonthPromotionBonusDate : EntityBase
    {
        public int SubscriptionMonthPromotionBonusId { get; set; }
        public DateTime Date { get; set; }
        public int DayBonusCount { get; set; }

        public SubscriptionMonthPromotionBonus SubscriptionMonthPromotionBonus { get; set; }
    }

    public class SubscriptionMonthPromotionBonusDateConfiguration
        : EntityBaseConfiguration<SubscriptionMonthPromotionBonusDate>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionMonthPromotionBonusDate> b)
        {

            b.HasIndex(x => new { x.SubscriptionMonthPromotionBonusId, x.Date }).IsUnique();
            b.HasOne(x => x.SubscriptionMonthPromotionBonus)
                .WithMany(x => x.SubscriptionMonthPromotionBonusDates)
                .HasForeignKey(x => x.SubscriptionMonthPromotionBonusId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
