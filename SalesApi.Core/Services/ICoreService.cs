using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SalesApi.Core.Abstractions.Data;

namespace SalesApi.Core.Services
{
    public interface ICoreService<out T> : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        ILogger<T> Logger { get; }
        IMapper Mapper { get; }
    }
}