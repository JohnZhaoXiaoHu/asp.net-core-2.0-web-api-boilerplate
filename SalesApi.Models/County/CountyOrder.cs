using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.County
{
    public class CountyOrder : EntityBase
    {
        public CountyOrder()
        {
            CountyPromotionGiftOrders = new List<CountyPromotionGiftOrder>();
        }
        
        public int CountyProductSnapshotId { get; set; }
        public int CountyAgentId { get; set; }
        public int? CountyPromotionEventId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }
        public decimal Price { get; set; }

        public CountyProductSnapshot CountyProductSnapshot { get; set; }
        public CountyAgent CountyAgent { get; set; }
        public CountyPromotionEvent CountyPromotionEvent { get; set; }
        public ICollection<CountyPromotionGiftOrder> CountyPromotionGiftOrders { get; set; }
    }

    public class CountyOrderConfiguration : EntityBaseConfiguration<CountyOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyOrder> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.Property(x => x.LegacyOrderId).HasMaxLength(20);
            b.Property(x => x.Price).HasColumnType("decimal(10, 2)");
            b.HasOne(x => x.CountyProductSnapshot).WithMany().HasForeignKey(x => x.CountyProductSnapshotId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.CountyAgent).WithMany().HasForeignKey(x => x.CountyAgentId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.CountyPromotionEvent).WithMany().HasForeignKey(x => x.CountyPromotionEventId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.Date, x.CountyProductSnapshotId, x.CountyAgentId }).IsUnique();
        }
    }
}
