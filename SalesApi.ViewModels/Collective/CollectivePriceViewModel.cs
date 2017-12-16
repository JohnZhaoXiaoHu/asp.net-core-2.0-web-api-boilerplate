using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Collective
{
    public class CollectivePriceViewModel: EntityBase
    {
        public int CollectiveCustomerId { get; set; }
        public int ProductForCollectiveId { get; set; }
        public decimal Price { get; set; }
    }
}
