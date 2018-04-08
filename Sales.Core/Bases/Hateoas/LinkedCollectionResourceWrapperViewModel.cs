using System.Collections.Generic;

namespace Sales.Core.Bases.Hateoas
{
    public class LinkedCollectionResourceWrapperViewModel<T> : LinkedResourceBaseViewModel
        where T : LinkedResourceBaseViewModel
    {
        public LinkedCollectionResourceWrapperViewModel(IEnumerable<T> value)
        {
            Value = value;
        }

        public IEnumerable<T> Value { get; set; }
    }
}