using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Core.Abstractions.DomainModels;

namespace SalesApi.Core.DomainModels
{
    public class CustomerConfiguration : EntityBaseConfiguration<Customer>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Customer> b)
        {
            b.Property(x => x.Company).IsRequired().HasMaxLength(100);
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}