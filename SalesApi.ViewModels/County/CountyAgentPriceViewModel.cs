using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.County
{
    public class CountyAgentPriceViewModel: EntityBase
    {
        public int CountyAgentId { get; set; }
        public int ProductForCountyId { get; set; }
        public decimal Price { get; set; }
    }
}
