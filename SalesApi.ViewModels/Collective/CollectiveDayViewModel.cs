using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Collective
{
    public class CollectiveDayViewModel : EntityBase
    {
        public string Date { get; set; }
        public bool Initialized { get; set; }
    }
}
