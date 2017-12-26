using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.County
{
    public class CountyDayViewModel : EntityBase
    {
        public string Date { get; set; }
        public bool Initialized { get; set; }
    }
}
