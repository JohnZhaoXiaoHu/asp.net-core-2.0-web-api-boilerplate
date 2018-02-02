using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Subscription.Promotion;

namespace SalesApi.Models.Subscription.Order
{
    public class SubscriptionOrder: EntityBase
    {
        public SubscriptionOrder()
        {
            SubscriptionOrderDates = new List<SubscriptionOrderDate>();
            SubscriptionOrderBonusDates = new List<SubscriptionOrderBonusDate>();
            SubscriptionOrderModifiedBonusDates = new List<SubscriptionOrderModifiedBonusDate>();
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int MilkmanId { get; set; }
        public int SubscriptionProductSnapshotId { get; set; }
        public int? SubscriptionMonthPromotionId { get; set; }
        public int PresetDayCount { get; set; }
        public int PresetDayBonus { get; set; }
        public int PresetDayGift { get; set; } // 批条
        
        public DateTime? PaidTime { get; set; }

        public Milkman Milkman { get; set; }
        public SubscriptionProductSnapshot SubscriptionProductSnapshot { get; set; }
        public SubscriptionMonthPromotion SubscriptionMonthPromotion { get; set; }
        public ICollection<SubscriptionOrderDate> SubscriptionOrderDates { get; set; }
        public ICollection<SubscriptionOrderBonusDate> SubscriptionOrderBonusDates { get; set; }
        public ICollection<SubscriptionOrderModifiedBonusDate> SubscriptionOrderModifiedBonusDates { get; set; }
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
