using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Retail
{
    public class RetailPromotionEventBonus: EntityBase
    {
        public int RetailPromotionEventId { get; set; }
        public int ProductForRetailId { get; set; }
        public int BonusCount { get; set; }

        public RetailPromotionEvent RetailPromotionEvent { get; set; }
        public ProductForRetail ProductForRetail { get; set; }
    }

    public class RetailPromotionEventBonusConfiguration : EntityBaseConfiguration<RetailPromotionEventBonus>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailPromotionEventBonus> b)
        {
            b.HasOne(x => x.RetailPromotionEvent).WithMany(x => x.RetailPromotionEventBonuses)
                .HasForeignKey(x => x.RetailPromotionEventId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForRetail).WithMany(x => x.RetailPromotionEventBonuses)
                .HasForeignKey(x => x.ProductForRetailId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
