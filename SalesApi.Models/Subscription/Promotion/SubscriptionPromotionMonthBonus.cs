using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription.Promotion
{
    public class SubscriptionPromotionMonthBonus : EntityBase
    {
        public SubscriptionPromotionMonthBonus()
        {
            SubscriptionPromotionMonthBonusDeliveryDates = new List<SubscriptionPromotionMonthBonusDeliveryDate>();
        }

        public int SubscriptionPromotionMonthId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBonusCount { get; set; }

        public SubscriptionPromotionMonth SubscriptionPromotionMonth { get; set; }
        public ICollection<SubscriptionPromotionMonthBonusDeliveryDate> SubscriptionPromotionMonthBonusDeliveryDates { get; set; }
    }

    public class SubscriptionPromotionMonthBonusConfiguration : EntityBaseConfiguration<SubscriptionPromotionMonthBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotionMonthBonus> b)
        {
            b.HasOne(x => x.SubscriptionPromotionMonth).WithMany(x => x.SubscriptionPromotionMonthBonuses)
                .HasForeignKey(x => x.SubscriptionPromotionMonthId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.SubscriptionPromotionMonthId, x.ProductForSubscriptionId, x.DayBonusCount }).IsUnique();
        }
    }
}
