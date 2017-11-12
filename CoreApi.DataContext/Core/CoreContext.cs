using Microsoft.EntityFrameworkCore;
using CoreApi.Models.Angular;
using CoreApi.Models.Core;
using Infrastructure.Features.Data;

namespace CoreApi.DataContext.Core
{
    public class CoreContext : DbContextBase
    {
        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UploadedFileConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}
