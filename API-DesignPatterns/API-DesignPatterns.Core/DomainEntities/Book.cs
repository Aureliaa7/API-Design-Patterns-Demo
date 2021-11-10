using API_DesignPatterns.Core.Interfaces;
using System;

namespace API_DesignPatterns.Core.DomainEntities
{
    public class Book : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int PublishingYear { get; set; }

        public string PublishingHouse { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
