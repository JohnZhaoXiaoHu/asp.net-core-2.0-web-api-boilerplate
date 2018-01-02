using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class ProductForRetailViewModel: EntityBase
    {
        public int ProductId { get; set; }
        
        [Display(Name = "折箱")]
        [Range(1, Int32.MaxValue, ErrorMessage = "{0}不可以小于{1}")]
        public int EquivalentBox { get; set; }

        [Display(Name = "单价")]
        [Range(0, double.MaxValue, ErrorMessage = "{0}不可以小于{1}")]
        public decimal Price { get; set; }

        [Display(Name = "内部单价")]
        [Range(0, double.MaxValue, ErrorMessage = "{0}不可以小于{1}")]
        public decimal InternalPrice { get; set; }

        [Display(Name = "整箱价")]
        [Range(0, double.MaxValue, ErrorMessage = "{0}不可以小于{1}")]
        public decimal BoxPrice { get; set; }

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
