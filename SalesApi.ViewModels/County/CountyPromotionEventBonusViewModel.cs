using System.ComponentModel.DataAnnotations;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.County
{
    public class CountyPromotionEventBonusViewModel: EntityBase
    {
        public int CountyPromotionEventId { get; set; }
        public int ProductForCountyId { get; set; }

        [Display(Name = "赠品数")]
        [Range(1, int.MaxValue, ErrorMessage = "{0}的必须大于{1}")]
        public int BonusCount { get; set; }
    }
}
