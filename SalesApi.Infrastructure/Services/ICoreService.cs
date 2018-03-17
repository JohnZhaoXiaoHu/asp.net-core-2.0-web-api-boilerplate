using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SalesApi.Infrastructure.Abstractions.Data;

namespace SalesApi.Infrastructure.Services
{
    public interface ICoreService<out T> : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        ILogger<T> Logger { get; }
        IMapper Mapper { get; }
    }
}