using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Overall
{
    public class SalesDay : EntityBase
    {
        public string Date { get; set; }
        public SalesDayStatus Status { get; set; }
    }

    public class SalesDayConfiguration : EntityBaseConfiguration<SalesDay>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SalesDay> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(8);
            b.HasIndex(x => x.Date).IsUnique();
        }
    }
}
