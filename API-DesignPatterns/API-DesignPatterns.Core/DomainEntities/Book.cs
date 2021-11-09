using System;

namespace API_DesignPatterns.Core.DomainEntities
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int PublishingYear { get; set; }

        public string PublishingHouse { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
