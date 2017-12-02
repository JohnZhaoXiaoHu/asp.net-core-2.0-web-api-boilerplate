using System;
using System.Collections.Generic;
using SharedSettings.Models;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionEventForFullCalendarViewModel: FullCalendarEventViewModel
    {
        public int RetailPromotionSeriesId { get; set; }
        public int ProductForRetailId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PurchaseBase { get; set; }

        public List<RetailPromotionEventBonusViewModel> RetailPromotionEventBonuses { get; set; }
    }
}
