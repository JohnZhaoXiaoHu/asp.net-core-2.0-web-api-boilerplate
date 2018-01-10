using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace SalesApi.Models.Subscription.Promotion
{
    public class SubscriptionPromotionMonth: EntityBase
    {
        public SubscriptionPromotionMonth()
        {
            SubscriptionPromotionMonthBonuses = new List<SubscriptionPromotionMonthBonus>();
        }

        public int SubscriptionPromotionId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public SubscriptionPromotion SubscriptionPromotion { get; set; }
        public ICollection<SubscriptionPromotionMonthBonus> SubscriptionPromotionMonthBonuses { get; set; }
    }

    public class SubscriptionPromotionMonthConfiguration : EntityBaseConfiguration<SubscriptionPromotionMonth>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotionMonth> b)
        {
            b.HasOne(x => x.SubscriptionPromotion).WithMany(x => x.SubscriptionPromotionMonths)
                .HasForeignKey(x => x.SubscriptionPromotionId).OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new {x.SubscriptionPromotionId, x.Year, x.Month}).IsUnique();
        }
    }
}
