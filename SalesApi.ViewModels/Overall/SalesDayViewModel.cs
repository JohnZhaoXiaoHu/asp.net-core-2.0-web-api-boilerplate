using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Overall
{
    public class SalesDayViewModel: EntityBase
    {
        public string Date { get; set; }
        public SalesDayStatus Status { get; set; }
    }
}
