using System;

namespace API_DesignPatterns.Core.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        // field needed to implement soft deletion
        public bool IsDeleted { get; set; }
    }
}
