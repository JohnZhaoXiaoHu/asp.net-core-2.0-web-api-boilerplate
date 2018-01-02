using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Mall
{
    public class MallPriceViewModel: EntityBase
    {
        public int MallCustomerId { get; set; }
        public int ProductForMallId { get; set; }
        public decimal Price { get; set; }
    }
}
