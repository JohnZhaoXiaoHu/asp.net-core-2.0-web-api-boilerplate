using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Mall
{
    public class MallOrder : EntityBase
    {
        public int MallProductSnapshotId { get; set; }
        public int MallCustomerId { get; set; }

        public string Date { get; set; }
        public string LegacyOrderId { get; set; }
        public int Ordered { get; set; }
        public int Gift { get; set; }
        public decimal Price { get; set; }

        public MallProductSnapshot MallProductSnapshot { get; set; }
        public MallCustomer MallCustomer { get; set; }
    }

    public class MallOrderConfiguration : EntityBaseConfiguration<MallOrder>
    {
        public override void ConfigureDerived(EntityTypeBuilder<MallOrder> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.Property(x => x.LegacyOrderId).HasMaxLength(20);
            b.Property(x => x.Price).HasColumnType("decimal(10, 2)");
            b.HasOne(x => x.MallProductSnapshot).WithMany().HasForeignKey(x => x.MallProductSnapshotId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.MallCustomer).WithMany().HasForeignKey(x => x.MallCustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new { x.Date, x.MallProductSnapshotId, x.MallCustomerId }).IsUnique();
        }
    }
}
