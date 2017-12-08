using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Retail
{
    public class RetailOrder : EntityBase
    {
        public int ProductForRetailId { get; set; }
        public int RetailerId { get; set; }
        public int? RetailPromotionEventId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }

        public ProductForRetail ProductForRetail { get; set; }
        public Retailer Retailer { get; set; }
        public RetailPromotionEvent RetailPromotionEvent { get; set; }
    }

    public class RetailOrderConfiguration : EntityBaseConfiguration<RetailOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailOrder> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.Property(x => x.LegacyOrderId).HasMaxLength(20);
            b.HasOne(x => x.ProductForRetail).WithMany().HasForeignKey(x => x.ProductForRetailId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Retailer).WithMany().HasForeignKey(x => x.RetailerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.RetailPromotionEvent).WithMany().HasForeignKey(x => x.RetailPromotionEventId)
                .OnDelete(DeleteBehavior.SetNull);
            b.HasIndex(x => new { x.Date, x.ProductForRetailId, x.RetailerId }).IsUnique();
        }
    }
}
