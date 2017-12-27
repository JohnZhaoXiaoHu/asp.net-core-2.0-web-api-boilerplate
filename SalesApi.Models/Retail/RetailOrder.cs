using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.County;

namespace SalesApi.Models.Retail
{
    public class RetailOrder : EntityBase
    {
        public int RetailProductSnapshotId { get; set; }
        public int RetailerId { get; set; }
        public int? RetailPromotionEventId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }

        public RetailProductSnapshot RetailProductSnapshot { get; set; }
        public Retailer Retailer { get; set; }
        public RetailPromotionEvent RetailPromotionEvent { get; set; }
        public ICollection<RetailPromotionGiftOrder> RetailPromotionGiftOrders { get; set; }
    }

    public class RetailOrderConfiguration : EntityBaseConfiguration<RetailOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailOrder> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.Property(x => x.LegacyOrderId).HasMaxLength(20);
            b.HasOne(x => x.RetailProductSnapshot).WithMany().HasForeignKey(x => x.RetailProductSnapshotId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Retailer).WithMany().HasForeignKey(x => x.RetailerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.RetailPromotionEvent).WithMany().HasForeignKey(x => x.RetailPromotionEventId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.Date, x.RetailProductSnapshotId, x.RetailerId }).IsUnique();
        }
    }
}
