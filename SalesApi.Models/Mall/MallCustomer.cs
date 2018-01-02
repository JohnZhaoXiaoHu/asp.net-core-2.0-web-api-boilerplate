using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Settings;

namespace SalesApi.Models.Mall
{
    public class MallCustomer : EntityBase
    {
        public MallCustomer()
        {
            MallPrices = new List<MallPrice>();
        }

        public int SubAreaId { get; set; }
        public int? MallGroupId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Pinyin { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public SubArea SubArea { get; set; }
        public ICollection<MallPrice> MallPrices { get; set; }
        public MallGroup MallGroup { get; set; }
    }

    public class MallCustomerConfiguration : EntityBaseConfiguration<MallCustomer>
    {
        public override void ConfigureDerived(EntityTypeBuilder<MallCustomer> b)
        {
            b.Property(x => x.No).IsRequired().HasMaxLength(10);
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
            b.Property(x => x.Pinyin).IsRequired().HasMaxLength(50);
            b.Property(x => x.Phone).HasMaxLength(50);
            b.Property(x => x.Address).HasMaxLength(200);

            b.HasIndex(x => x.No).IsUnique();
            b.HasIndex(x => x.Name).IsUnique();

            b.HasOne(x => x.SubArea).WithMany(x => x.MallCustomers).HasForeignKey(x => x.SubAreaId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.MallGroup).WithMany(x => x.MallCustomers).HasForeignKey(x => x.MallGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
