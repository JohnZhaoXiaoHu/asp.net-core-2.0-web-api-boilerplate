using AutoMapper;
using Infrastructure.Features.Data;
using Infrastructure.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using SalesApi.Repositories.Mall;

namespace SalesApi.Services.Mall
{
    public interface IMallService<out T> : ICoreService<T>
    {
        IMallDayRepository MallDayRepository { get; }
    }

    public class MallService<T> : CoreService<T>, IMallService<T>
    {
        public IMallDayRepository MallDayRepository { get; }

        public MallService(
            IUnitOfWork unitOfWork, 
            ILogger<T> logger, 
            IFileProvider fileProvider, 
            IMapper mapper,
            IMallDayRepository collectiveDayRepository)
            : base(unitOfWork, logger, fileProvider, mapper)
        {
            MallDayRepository = collectiveDayRepository;
        }
    }
}
