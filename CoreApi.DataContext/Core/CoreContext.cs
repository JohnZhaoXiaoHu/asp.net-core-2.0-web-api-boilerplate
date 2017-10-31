using System.Threading;
using System.Threading.Tasks;
using CoreApi.DataContext.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CoreApi.Infrastructure.Configurations;
using CoreApi.Models.Angular;
using CoreApi.Models.Core;

namespace CoreApi.DataContext.Core
{
    public class CoreContext : DbContext, IUnitOfWork
    {
        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(AppSettings.DefaultSchema);

            modelBuilder.ApplyConfiguration(new UploadedFileConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<Client> Clients { get; set; }

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
