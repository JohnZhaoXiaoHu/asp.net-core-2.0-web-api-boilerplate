using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sales.Core.Bases;
using Sales.Infrastructure.Data;
using Sales.Infrastructure.Extensions;
using Sales.Infrastructure.Interfaces;
using Sales.Infrastructure.UsefulModels.Pagination;

namespace Sales.Infrastructure.Repositories
{
    public class EfEnhancedRepository<T> : EfRepository<T>, IEnhancedRepository<T> where T : EntityBase
    {
        public EfEnhancedRepository(SalesContext context): base(context)
        {
        }

        public async Task<PaginatedList<T>> GetPaginatedAsync<TPropertyMapping>(PaginationBase parameters) 
            where TPropertyMapping : PropertyMapping, new()
        {
            var collectionBeforePaging = Context.Set<T>().ApplySort(parameters.OrderBy, new TPropertyMapping());
            parameters.Count = await collectionBeforePaging.CountAsync();
            var items = await collectionBeforePaging.Skip(parameters.PageIndex * parameters.PageSize).Take(parameters.PageSize).ToListAsync();
            var result = new PaginatedList<T>(parameters, items);
            return result;
        }

        public Task<PaginatedList<T>> GetPaginatedAsync<TPropertyMapping>(PaginationBase parameters, Expression<Func<T, bool>> criteria) 
            where TPropertyMapping : PropertyMapping, new()
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<T>> GetPaginatedAsync<TPropertyMapping>(PaginationBase parameters, Expression<Func<T, bool>> criteria, 
            params Expression<Func<T, object>>[] includes) where TPropertyMapping : PropertyMapping, new()
        {
            throw new NotImplementedException();
        }
    }
}
