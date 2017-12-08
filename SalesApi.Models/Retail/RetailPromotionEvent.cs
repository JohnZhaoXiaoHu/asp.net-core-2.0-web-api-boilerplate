using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Retail
{
    public class RetailPromotionEvent : EntityBase
    {
        public RetailPromotionEvent()
        {
            RetailPromotionEventBonuses = new List<RetailPromotionEventBonus>();
            RetailOrders = new List<RetailOrder>();
        }

        public int RetailPromotionSeriesId { get; set; }
        public int ProductForRetailId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PurchaseBase { get; set; }

        public RetailPromotionSeries RetailPromotionSeries { get; set; }
        public ProductForRetail ProductForRetail { get; set; }
        public ICollection<RetailPromotionEventBonus> RetailPromotionEventBonuses { get; set; }
        public ICollection<RetailOrder> RetailOrders { get; set; }
    }

    public class RetailPromotionEventConfiguration : EntityBaseConfiguration<RetailPromotionEvent>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailPromotionEvent> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
            b.HasOne(x => x.RetailPromotionSeries).WithMany(x => x.RetailPromotionEvents).HasForeignKey(x => x.RetailPromotionSeriesId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForRetail).WithMany(x => x.RetailPromotionEvents).HasForeignKey(x => x.ProductForRetailId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
