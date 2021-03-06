using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_DesignPatterns.Core.DTOs
{
    public class AddBookDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int PublishingYear { get; set; }

        [Required]
        public string PublishingHouse { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IEnumerable<Guid> AuthorIds { get; set; }

        public bool ValidateOnly { get; set; } = false;
    }
}
