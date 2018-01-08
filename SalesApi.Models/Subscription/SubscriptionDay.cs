using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Subscription
{
    public class SubscriptionDay : EntityBase
    {
        public SubscriptionDay()
        {
            SubscriptionProductSnapshots = new List<SubscriptionProductSnapshot>();
        }

        public string Date { get; set; }
        public bool Initialized { get; set; }

        public ICollection<SubscriptionProductSnapshot> SubscriptionProductSnapshots { get; set; }
    }

    public class SubscriptionDayConfiguration : EntityBaseConfiguration<SubscriptionDay>
    {
        public override void ConfigureDerived(EntityTypeBuilder<SubscriptionDay> b)
        {
            b.Property(x => x.Date).IsRequired().HasMaxLength(10);
            b.HasIndex(x => x.Date).IsUnique();
        }
    }
}
