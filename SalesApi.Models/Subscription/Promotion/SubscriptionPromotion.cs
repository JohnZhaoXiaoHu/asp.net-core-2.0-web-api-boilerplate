using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Subscription.Promotion
{
    public class SubscriptionPromotion : EntityBase
    {
        public SubscriptionPromotion()
        {
            SubscriptionPromotionMonths = new List<SubscriptionPromotionMonth>();
        }

        public string Name { get; set; }
        public SubscriptionPromotionType PromotionType { get; set; }
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }
        public int MonthSpan { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public int DayBase { get; set; }

        public ProductForSubscription ProductForSubscription { get; set; }
        public ICollection<SubscriptionPromotionMonth> SubscriptionPromotionMonths { get; set; }
    }

    public class SubscriptionPromotionConfiguration : EntityBaseConfiguration<SubscriptionPromotion>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotion> b)
        {
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
            b.HasOne(x => x.ProductForSubscription).WithMany().HasForeignKey(x => x.ProductForSubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
