using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Settings
{
    public class DistributionGroup: EntityBase
    {
        public DistributionGroup()
        {
            DeliveryVehicles = new List<DeliveryVehicle>();
        }

        public int No { get; set; }
        public string Name { get; set; }
        public ICollection<DeliveryVehicle> DeliveryVehicles { get; set; }
    }

    public class DistributionGroupConfiguration : EntityBaseConfiguration<DistributionGroup>
    {
        public override void ConfigureDerived(EntityTypeBuilder<DistributionGroup> b)
        {
            b.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
