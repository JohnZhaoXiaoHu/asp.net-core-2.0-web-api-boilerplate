using System.Collections.Generic;

namespace SalesApi.ViewModels.County
{
    public class CountyOrderWithGiftViewModel: CountyOrderViewModel
    {
        public List<CountyPromotionGiftOrderViewModel> CountyPromotionGiftOrders { get; set; }
    }
}
