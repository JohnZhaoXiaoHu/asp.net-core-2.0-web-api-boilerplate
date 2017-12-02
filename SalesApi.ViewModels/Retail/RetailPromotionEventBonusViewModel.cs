using System.ComponentModel.DataAnnotations;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionEventBonusViewModel: EntityBase
    {
        public int RetailPromotionEventId { get; set; }
        public int ProductForRetailId { get; set; }

        [Display(Name = "赠品数")]
        [Range(1, int.MaxValue, ErrorMessage = "{0}的必须大于{1}")]
        public int BonusCount { get; set; }
    }
}
