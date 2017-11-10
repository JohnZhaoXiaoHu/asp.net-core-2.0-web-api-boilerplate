using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreApi.DataContext.Core;
using CoreApi.Infrastructure.Configurations;
using CoreApi.Models.Angular;
using CoreApi.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.DataContext.Infrastructure
{
    public abstract class DbContextBase : DbContext, IUnitOfWork
    {
        protected DbContextBase(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(AppSettings.DefaultSchema);
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
