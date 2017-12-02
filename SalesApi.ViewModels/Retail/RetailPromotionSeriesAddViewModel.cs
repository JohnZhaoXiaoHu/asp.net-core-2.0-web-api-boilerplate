using System.Collections.Generic;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionSeriesAddViewModel : RetailPromotionSeriesViewModel
    {
        public List<RetailPromotionSeriesBonusViewModel> RetailPromotionSeriesBonuses { get; set; }
    }
}
