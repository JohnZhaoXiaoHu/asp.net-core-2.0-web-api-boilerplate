using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Settings;

namespace SalesApi.Models.Subscription
{
    public class Milkman: EntityBase
    {
        public int SubAreaId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Pinyin { get; set; }
        public string Phone { get; set; }
        public string IdentityCardNo { get; set; }
        public string LegacyId { get; set; }

        public SubArea SubArea { get; set; }
    }

    public class MilkmanConfiguration : EntityBaseConfiguration<Milkman>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Milkman> b)
        {
            b.Property(x => x.No).IsRequired().HasMaxLength(10);
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
            b.Property(x => x.Pinyin).IsRequired().HasMaxLength(50);
            b.Property(x => x.Phone).HasMaxLength(50);
            b.Property(x => x.IdentityCardNo).HasMaxLength(50);
            b.Property(x => x.LegacyId).HasMaxLength(10);
            
            b.HasOne(x => x.SubArea).WithMany(x => x.Milkmen).HasForeignKey(x => x.SubAreaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
