using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Retail
{
    public class RetailPromotionGiftOrder : EntityBase
    {
        public int RetailOrderId { get; set; }
        public int RetailPromotionEventBonusId { get; set; }
        public int PromotionGift { get; set; }

        public RetailOrder RetailOrder { get; set; }
        public RetailPromotionEventBonus RetailPromotionEventBonus { get; set; }
    }

    public class RetailPromotionGiftOrderConfiguration : EntityBaseConfiguration<RetailPromotionGiftOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailPromotionGiftOrder> b)
        {
            b.HasOne(x => x.RetailOrder).WithMany(x => x.RetailPromotionGiftOrders).HasForeignKey(x => x.RetailOrderId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.RetailPromotionEventBonus).WithMany().HasForeignKey(x => x.RetailPromotionEventBonusId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new {x.RetailOrderId, x.RetailPromotionEventBonusId}).IsUnique();
        }
    }
}
