using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.County
{
    public class CountyPromotionEventBonus: EntityBase
    {
        public int CountyPromotionEventId { get; set; }
        public int ProductForCountyId { get; set; }
        public int BonusCount { get; set; }

        public CountyPromotionEvent CountyPromotionEvent { get; set; }
        public ProductForCounty ProductForCounty { get; set; }
    }

    public class CountyPromotionEventBonusConfiguration : EntityBaseConfiguration<CountyPromotionEventBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyPromotionEventBonus> b)
        {
            b.HasOne(x => x.CountyPromotionEvent).WithMany(x => x.CountyPromotionEventBonuses)
                .HasForeignKey(x => x.CountyPromotionEventId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForCounty).WithMany(x => x.CountyPromotionEventBonuses)
                .HasForeignKey(x => x.ProductForCountyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
