using System;

namespace API_DesignPatterns.Core.DomainEntities
{
    public class Author
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // field needed to implement soft deletion
        public bool IsDeleted { get; set; }
    }
}
