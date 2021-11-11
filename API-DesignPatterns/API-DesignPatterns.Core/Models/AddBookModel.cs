using System;
using System.Collections.Generic;

namespace API_DesignPatterns.Core.Models
{
    public class AddBookModel
    {
        public string Title { get; set; }

        public int PublishingYear { get; set; }

        public string PublishingHouse { get; set; }

        public string Description { get; set; }

        public IEnumerable<Guid> AuthorIds { get; set; }

        public bool ValidateOnly { get; set; }
    }
}
