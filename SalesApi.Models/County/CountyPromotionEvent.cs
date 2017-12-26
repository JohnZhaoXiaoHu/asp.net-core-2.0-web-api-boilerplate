using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.County
{
    public class CountyPromotionEvent : EntityBase
    {
        public CountyPromotionEvent()
        {
            CountyPromotionEventBonuses = new List<CountyPromotionEventBonus>();
            CountyOrders = new List<CountyOrder>();
        }

        public int CountyPromotionSeriesId { get; set; }
        public int ProductForCountyId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PurchaseBase { get; set; }

        public CountyPromotionSeries CountyPromotionSeries { get; set; }
        public ProductForCounty ProductForCounty { get; set; }
        public ICollection<CountyPromotionEventBonus> CountyPromotionEventBonuses { get; set; }
        public ICollection<CountyOrder> CountyOrders { get; set; }
    }

    public class CountyPromotionEventConfiguration : EntityBaseConfiguration<CountyPromotionEvent>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyPromotionEvent> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
            b.HasOne(x => x.CountyPromotionSeries).WithMany(x => x.CountyPromotionEvents).HasForeignKey(x => x.CountyPromotionSeriesId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForCounty).WithMany(x => x.CountyPromotionEvents).HasForeignKey(x => x.ProductForCountyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
