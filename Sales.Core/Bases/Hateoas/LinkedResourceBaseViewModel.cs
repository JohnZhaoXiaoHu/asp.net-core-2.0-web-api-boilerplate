using System.Collections.Generic;

namespace Sales.Core.Bases.Hateoas
{
    public abstract class LinkedResourceBaseViewModel: EntityBase
    {
        public List<LinkViewModel> Links { get; set; } = new List<LinkViewModel>();
    }
}