using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Settings
{
    public class Vehicle: EntityBase
    {
        public string Name { get; set; }
        public string Owner { get; set; }
    }

    public class VehicleConfiguration : EntityBaseConfiguration<Vehicle>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Owner).HasMaxLength(50);
        }
    }
}
