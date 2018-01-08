using Infrastructure.Features.Common;

namespace SalesApi.ViewModels.Subscription
{
    public class SubscriptionDayViewModel : EntityBase
    {
        public string Date { get; set; }
        public bool Initialized { get; set; }
    }
}
