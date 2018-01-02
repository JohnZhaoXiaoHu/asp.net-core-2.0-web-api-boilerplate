using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Models.Settings;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.County
{
    public class CountyAgent: EntityBase
    {
        public CountyAgent()
        {
            CountyAgentPrices = new List<CountyAgentPrice>();
        }

        public int SubAreaId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string Pinyin { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public SubArea SubArea { get; set; }
        public ICollection<CountyAgentPrice> CountyAgentPrices { get; set; }
    }

    public class CountyAgentConfiguration : EntityBaseConfiguration<CountyAgent>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyAgent> b)
        {
            b.Property(x => x.No).IsRequired().HasMaxLength(10);
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
            b.Property(x => x.Pinyin).IsRequired().HasMaxLength(50);
            b.Property(x => x.Phone).HasMaxLength(50);
            b.Property(x => x.Address).HasMaxLength(200);

            b.HasIndex(x => x.No).IsUnique();

            b.HasOne(x => x.SubArea).WithMany(x => x.CountyAgents).HasForeignKey(x => x.SubAreaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
