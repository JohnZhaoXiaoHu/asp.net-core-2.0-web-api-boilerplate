using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Features.Data;
using SalesApi.Models.Retail;

namespace SalesApi.Repositories.Retail
{
    public interface IRetailDayRepository : IEntityBaseRepository<RetailDay> { }

    public class RetailDayRepository : EntityBaseRepository<RetailDay>, IRetailDayRepository
    {
        public RetailDayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
