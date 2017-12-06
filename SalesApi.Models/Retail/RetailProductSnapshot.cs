using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Shared.Enums;

namespace SalesApi.Models.Retail
{
    public class RetailProductSnapshot: EntityBase
    {
        public int RetailDayId { get; set; }

        public int ProductId { get; set; }
        public SalesType SalesType { get; set; }
        public int EquivalentBox { get; set; }
        public decimal Price { get; set; }
        public decimal InternalPrice { get; set; }
        public decimal BoxPrice { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }

        public string ProductName { get; set; }
        public string LegacyProductId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Specification { get; set; }
        public ProductUnit ProductUnit { get; set; }
        public int ShelfLife { get; set; }
        public decimal EquivalentTon { get; set; }
        public string Barcode { get; set; }
        public decimal TaxRate { get; set; }

        public RetailDay RetailDay { get; set; }
    }

    public class RetailProductSnapshotConfiguration : EntityBaseConfiguration<RetailProductSnapshot>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RetailProductSnapshot> b)
        {
            
        }
    }
}
