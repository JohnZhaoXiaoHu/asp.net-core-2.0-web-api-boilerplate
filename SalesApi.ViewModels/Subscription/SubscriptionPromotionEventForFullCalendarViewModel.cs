using System;
using System.Collections.Generic;
using SharedSettings.Models;

namespace SalesApi.ViewModels.Subscription
{
    public class SubscriptionPromotionEventForFullCalendarViewModel: FullCalendarEventViewModel
    {
        public int SubscriptionPromotionSeriesId { get; set; }
        public int ProductForSubscriptionId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PurchaseBase { get; set; }

        public List<SubscriptionPromotionEventBonusViewModel> SubscriptionPromotionEventBonuses { get; set; }
    }
}
