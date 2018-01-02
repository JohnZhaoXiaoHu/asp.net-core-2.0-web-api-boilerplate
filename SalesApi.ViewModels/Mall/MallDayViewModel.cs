using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Mall
{
    public class MallDayViewModel : EntityBase
    {
        public string Date { get; set; }
        public bool Initialized { get; set; }
    }
}
