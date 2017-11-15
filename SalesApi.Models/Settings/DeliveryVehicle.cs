using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedSettings.Enums;

namespace SalesApi.Models.Settings
{
    public class DeliveryVehicle: EntityBase
    {
        public SalesType SalesType { get; set; }
        public string LegacyAreaId { get; set; }
        public int VehicleId { get; set; }
        public int DistributionGroupId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DistributionGroup DistributionGroup { get; set; }
    }

    public class DeliveryVehicleConfiguration : EntityBaseConfiguration<DeliveryVehicle>
    {
        public override void ConfigureDerived(EntityTypeBuilder<DeliveryVehicle> b)
        {
            b.Property(x => x.LegacyAreaId).HasMaxLength(10);
            b.HasOne(x => x.Vehicle).WithMany().HasForeignKey(x => x.VehicleId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.DistributionGroup).WithMany().HasForeignKey(x => x.DistributionGroupId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
