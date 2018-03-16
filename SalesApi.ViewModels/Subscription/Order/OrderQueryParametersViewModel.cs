using System;
using System.Collections.Generic;

namespace SalesApi.ViewModels.Subscription.Order
{
    public class OrderQueryParametersViewModel
    {
        public DateTime? OrderTimeStart { get; set; }
        public DateTime? OrderTimeEnd { get; set; }
        public bool IsOrderTimeAccurate { get; set; }
        public DateTime? CreateTimeStart { get; set; }
        public DateTime? CreateTimeEnd { get; set; }
        public DateTime? PaidTimeStart { get; set; }
        public DateTime? PaidTimeEnd { get; set; }
        public bool? HasPaid { get; set; }

        public List<int> MilkmanIds { get; set; }
    }
}