using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SalesApi.Core.Abstractions.DomainModels;

namespace SalesApi.Core.Abstractions.Data
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<int> CountAsync();
        Task<T> GetSingleAsync(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        void Detach(T entity);
        void DetachRange(IEnumerable<T> entities);
        void AttachAsModified(T entity);
    }
}