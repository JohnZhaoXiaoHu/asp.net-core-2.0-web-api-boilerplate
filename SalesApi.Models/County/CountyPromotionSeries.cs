using System;
using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.County
{
    public class CountyPromotionSeries: EntityBase
    {
        public CountyPromotionSeries()
        {
            CountyPromotionSeriesBonuses = new List<CountyPromotionSeriesBonus>();
            CountyPromotionEvents = new List<CountyPromotionEvent>();
        }
        
        public int ProductForCountyId { get; set; }
        public string Name { get; set; }
        public DateRepeatType DateRepeatType { get; set; }
        public int Step { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PurchaseBase { get; set; }

        public ProductForCounty ProductForCounty { get; set; }
        public ICollection<CountyPromotionSeriesBonus> CountyPromotionSeriesBonuses { get; set; }
        public ICollection<CountyPromotionEvent> CountyPromotionEvents { get; set; }
    }

    public class CountyPromotionSeriesConfiguration : EntityBaseConfiguration<CountyPromotionSeries>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyPromotionSeries> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
            b.HasOne(x => x.ProductForCounty).WithMany(x => x.CountyPromotionSeries).HasForeignKey(x => x.ProductForCountyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
