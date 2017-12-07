using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Retail
{
    public class RetailDayViewModel: EntityBase
    {
        public string Date { get; set; }
        public bool Initialized { get; set; }
    }
}
