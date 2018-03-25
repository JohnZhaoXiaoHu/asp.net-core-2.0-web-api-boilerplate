using SalesApi.Core.Abstractions.DomainModels;
using SalesApi.Shared.Enums;

namespace SalesApi.Core.DomainModels
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Specification { get; set; }
        public ProductUnit ProductUnit { get; set; }
        public int ShelfLife { get; set; }
        public decimal EquivalentTon { get; set; }
        public string Barcode { get; set; }
        public decimal TaxRate { get; set; }
    }
}
