using AutoMapper;
using Microsoft.Extensions.Logging;
using SalesApi.Core.Abstractions.Data;

namespace SalesApi.Core.Services
{
    public class CoreService<T> : ICoreService<T>
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILogger<T> Logger { get; }
        public IMapper Mapper { get; }

        public CoreService(
            IUnitOfWork unitOfWork,
            ILogger<T> logger,
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
            Mapper = mapper;
        }

        public virtual void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}
