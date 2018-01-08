using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription
{
    public class SubscriptionPromotionEvent : EntityBase
    {
        public SubscriptionPromotionEvent()
        {
            SubscriptionPromotionEventBonuses = new List<SubscriptionPromotionEventBonus>();
        }

        public int SubscriptionPromotionSeriesId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PurchaseBase { get; set; }

        public SubscriptionPromotionSeries SubscriptionPromotionSeries { get; set; }
        public ProductForSubscription ProductForSubscription { get; set; }
        public ICollection<SubscriptionPromotionEventBonus> SubscriptionPromotionEventBonuses { get; set; }
    }

    public class SubscriptionPromotionEventConfiguration : EntityBaseConfiguration<SubscriptionPromotionEvent>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotionEvent> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
            b.HasOne(x => x.SubscriptionPromotionSeries).WithMany(x => x.SubscriptionPromotionEvents).HasForeignKey(x => x.SubscriptionPromotionSeriesId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForSubscription).WithMany(x => x.SubscriptionPromotionEvents).HasForeignKey(x => x.ProductForSubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.ProductForSubscriptionId, x.Date }).IsUnique();
        }
    }
}
