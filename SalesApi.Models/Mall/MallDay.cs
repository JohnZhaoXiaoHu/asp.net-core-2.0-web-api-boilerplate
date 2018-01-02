using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Mall
{
    public class MallDay : EntityBase
    {
        public MallDay()
        {
            MallProductSnapshots = new List<MallProductSnapshot>();
        }

        public string Date { get; set; }
        public bool Initialized { get; set; }

        public ICollection<MallProductSnapshot> MallProductSnapshots { get; set; }
    }

    public class MallDayConfiguration : EntityBaseConfiguration<MallDay>
    {
        public override void ConfigureDerived(EntityTypeBuilder<MallDay> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.HasIndex(x => x.Date).IsUnique();
        }
    }
}
