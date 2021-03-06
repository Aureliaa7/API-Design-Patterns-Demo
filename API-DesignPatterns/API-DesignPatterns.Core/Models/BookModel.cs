using System;

namespace API_DesignPatterns.Core.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int PublishingYear { get; set; }

        public string PublishingHouse { get; set; }

        public string Description { get; set; }

        public string AuthorNames { get; set; }

        public bool IsDeleted { get; set; }
    }
}
