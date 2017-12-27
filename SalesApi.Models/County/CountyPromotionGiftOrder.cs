using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.County
{
    public class CountyPromotionGiftOrder : EntityBase
    {
        public int CountyOrderId { get; set; }
        public int CountyPromotionEventBonusId { get; set; }
        public int PromotionGift { get; set; }

        public CountyOrder CountyOrder { get; set; }
        public CountyPromotionEventBonus CountyPromotionEventBonus { get; set; }
    }

    public class CountyPromotionGiftOrderConfiguration : EntityBaseConfiguration<CountyPromotionGiftOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyPromotionGiftOrder> b)
        {
            b.HasOne(x => x.CountyOrder).WithMany(x => x.CountyPromotionGiftOrders).HasForeignKey(x => x.CountyOrderId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.CountyPromotionEventBonus).WithMany().HasForeignKey(x => x.CountyPromotionEventBonusId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new {x.CountyOrderId, x.CountyPromotionEventBonusId}).IsUnique();
        }
    }
}
