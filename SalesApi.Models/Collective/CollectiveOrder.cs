using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Collective
{
    public class CollectiveOrder : EntityBase
    {
        public int CollectiveProductSnapshotId { get; set; }
        public int CollectiveCustomerId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }
        public decimal Price { get; set; }

        public CollectiveProductSnapshot CollectiveProductSnapshot { get; set; }
        public CollectiveCustomer CollectiveCustomer { get; set; }
    }

    public class CollectiveOrderConfiguration : EntityBaseConfiguration<CollectiveOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CollectiveOrder> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.Property(x => x.LegacyOrderId).HasMaxLength(20);
            b.Property(x => x.Price).HasColumnType("decimal(10, 2)");
            b.HasOne(x => x.CollectiveProductSnapshot).WithMany().HasForeignKey(x => x.CollectiveProductSnapshotId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.CollectiveCustomer).WithMany().HasForeignKey(x => x.CollectiveCustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.Date, x.CollectiveProductSnapshotId, x.CollectiveCustomerId }).IsUnique();
        }
    }
}
