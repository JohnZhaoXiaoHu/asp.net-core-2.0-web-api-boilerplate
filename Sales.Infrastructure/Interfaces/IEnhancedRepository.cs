using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sales.Core.Bases;
using Sales.Core.Interfaces;
using Sales.Infrastructure.UsefulModels.Pagination;

namespace Sales.Infrastructure.Interfaces
{
    public interface IEnhancedRepository<T>: IRepository<T> where T: EntityBase
    {
        Task<PaginatedItems<T>> GetPaginatedAsync(PaginationParameters<T> parameters);
        Task<PaginatedItems<T>> GetPaginatedAsync(PaginationParameters<T> parameters, Expression<Func<T, bool>> criteria);
        Task<PaginatedItems<T>> GetPaginatedAsync(PaginationParameters<T> parameters, Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);
    }
}