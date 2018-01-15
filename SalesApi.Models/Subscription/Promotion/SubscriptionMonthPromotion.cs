using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Subscription.Promotion
{
    public class SubscriptionMonthPromotion: EntityBase
    {
        public SubscriptionMonthPromotion()
        {
            SubscriptionMonthPromotionBonuses = new List<SubscriptionMonthPromotionBonus>();
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public string Name { get; set; }
        public SubscriptionPromotionType PromotionType { get; set; }
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBase { get; set; }

        public ProductForSubscription ProductForSubscription { get; set; }
        public ICollection<SubscriptionMonthPromotionBonus> SubscriptionMonthPromotionBonuses { get; set; }
    }

    public class SubscriptionMonthPromotionConfiguration : EntityBaseConfiguration<SubscriptionMonthPromotion>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionMonthPromotion> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
            b.HasOne(x => x.ProductForSubscription).WithMany().HasForeignKey(x => x.ProductForSubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
