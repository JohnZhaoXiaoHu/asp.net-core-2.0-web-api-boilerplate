using System.Collections.Generic;
using SalesApi.Core.Abstractions.DomainModels;

namespace SalesApi.Core.Abstractions.Hateoas
{
    public abstract class LinkedResourceBaseViewModel: EntityBase
    {
        public List<LinkViewModel> Links { get; set; } = new List<LinkViewModel>();
    }
}