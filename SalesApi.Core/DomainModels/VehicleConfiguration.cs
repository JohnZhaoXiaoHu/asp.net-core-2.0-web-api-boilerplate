using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Core.Abstractions.DomainModels;

namespace SalesApi.Core.DomainModels
{
    public class VehicleConfiguration : EntityBaseConfiguration<Vehicle>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Vehicle> b)
        {
            b.Property(x => x.Model).IsRequired().HasMaxLength(50);
            b.Property(x => x.Owner).IsRequired().HasMaxLength(50);
        }
    }
}