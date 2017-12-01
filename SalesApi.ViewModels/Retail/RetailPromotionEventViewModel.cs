using System;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionEventViewModel: EntityBase
    {
        public int PromotionSeriesId { get; set; }
        public int ProductForRetailId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int PurchaseBase { get; set; }
    }
}
