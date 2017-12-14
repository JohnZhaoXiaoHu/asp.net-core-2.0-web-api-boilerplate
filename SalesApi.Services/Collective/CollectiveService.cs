using AutoMapper;
using Infrastructure.Features.Data;
using Infrastructure.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using SalesApi.Repositories.Collective;

namespace SalesApi.Services.Collective
{
    public interface ICollectiveService<out T> : ICoreService<T>
    {
        ICollectiveDayRepository CollectiveDayRepository { get; }
    }

    public class CollectiveService<T> : CoreService<T>, ICollectiveService<T>
    {
        public ICollectiveDayRepository CollectiveDayRepository { get; }

        public CollectiveService(
            IUnitOfWork unitOfWork, 
            ILogger<T> logger, 
            IFileProvider fileProvider, 
            IMapper mapper,
            ICollectiveDayRepository collectiveDayRepository)
            : base(unitOfWork, logger, fileProvider, mapper)
        {
            CollectiveDayRepository = collectiveDayRepository;
        }
    }
}
