using AutoMapper;
using Infrastructure.Features.Data;
using Infrastructure.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using SalesApi.Repositories.County;

namespace SalesApi.Services.County
{
    public interface ICountyService<out T> : ICoreService<T>
    {
        ICountyDayRepository CountyDayRepository { get; }
    }

    public class CountyService<T> : CoreService<T>, ICountyService<T>
    {
        public ICountyDayRepository CountyDayRepository { get; }

        public CountyService(
            IUnitOfWork unitOfWork, 
            ILogger<T> logger, 
            IFileProvider fileProvider, 
            IMapper mapper,
            ICountyDayRepository collectiveDayRepository)
            : base(unitOfWork, logger, fileProvider, mapper)
        {
            CountyDayRepository = collectiveDayRepository;
        }
    }
}
