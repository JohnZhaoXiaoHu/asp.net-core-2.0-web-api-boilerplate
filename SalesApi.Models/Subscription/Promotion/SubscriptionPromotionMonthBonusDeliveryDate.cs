using System;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription.Promotion
{
    public class SubscriptionPromotionMonthBonusDeliveryDate: EntityBase
    {
        public int SubscriptionPromotionMonthBonusId { get; set; }
        public DateTime Date { get; set; }
        public int DayBonusCount { get; set; }

        public SubscriptionPromotionMonthBonus SubscriptionPromotionMonthBonus { get; set; }
    }

    public class
        SubscriptionPromotionMonthBonusDeliveryDateConfiguration : EntityBaseConfiguration<
            SubscriptionPromotionMonthBonusDeliveryDate>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotionMonthBonusDeliveryDate> b)
        {
            b.HasIndex(x => new {x.SubscriptionPromotionMonthBonusId, x.Date}).IsUnique();
            b.HasOne(x => x.SubscriptionPromotionMonthBonus)
                .WithMany(x => x.SubscriptionPromotionMonthBonusDeliveryDates)
                .HasForeignKey(x => x.SubscriptionPromotionMonthBonusId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
