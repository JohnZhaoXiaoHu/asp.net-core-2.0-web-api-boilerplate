using Infrastructure.Features.Common;
using SalesApi.Shared.Enums;

namespace SalesApi.ViewModels.Subscription
{
    public class SubscriptionDayViewModel : EntityBase
    {
        public string Date { get; set; }
        public SubscriptionDayStatus Status { get; set; }
    }
}
