using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Retail
{
    public class RetailPromotionSeries: EntityBase
    {
        public RetailPromotionSeries()
        {
            RetailPromotionSeriesBonuses = new List<RetailPromotionSeriesBonus>();
            RetailPromotionEvents = new List<RetailPromotionEvent>();
        }

        public SalesType SalesType { get; set; }
        public int ProductForRetailId { get; set; }
        public string Name { get; set; }
        public DateRepeatType DateRepeatType { get; set; }
        public int Step { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PurchaseBase { get; set; }

        public ProductForRetail ProductForRetail { get; set; }
        public ICollection<RetailPromotionSeriesBonus> RetailPromotionSeriesBonuses { get; set; }
        public ICollection<RetailPromotionEvent> RetailPromotionEvents { get; set; }
    }

    public class RetailPromotionSeriesConfiguration : EntityBaseConfiguration<RetailPromotionSeries>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailPromotionSeries> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
            b.HasOne(x => x.ProductForRetail).WithMany(x => x.RetailPromotionSeries).HasForeignKey(x => x.ProductForRetailId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
