using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.County
{
    public class CountyAgentPrice: EntityBase
    {
        public int CountyAgentId { get; set; }
        public int ProductForCountyId { get; set; }
        public decimal Price { get; set; }

        public CountyAgent CountyAgent { get; set; }
        public ProductForCounty ProductForCounty { get; set; }
    }

    public class CountyAgentPriceConfiguration : EntityBaseConfiguration<CountyAgentPrice>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyAgentPrice> b)
        {
            b.HasOne(x => x.CountyAgent).WithMany(x => x.CountyAgentPrices)
                .HasForeignKey(x => x.CountyAgentId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ProductForCounty).WithMany(x => x.CountyAgentPrices)
                .HasForeignKey(x => x.ProductForCountyId).OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(x => new {x.CountyAgentId, x.ProductForCountyId}).IsUnique();
        }
    }
}
