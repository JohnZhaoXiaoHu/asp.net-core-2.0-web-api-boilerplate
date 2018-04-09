using Sales.Core.DomainModels.Enums;
using Sales.Core.Interfaces;

namespace Sales.Api.ViewModels
{
    public class ProductModificationViewModel: IDeleted, IOrder
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Specification { get; set; }
        public ProductUnit ProductUnit { get; set; }
        public int ShelfLife { get; set; }
        public decimal EquivalentTon { get; set; }
        public string Barcode { get; set; }
        public decimal TaxRate { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
    }
}
