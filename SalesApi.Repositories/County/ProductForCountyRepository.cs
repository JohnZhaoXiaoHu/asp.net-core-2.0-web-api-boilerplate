using Infrastructure.Features.Data;
using SalesApi.Models.County;

namespace SalesApi.Repositories.County
{
    public interface IProductForCountyRepository : IEntityBaseRepository<ProductForCounty> { }

    public class ProductForCountyRepository : EntityBaseRepository<ProductForCounty>, IProductForCountyRepository
    {
        public ProductForCountyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
