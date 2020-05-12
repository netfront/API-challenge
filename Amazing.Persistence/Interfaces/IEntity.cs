using System;

namespace Amazing.Persistence.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        DateTime CreationDate { get; set; }
    }
}