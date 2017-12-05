using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionEventViewModel: EntityBase
    {
        public int RetailPromotionSeriesId { get; set; }
        public int ProductForRetailId { get; set; }

        [Display(Name = "活动名称")]
        [StringLength(20, ErrorMessage = "{0}的长度不能超过{1}")]
        public string Name { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "基数")]
        [Range(1, int.MaxValue, ErrorMessage = "{0}的必须大于{1}")]
        public int PurchaseBase { get; set; }

        public string SeriesName { get; set; }
        public List<RetailPromotionEventBonusViewModel> RetailPromotionEventBonuses { get; set; }
    }
}
