using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Collective
{
    public class ProductForCollectiveViewModel: EntityBase
    {
        public int ProductId { get; set; }
        
        [Display(Name = "折箱")]
        [Range(1, Int32.MaxValue, ErrorMessage = "{0}不可以小于{1}")]
        public int EquivalentBox { get; set; }
        
        public bool IsOrderByBox { get; set; }

        [Display(Name = "最小订货量")]
        [Range(0, int.MaxValue, ErrorMessage = "{0}不可以小于{1}")]
        public int MinOrderCount { get; set; }

        [Display(Name = "报货整数倍")]
        [Range(0, int.MaxValue, ErrorMessage = "{0}不可以小于{1}")]
        public int OrderDivisor { get; set; }

        public string ProductName { get; set; }
    }
}
