using System.Collections.Generic;

namespace SalesApi.Core.Abstractions.Hateoas
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