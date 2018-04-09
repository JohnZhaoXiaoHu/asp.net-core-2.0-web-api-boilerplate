using System.Collections.Generic;
using Sales.Core.Bases;

namespace Sales.Infrastructure.UsefulModels.Hateoas
{
    public abstract class LinkedResourceBaseViewModel: EntityBase
    {
        public List<LinkViewModel> Links { get; set; } = new List<LinkViewModel>();
    }
}