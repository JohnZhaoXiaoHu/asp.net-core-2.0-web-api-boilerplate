using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesApi.Core.Abstractions.Data
{
    public abstract class DbContextBase : DbContext, IUnitOfWork
    {
        protected DbContextBase(DbContextOptions options)
            : base(options)
        {
        }
        
        public bool Save()
        {
            return SaveChanges() >= 0;
        }

        public bool Save(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges(acceptAllChangesOnSuccess) >= 0;
        }

        public async Task<bool> SaveAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken) >= 0;
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaveChangesAsync(cancellationToken) >= 0;
        }
    }
}
