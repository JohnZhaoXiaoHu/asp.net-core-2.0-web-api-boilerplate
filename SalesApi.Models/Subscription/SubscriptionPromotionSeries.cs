using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Subscription
{
    public class SubscriptionPromotionSeries: EntityBase
    {
        public SubscriptionPromotionSeries()
        {
            SubscriptionPromotionSeriesBonuses = new List<SubscriptionPromotionSeriesBonus>();
            SubscriptionPromotionEvents = new List<SubscriptionPromotionEvent>();
        }
        
        public int ProductForSubscriptionId { get; set; }
        public string Name { get; set; }
        public DateRepeatType DateRepeatType { get; set; }
        public int Step { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PurchaseBase { get; set; }

        public ProductForSubscription ProductForSubscription { get; set; }
        public ICollection<SubscriptionPromotionSeriesBonus> SubscriptionPromotionSeriesBonuses { get; set; }
        public ICollection<SubscriptionPromotionEvent> SubscriptionPromotionEvents { get; set; }
    }

    public class SubscriptionPromotionSeriesConfiguration : EntityBaseConfiguration<SubscriptionPromotionSeries>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionPromotionSeries> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
            b.HasOne(x => x.ProductForSubscription).WithMany(x => x.SubscriptionPromotionSeries).HasForeignKey(x => x.ProductForSubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
