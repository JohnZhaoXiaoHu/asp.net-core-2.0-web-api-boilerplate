using System.Collections.Generic;
using SalesApi.ViewModels.County;

namespace SalesApi.ViewModels.Retail
{
    public class RetailOrderWithGiftViewModel: RetailOrderViewModel
    {
        public List<CountyPromotionGiftOrderViewModel> CountyPromotionGiftOrders { get; set; }
    }
}
