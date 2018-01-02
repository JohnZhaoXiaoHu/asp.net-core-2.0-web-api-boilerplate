using System.Collections.Generic;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesApi.Models.Mall
{
    public class MallGroup: EntityBase
    {
        public MallGroup()
        {
            MallCustomers = new List<MallCustomer>();
        }

        public string Name { get; set; }
        public ICollection<MallCustomer> MallCustomers { get; set; }
    }

    public class MallGroupConfiguration : EntityBaseConfiguration<MallGroup>
    {
        public override void ConfigureDerived(EntityTypeBuilder<MallGroup> b)
        {
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
            b.HasIndex(x => x.Name).IsUnique();
        }
    }
}
