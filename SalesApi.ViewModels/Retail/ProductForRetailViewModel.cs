using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Retail
{
    public class ProductForRetailViewModel: EntityBase
    {
        public int ProductId { get; set; }
        public SalesType SalesType { get; set; }
        public int EquivalentBox { get; set; }
        public decimal Price { get; set; }
        public decimal InternalPrice { get; set; }
        public decimal BoxPrice { get; set; }
        public bool IsOrderByBox { get; set; }
        public int MinOrderCount { get; set; }
        public int OrderDivisor { get; set; }
    }
}
