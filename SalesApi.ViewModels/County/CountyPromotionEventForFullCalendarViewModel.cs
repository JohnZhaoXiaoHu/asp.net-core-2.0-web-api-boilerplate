using System;
using System.Collections.Generic;
using SharedSettings.Models;

namespace SalesApi.ViewModels.County
{
    public class CountyPromotionEventForFullCalendarViewModel: FullCalendarEventViewModel
    {
        public int CountyPromotionSeriesId { get; set; }
        public int ProductForCountyId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PurchaseBase { get; set; }

        public List<CountyPromotionEventBonusViewModel> CountyPromotionEventBonuses { get; set; }
    }
}
