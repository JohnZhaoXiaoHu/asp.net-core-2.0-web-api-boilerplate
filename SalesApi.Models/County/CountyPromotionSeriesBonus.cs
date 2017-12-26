using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.County
{
    public class CountyPromotionSeriesBonus : EntityBase
    {
        public int CountyPromotionSeriesId { get; set; }
        public int ProductForCountyId { get; set; }
        public int BonusCount { get; set; }

        public CountyPromotionSeries CountyPromotionSeries { get; set; }
        public ProductForCounty ProductForCounty { get; set; }
    }

    public class CountyPromotionSeriesBonusConfiguration : EntityBaseConfiguration<CountyPromotionSeriesBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyPromotionSeriesBonus> b)
        {
            b.HasOne(x => x.CountyPromotionSeries).WithMany(x => x.CountyPromotionSeriesBonuses)
                .HasForeignKey(x => x.CountyPromotionSeriesId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForCounty).WithMany(x => x.CountyPromotionSeriesBonuses)
                .HasForeignKey(x => x.ProductForCountyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
