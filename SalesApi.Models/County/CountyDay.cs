using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.County
{
    public class CountyDay : EntityBase
    {
        public CountyDay()
        {
            CountyProductSnapshots = new List<CountyProductSnapshot>();
        }

        public string Date { get; set; }
        public bool Initialized { get; set; }

        public ICollection<CountyProductSnapshot> CountyProductSnapshots { get; set; }
    }

    public class CountyDayConfiguration : EntityBaseConfiguration<CountyDay>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CountyDay> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.HasIndex(x => x.Date).IsUnique();
        }
    }
}
