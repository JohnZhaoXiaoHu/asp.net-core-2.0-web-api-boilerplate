using AutoMapper;
using Infrastructure.Features.Data;
using Infrastructure.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using SalesApi.Repositories.Subscription;

namespace SalesApi.Services.Subscription
{
    public interface ISubscriptionService<out T> : ICoreService<T>
    {
        ISubscriptionDayRepository SubscriptionDayRepository { get; }
    }

    public class SubscriptionService<T> : CoreService<T>, ISubscriptionService<T>
    {
        public ISubscriptionDayRepository SubscriptionDayRepository { get; }

        public SubscriptionService(
            IUnitOfWork unitOfWork, 
            ILogger<T> logger, 
            IFileProvider fileProvider, 
            IMapper mapper,
            ISubscriptionDayRepository collectiveDayRepository)
            : base(unitOfWork, logger, fileProvider, mapper)
        {
            SubscriptionDayRepository = collectiveDayRepository;
        }
    }
}
