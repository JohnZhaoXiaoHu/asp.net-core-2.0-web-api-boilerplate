using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Collective
{
    public class CollectiveDay : EntityBase
    {
        public CollectiveDay()
        {
            CollectiveProductSnapshots = new List<CollectiveProductSnapshot>();
        }

        public string Date { get; set; }
        public bool Initialized { get; set; }

        public ICollection<CollectiveProductSnapshot> CollectiveProductSnapshots { get; set; }
    }

    public class CollectiveDayConfiguration : EntityBaseConfiguration<CollectiveDay>
    {
        public override void ConfigureDerived(EntityTypeBuilder<CollectiveDay> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.HasIndex(x => x.Date).IsUnique();
        }
    }
}
