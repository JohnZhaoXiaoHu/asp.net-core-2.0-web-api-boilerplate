using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Retail
{
    public class RetailPromotionSeriesBonus : EntityBase
    {
        public int RetailPromotionSeriesId { get; set; }
        public int ProductForRetailId { get; set; }
        public int BonusCount { get; set; }

        public RetailPromotionSeries RetailPromotionSeries { get; set; }
        public ProductForRetail ProductForRetail { get; set; }
    }

    public class RetailPromotionSeriesBonusConfiguration : EntityBaseConfiguration<RetailPromotionSeriesBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailPromotionSeriesBonus> b)
        {
            b.HasOne(x => x.RetailPromotionSeries).WithMany(x => x.RetailPromotionSeriesBonuses)
                .HasForeignKey(x => x.RetailPromotionSeriesId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForRetail).WithMany(x => x.RetailPromotionSeriesBonuses)
                .HasForeignKey(x => x.ProductForRetailId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
