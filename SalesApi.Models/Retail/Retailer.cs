using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Settings;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Retail
{
    public class Retailer: EntityBase
    {
        public int SubAreaId { get; set; }
        public SalesType SalesType { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Pinyin { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public SubArea SubArea { get; set; }
    }

    public class RetailerConfiguration : EntityBaseConfiguration<Retailer>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Retailer> b)
        {
            b.Property(x => x.No).IsRequired().HasMaxLength(10);
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
            b.Property(x => x.Pinyin).IsRequired().HasMaxLength(50);
            b.Property(x => x.Phone).HasMaxLength(50);
            b.Property(x => x.Address).HasMaxLength(200);

            b.HasIndex(x => x.No).IsUnique();

            b.HasOne(x => x.SubArea).WithMany(x => x.Retailers).HasForeignKey(x => x.SubAreaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
