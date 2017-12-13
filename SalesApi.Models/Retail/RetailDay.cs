using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Retail
{
    public class RetailDay : EntityBase
    {
        public RetailDay()
        {
            RetailProductSnapshots = new List<RetailProductSnapshot>();
        }

        public string Date { get; set; }
        public bool Initialized { get; set; }

        public ICollection<RetailProductSnapshot> RetailProductSnapshots { get; set; }
    }

    public class RetailDayConfiguration : EntityBaseConfiguration<RetailDay>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailDay> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.HasIndex(x => x.Date).IsUnique();
        }
    }
}
