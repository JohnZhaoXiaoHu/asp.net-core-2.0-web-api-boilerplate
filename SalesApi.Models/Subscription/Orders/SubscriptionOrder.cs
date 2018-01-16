using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Models.Subscription.Orders
{
    public class SubscriptionOrder: EntityBase
    {
        public SubscriptionOrder()
        {
            SubscriptionOrderDates = new List<SubscriptionOrderDate>();
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int MilkmanId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public int? SubscriptionMonthPromotionId { get; set; }
        public int PresetDayCount { get; set; }
        public int PresetDayBonus { get; set; }
        public int PresetDayGift { get; set; } // 批条

        public Milkman Milkman { get; set; }
        public SubscriptionProductSnapshot SubscriptionProductSnapshot { get; set; }
        public SubscriptionMonthPromotion SubscriptionMonthPromotion { get; set; }
        public ICollection<SubscriptionOrderDate> SubscriptionOrderDates { get; set; }
        public ICollection<SubscriptionOrderBonus> SubscriptionOrderBonuses { get; set; }
    }

    public class SubscriptionOrderConfiguration : EntityBaseConfiguration<SubscriptionOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionOrder> b)
        {
            b.HasOne(x => x.Milkman).WithMany().HasForeignKey(x => x.MilkmanId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SubscriptionProductSnapshot).WithMany().HasForeignKey(x => x.SubscriptionProductSnapshotId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SubscriptionMonthPromotion).WithMany().HasForeignKey(x => x.SubscriptionMonthPromotionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
