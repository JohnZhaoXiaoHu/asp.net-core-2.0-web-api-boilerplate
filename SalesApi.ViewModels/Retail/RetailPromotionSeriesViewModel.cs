using System;
using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionSeriesViewModel : EntityBase
    {
        public SalesType SalesType { get; set; }
        public int ProductForRetailId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PurchaseBase { get; set; }
    }
}
