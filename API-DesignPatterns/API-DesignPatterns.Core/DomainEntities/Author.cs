using API_DesignPatterns.Core.Interfaces;
using System;

namespace API_DesignPatterns.Core.DomainEntities
{
    public class Author : IEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
