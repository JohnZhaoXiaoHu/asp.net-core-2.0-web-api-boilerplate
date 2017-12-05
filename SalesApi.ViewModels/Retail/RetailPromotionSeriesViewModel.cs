using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionSeriesViewModel : EntityBase
    {
        public SalesType SalesType { get; set; }
        public int ProductForRetailId { get; set; }

        [Display(Name = "活动名称")]
        [StringLength(20, ErrorMessage = "{0}的长度不能超过{1}")]
        public string Name { get; set; }

        public DateRepeatType DateRepeatType { get; set; }

        [Display(Name = "周期步幅")]
        [Range(1, int.MaxValue, ErrorMessage = "{0}的必须大于{1}")]
        public int Step { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Display(Name = "基数")]
        [Range(1, int.MaxValue, ErrorMessage = "{0}的必须大于{1}")]
        public int PurchaseBase { get; set; }
    }
}
