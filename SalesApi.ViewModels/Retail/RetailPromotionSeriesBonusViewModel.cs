using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionSeriesBonusViewModel: EntityBase
    {
        public int PromotionSeriesId { get; set; }
        public int ProductForRetailId { get; set; }
        public int BonusCount { get; set; }
    }
}
