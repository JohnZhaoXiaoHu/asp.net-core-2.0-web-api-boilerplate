using System;
using AutoMapper;
using Infrastructure.Features.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public interface ICoreService<out T> : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        ILogger<T> Logger { get; }
        IFileProvider FileProvider { get; }
        IMapper Mapper { get; }
    }

    public class CoreService<T> : ICoreService<T>
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILogger<T> Logger { get; }
        public IFileProvider FileProvider { get; }
        public IMapper Mapper { get; }

        public CoreService(
            IUnitOfWork unitOfWork,
            ILogger<T> logger,
            IFileProvider fileProvider,
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
            FileProvider = fileProvider;
            Mapper = mapper;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}
