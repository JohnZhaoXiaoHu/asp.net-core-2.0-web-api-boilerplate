using AutoMapper;
using Infrastructure.Features.Data;
using Infrastructure.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using SalesApi.Repositories.Retail;

namespace SalesApi.Services.Retail
{
    public interface IRetailService<out T> : ICoreService<T>
    {
        IRetailDayRepository RetailDayRepository { get; }
    }

    public class RetailService<T> : CoreService<T>, IRetailService<T>
    {
        public IRetailDayRepository RetailDayRepository { get; }

        public RetailService(
            IUnitOfWork unitOfWork, 
            ILogger<T> logger, 
            IFileProvider fileProvider, 
            IMapper mapper,
            IRetailDayRepository retailDayRepository)
            : base(unitOfWork, logger, fileProvider, mapper)
        {
            RetailDayRepository = retailDayRepository;
        }
    }
}
