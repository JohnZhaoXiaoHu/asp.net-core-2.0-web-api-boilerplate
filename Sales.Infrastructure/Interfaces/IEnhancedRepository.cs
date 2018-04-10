using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sales.Core.Bases;
using Sales.Core.Interfaces;
using Sales.Infrastructure.UsefulModels.Pagination;

namespace Sales.Infrastructure.Interfaces
{
    public interface IEnhancedRepository<T> : IRepository<T> where T : EntityBase
    {
        Task<PaginatedList<T>> GetPaginatedAsync<TPropertyMapping>(PaginationBase parameters) where TPropertyMapping : PropertyMapping, new();
        Task<PaginatedList<T>> GetPaginatedAsync<TPropertyMapping>(PaginationBase parameters, Expression<Func<T, bool>> criteria) where TPropertyMapping : PropertyMapping, new();
        Task<PaginatedList<T>> GetPaginatedAsync<TPropertyMapping>(PaginationBase parameters, Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes) where TPropertyMapping : PropertyMapping, new();
    }
}