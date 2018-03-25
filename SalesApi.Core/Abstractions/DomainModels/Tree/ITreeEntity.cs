using System.Collections.Generic;

namespace SalesApi.Core.Abstractions.DomainModels.Tree
{
    public interface ITreeEntity<T> where T : EntityBase
    {
        int? ParentId { get; set; }
        string AncestorIds { get; set; }
        bool IsAbstract { get; set; }
        int Level { get; }
        T Parent { get; set; }
        ICollection<T> Children { get; set; }
    }
}
